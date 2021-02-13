using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Client
{
    public partial class Client : Form

    {
        bool terminated = false;
        bool connected = false;
        string downloadfile = "";
        string folderName = "";
        string uname = "";
        Socket clientSocket;

        string m_splitter = "'\\'";
        string m_fName;
        string[] m_split = null; // splitted words
        byte[] m_clientData; // data to be sent

        public Client()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Client_FormClosing);

            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void Client_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) // disconnect by closing client window
        {
            connected = false;
            terminated = true;
            Environment.Exit(0);
        }

        private void label_IP_Click(object sender, EventArgs e)
        {

        }

        private void textBox_IP_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_Port_Click(object sender, EventArgs e)
        {

        }

        private void textBox_Port_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_IP.Text;

            int portNum;
            if (Int32.TryParse(textBox_Port.Text, out portNum)) 
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    button_Connect.Enabled = false;
                    connected = true;
                    richTextBox_msg.AppendText("Connection established...\n");
                    richTextBox_msg.AppendText("Port: "+ portNum.ToString()+"\n");
                    
                    if (connected == true) { // get server details

                        uname = textBox_uname.Text;
                        button_Browse.Enabled = true;
                        button1.Enabled = true;

                        if (uname != "" && uname.Length <= 64)
                        {
                            Byte[] buffer = new Byte[64];
                            buffer = Encoding.Default.GetBytes(uname);
                            clientSocket.Send(buffer); //send username
                            richTextBox_msg.AppendText("Username sent: " + uname + "\n");
                            Thread receiveThread = new Thread(Receive); //recieve message to confirm connection
                            receiveThread.Start();
                        }
                    }
                }
                catch // print error messages
                {
                    richTextBox_msg.AppendText("Problem occured while connecting...\n");
                }
            }
            else
            {
                richTextBox_msg.AppendText("Problem occured while connecting...\n");
            }

        }

        private void richTextBox_msg_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_uname_Click(object sender, EventArgs e)
        {

        }

        private void textBox_uname_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            char[] delimiter = m_splitter.ToCharArray();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog(); // check if the user has selected a file
            if (result == DialogResult.OK) // if selected, continue
            {
                try
                {
                    textBox_file.Text = openFileDialog.FileName; // displays path of file
                    m_split = (textBox_file.Text).Split(delimiter);
                    int limit = m_split.Length;
                    m_fName = m_split[limit - 1].ToString(); // get file name

                    if (textBox_file.Text != null)
                        button_transfer.Enabled = true; //enable transfer button to send the file to server
                }
                catch (Exception ex) // if file could not be acquired
                {
                    richTextBox_msg.AppendText("\n" + ex.Message); // display error message
                }
            }
        }

        private void textBox_file_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_file_Click(object sender, EventArgs e)
        {

        }

        private void button_transfer_Click(object sender, EventArgs e)
        {
            try
            {
                
                byte[] fileName = Encoding.UTF8.GetBytes(m_fName); //file name
                byte[] fileData = File.ReadAllBytes(textBox_file.Text); //file
                byte[] fileNameLen = BitConverter.GetBytes(fileName.Length); //length of file name
                m_clientData = new byte[4 + fileName.Length + fileData.Length]; //file to be sent

                //get file details to send
                richTextBox_msg.AppendText("Transferring file: " + m_fName + "\n");
                fileNameLen.CopyTo(m_clientData, 0); 
                fileName.CopyTo(m_clientData, 4); 
                fileData.CopyTo(m_clientData, 4 + fileName.Length);

                //send file to server
                clientSocket.Send(m_clientData);
                richTextBox_msg.AppendText("The file has been transferred: " + m_fName + "\n");
                
            }
            catch (Exception ex) //fail to send
            {
                richTextBox_msg.AppendText("\n" + ex.Message);
            }
        }

        private void Receive()
        {
            while(connected)
            {
                try
                {
                    
                    Byte[] buffer = new Byte[256];
                    clientSocket.Receive(buffer); // recieve message from server

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    richTextBox_msg.AppendText("Server: " + incomingMessage + "\n"); // display server message
                    button_Connect.Enabled = true;

                    
                    if(incomingMessage.Contains("Download")) //gets downloaded file and starts download operation
                    {
                        downloadFunc();
                    }

                    

                }
                catch
                {
                    if (!terminated) // disconnection occured
                    {
                        richTextBox_msg.AppendText("The server has disconnected\n"); // display disconnection error to user
                        clientSocket.Close();
                        connected = false;
                        button_Connect.Enabled = true;
                    }

                }
            }
        }

        private void requestfile_Click(object sender, EventArgs e)
        {
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes("requestFile" );
            clientSocket.Send(buffer); //send file request
            richTextBox_msg.AppendText("Request for files sent" + "\n");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void browseDownload_Click(object sender, EventArgs e)
        {
            
            var folderBrowserDialog1 = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog1.ShowDialog(); // opens the file browser and waits for the user to select file
            if (result == DialogResult.OK)
            {
                // if we succesfully select a file then we store directory
                folderName = folderBrowserDialog1.SelectedPath;
                textBoxDownloadDirectory.Text = folderName;

                if (textBoxDownloadDirectory.Text != null)
                {
                    download.Enabled = true; //enable transfer button to send the file to server
                    
                }

            }
        }

        private void download_Click(object sender, EventArgs e) // download button: for owned files 
        {

            string message = textBoxDownloadFile.Text;
            downloadfile = message;
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes("downloadOwnFile " + downloadfile);
            clientSocket.Send(buffer); //send file request
            richTextBox_msg.AppendText("Request to download file " + downloadfile + "\n");
        }

        private void copy_Click(object sender, EventArgs e)
        {
            string message = textBoxDownloadFile.Text;
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes("copyFile " + message);
            clientSocket.Send(buffer); //send file request
            richTextBox_msg.AppendText("Request to copy file " + message + "\n");
        }

        private void button1_Click(object sender, EventArgs e) //disconnect button
        {
            connected = false;
            terminated = true;
            clientSocket.Close();
            button_Connect.Enabled = true;
            button1.Enabled = false;
        }

        private void delete_Click(object sender, EventArgs e) // delete file button
        {
            string message = textBoxDownloadFile.Text;
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes("deleteFile " + message);
            clientSocket.Send(buffer); //send file request
            richTextBox_msg.AppendText("Request to delete file " + message + "\n");
        }

        
        private void downloadFunc() // download operation is implemented in a similar way to transfer/upload operation
        {
            try
            {
                byte[] clientData = new byte[1024 * 5000];

                int receivedBytesLen = clientSocket.Receive(clientData);

                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(folderName + "\\" + fileName, FileMode.Append));
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
                bWrite.Close();
                richTextBox_msg.AppendText("Successfully downloaded file " + fileName + "\n");
            }
            catch
            {
                richTextBox_msg.AppendText("File could not be downloaded!");
            }


        }

        private void publicButton_Click(object sender, EventArgs e) // get public file list
        {
            string message = textBoxDownloadFile.Text;
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes("public " + message);
            clientSocket.Send(buffer); //send file request
            richTextBox_msg.AppendText("Request to make the file " + message + " public\n");
        }

        private void button2_Click(object sender, EventArgs e) // request file button
        { 
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes("List");
            clientSocket.Send(buffer); //send file request
            richTextBox_msg.AppendText("Request to retreive public file list." + "\n");
        }

        private void button3_Click(object sender, EventArgs e) // download button for public files
        {
            string message = textBoxDownloadFile.Text;
            downloadfile = message;
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes("downloadPublicFile " + downloadfile);
            clientSocket.Send(buffer); //send file request
            richTextBox_msg.AppendText("Request to download public file " + downloadfile + "\n");
        }

        private void textBoxDownloadDirectory_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
