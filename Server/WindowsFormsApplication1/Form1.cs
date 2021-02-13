using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        string dbpath = "";
        
        FileStream createDB;
        
        List<String> userNames = new List<String>();

        string username = "";
        bool terminating = false;
        bool listening = false;
        string folderName = "";
        byte[] m_serverData;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog(); 

            DialogResult result = folderBrowserDialog1.ShowDialog(); // opens the file browser and waits for the user to select file
            if (result == DialogResult.OK) 
            {   
                // if we succesfully select a file then we store directory
                folderName = folderBrowserDialog1.SelectedPath;
                textBox_path.Text = folderName;
                dbpath = Path.Combine(folderName, @"DataBase.txt");
                if(!File.Exists(folderName + "\\" + "DataBase.txt"))
                {
                    try
                    {
                        
                        createDB = new FileStream(dbpath, FileMode.Create);
                        createDB.Close(); // we changed the DB location to our file directory
                    }

                    catch
                    {
                        logs.AppendText("error creating database");
                    }
                }
            }

        }

        private void btn_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_message.Text, out serverPort)) // establishing server connection
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                btn_listen.Enabled = false;
                textBox_path.Enabled = true;
                btn_directory.Enabled = true;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }

        }

        private void Accept()
        {
            while (listening) 
            {
                try
                {
                    Socket newClient = serverSocket.Accept(); // establishing connection with client
                    clientSockets.Add(newClient);
                    logs.AppendText("A client is connected.\n");

                    Thread receiveThread = new Thread(() => Receive(newClient)); 
                    receiveThread.Start();
                    btn_directory.Enabled = true;
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient)
        {
            bool connected = true;

            try
            {
                Byte[] buffer = new Byte[64];
                thisClient.Receive(buffer);

                username = Encoding.Default.GetString(buffer); // get username from client
                username = username.Substring(0, username.IndexOf("\0"));
                logs.AppendText("Client: " + username + "\n");
                if (userNames.Contains(username)) // if the user is already connected
                {
                    logs.AppendText("Client already connected");
                    Byte[] buffer_2 = Encoding.Default.GetBytes("The username is already taken! Please try reconnecting again."); // error message
                    thisClient.Send(buffer_2);
                    thisClient.Close(); //reject connection
                }
                else // new user
                {
                    userNames.Add(username); // keep track of connected users

                    while (connected && !terminating)
                    {

                        Byte[] clientData = new Byte[1024 * 5000];
                        int receivedBytesLen = thisClient.Receive(clientData);

                        string incoming = Encoding.Default.GetString(clientData);
                        incoming = incoming.Substring(0, incoming.IndexOf("\0"));

                        if (incoming == "requestFile") // if incoming request is to request a file
                        {
                            logs.AppendText(incoming);
                            const Int32 BufSize = 128;
                            FileStream readDb = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read); // open database

                            using (var streamReader = new StreamReader(readDb, Encoding.UTF8, true, BufSize)) // read database
                            {
                                String line;
                                string user;
                                List<String> fileList = new List<String>();
                                while ((line = streamReader.ReadLine()) != null)
                                {
                                    user = line.Split(' ')[0]; //seperate username

                                    if (user == username)
                                    {
                                        string[] fname;
                                        string[] words;

                                        if (line.Count(x => x == '-') == 2) // if there are 3 files with same name
                                        {
                                            words = line.Split(' '); //seperate username
                                            fname = words[1].Split('-'); //seperate username from filename i.e user1 from user1-file.txt
                                            string num = fname[2];
                                            string root = fname[1].Split('.')[0]; // remove extension (.txt) to get the root of file name 
                                            fileList.Add(root + "-" + fname[2] + " " + words[2] + " bytes" + " " + words[3] + " " + words[4]);
                                        }
                                        else // if there are no duplicate files
                                        {
                                            // similar split to get root of filename
                                            words = line.Split(' ');
                                            string required = words[1];
                                            fname = required.Split('-');
                                            string root = fname[1].Split('.')[0];
                                            fileList.Add(root + ".txt" + " " + words[2] + " bytes" + " " + words[3] + " " + words[4]);
                                        }
                                    }
                                }

                                if (fileList.Count == 0) // user has not transfered anything yet!
                                {
                                    Byte[] buffer_3 = Encoding.Default.GetBytes("You have no files in the system");
                                    thisClient.Send(buffer_3);
                                }

                                foreach (var text in fileList)
                                {
                                    Byte[] buffer_3 = Encoding.Default.GetBytes(text);
                                    thisClient.Send(buffer_3);
                                    logs.AppendText(text + '\n');
                                }
                            }
                        }

                        else if (incoming.Contains("copyFile")) // if incoming request is to copy a file
                        {
                            string f_name = incoming.Split(' ')[1];
                            string tmpname = f_name;
                            
                            if(f_name.Contains('-')) // indicates a duplicate file e.g input-01.txt
                            {
                                f_name = f_name.Split('-')[0] + ".txt";
                            }

                            if(f_name != "")
                            f_name = username + "-" + f_name; // append the username of client to the f_name

                            if (tmpname != "" && File.Exists(folderName + "\\" + f_name))
                            {
                                logs.AppendText("Client " + username + ":File " + f_name + " copy request received." + "\n");
                                
                                const Int32 bSize = 128;
                                FileStream checkDB = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read); // open database
                                string fileStatus = "";
                                string user = "";

                                using (var sReader = new StreamReader(checkDB, Encoding.UTF8, true, bSize)) // read database to file status
                                {
                                    String dbline;
                                    while ((dbline = sReader.ReadLine()) != null)
                                    { 
                                        if (dbline.Contains(f_name))
                                        {
                                            user = dbline.Split(' ')[0]; // get the owner of the file

                                            if (dbline.Contains("public"))
                                            {
                                                fileStatus = "public";
                                            }
                                            else
                                            {
                                                fileStatus = "private";
                                            }
                                        }
                                    }
                                }

                                if (user == username) // only the owner can copy a file
                                {
                                    
                                        //get the directory name the file is in
                                        string sourceDirectory = folderName;

                                        //get the file name without extension
                                        string filenameWithoutExtension = Path.GetFileNameWithoutExtension(f_name);
                                        string temp = filenameWithoutExtension.Split('-')[1];

                                        //get the file extension
                                        string fileExtension = Path.GetExtension(f_name);

                                        int filecounter = 0; // keep track of duplicate files
                                        const Int32 BufferSize = 128;
                                        FileStream readDB = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read); // open database

                                        using (var streamReader = new StreamReader(readDB, Encoding.UTF8, true, BufferSize)) // read database
                                        {
                                            String line;
                                            while ((line = streamReader.ReadLine()) != null)
                                            {
                                                string[] fname;
                                                string[] words;

                                                if (line.Count(x => x == '-') == 2) // if there are 3 files with same name
                                                {
                                                    words = line.Split(' '); //seperate username
                                                    fname = words[1].Split('-'); //seperate username from filename i.e user1 from user1-file.txt
                                                    string root = fname[1].Split('.')[0]; // remove extension (.txt) to get the root of file name 

                                                    if (temp == root && username == words[0]) // if filenames and usernames are same
                                                    {
                                                        filecounter++;
                                                    }

                                                }
                                                else // if there are no duplicate files
                                                {
                                                    // similar split to get root of filename
                                                    words = line.Split(' ');
                                                    string required = words[1];
                                                    fname = required.Split('-');
                                                    string root = fname[1].Split('.')[0];

                                                    if (temp == root && username == words[0]) // if filenames and usernames are same
                                                    {
                                                        filecounter++;
                                                    }
                                                }

                                            }
                                        }

                                        //get the new file name you want,the Combine method is strongly recommended
                                        string destFileName = Path.Combine(sourceDirectory, filenameWithoutExtension + "-0" + filecounter + fileExtension);
                                        string sourceFileName = Path.Combine(sourceDirectory, f_name);

                                        File.Copy(sourceFileName, destFileName); // copy file
                                        Byte[] mybuffer = new Byte[64];
                                        mybuffer = Encoding.Default.GetBytes("File " + temp + fileExtension + " has been successfully copied");
                                        thisClient.Send(mybuffer); //send file request
                                        logs.AppendText("Client " + username + ":File " + f_name + " has been successfully copied" + "\n");

                                        FileInfo fi = new FileInfo(destFileName);
                                        long size = fi.Length;

                                        File.AppendAllText(dbpath, username + " " + filenameWithoutExtension + "-0" + filecounter + fileExtension + " " + size + " " + DateTime.Now + " " + fileStatus + Environment.NewLine); // write file details on database

                                        logs.AppendText("Added " + f_name + " to Database\n");
                                        readDB.Close();
                                    
                                }
                                    else
                                    {
                                            Byte[] mybuffer = new Byte[64];
                                            mybuffer = Encoding.Default.GetBytes("File copy request denied -> You do not own this file.\n");
                                            thisClient.Send(mybuffer); //send file request
                                    }
                                }
                                else
                                {
                                        Byte[] mybuffer = new Byte[64];
                                        mybuffer = Encoding.Default.GetBytes("File copy request denied -> File does not exist.\n");
                                        thisClient.Send(mybuffer); //send file request
                                }
                        }

                        else if (incoming.Contains("deleteFile")) // if incoming request is to delete a file
                        {
                            string f_name = incoming.Split(' ')[1]; // get the filename
                            string tmpname = f_name;
                            
                            
                             if (f_name != "")
                                f_name = username + "-" + f_name; // append the username of client to f_name
                            

                            if (tmpname != "" && File.Exists(folderName + "\\" + f_name))
                            {
                                logs.AppendText("Client " + username + ":File " + f_name + " delete request received." + "\n");

                                const Int32 bSize = 128;
                                FileStream checkDB = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read); // open database
                                string user = "";

                                using (var sReader = new StreamReader(checkDB, Encoding.UTF8, true, bSize)) // read database to get username
                                {
                                    String dbline;
                                    while ((dbline = sReader.ReadLine()) != null)
                                    {
                                        if (dbline.Contains(f_name))
                                        {
                                            user = dbline.Split(' ')[0]; // get the owner of the file
                                        }
                                    }
                                }

                                if (user == username) // only the owner can delete a file
                                {
                                    string sourceDirectory = folderName;//get the directory name the file is in
                                    string filenameWithoutExtension = Path.GetFileNameWithoutExtension(tmpname);
                                    File.Delete(Path.Combine(sourceDirectory, f_name)); // delete file

                                    var tempFile = Path.GetTempFileName();
                                    var linesToKeep = File.ReadLines(folderName + "\\" + "DataBase.txt").Where(l => !l.Contains(f_name));

                                    File.WriteAllLines(tempFile, linesToKeep);

                                    File.Delete(folderName + "\\" + "DataBase.txt"); 
                                    File.Move(tempFile, folderName + "\\" + "DataBase.txt");

                                    logs.AppendText("Deleted " + f_name + " from Database\n");
                                    }
                                    
                                else
                                {
                                    Byte[] mybuffer = new Byte[64];
                                    mybuffer = Encoding.Default.GetBytes("File delete request denied -> You do not own this file.\n");
                                    thisClient.Send(mybuffer); //send file request
                                }
                            }
                            else
                            {
                                Byte[] mybuffer = new Byte[64];
                                mybuffer = Encoding.Default.GetBytes("File delete request denied -> File does not exist. \n");
                                thisClient.Send(mybuffer); //send file request
                            }
                        }

                        else if (incoming.Contains("downloadOwnFile")) // if incoming request is to download an owned file
                        {
                            try
                            {
                                string f_name = incoming.Split(' ')[1]; // get file name
                                string tmpname = f_name;
                                
                                   if (f_name != "")
                                    f_name = username + "-" + f_name; // append the username of client to f_name
                                
                                if (tmpname != "")
                                {
                                    try
                                    {
                                        logs.AppendText("Client " + username + ": File " + f_name + " download request received." + "\n");

                                        logs.AppendText("Download " + f_name + " from Database\n");

                                        bool check = false;
                                        const Int32 bSize = 128;
                                        FileStream checkDB = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read); // open database
                                        string fileStatus = "";
                                        string user = "";

                                        using (var sReader = new StreamReader(checkDB, Encoding.UTF8, true, bSize)) // read database to get file status
                                        {
                                            String dbline;
                                            while ((dbline = sReader.ReadLine()) != null)
                                            {
                                                if (dbline.Contains(f_name))
                                                {
                                                    check = true;
                                                    user = dbline.Split(' ')[0];

                                                    if (dbline.Contains("public"))
                                                    {
                                                        fileStatus = "public";
                                                    }
                                                    else
                                                    {
                                                        fileStatus = "private";
                                                    }
                                                }
                                            }
                                        }

                                        if (check && (user == username || fileStatus == "public")) // the owner can download their own file
                                        { 
                                        byte[] fileNameByte = Encoding.ASCII.GetBytes(tmpname);
                                        byte[] fileData = File.ReadAllBytes(folderName + "\\" + f_name);
                                        byte[] downloadData = new byte[4 + fileNameByte.Length + fileData.Length];
                                        byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                         
                                        fileNameLen.CopyTo(downloadData, 0);
                                        fileNameByte.CopyTo(downloadData, 4);
                                        fileData.CopyTo(downloadData, 4 + fileNameByte.Length);

                                        Byte[] mybuffer = new Byte[64];
                                        mybuffer = Encoding.Default.GetBytes("Download request approved. Downloading file...");
                                        thisClient.Send(mybuffer); //send file request

                                        thisClient.Send(downloadData); // download operation is implemented in a similar way to the transfer/upload operation

                                        }
                                           
                                        else if(!check)
                                        {
                                            Byte[] mybuffer = new Byte[64];
                                            mybuffer = Encoding.Default.GetBytes("File does not exist.");
                                            thisClient.Send(mybuffer); //send file request

                                        }

                                        else
                                        {
                                            Byte[] mybuffer = new Byte[64];
                                            mybuffer = Encoding.Default.GetBytes("File download request denied -> The requested file is private\n");
                                            thisClient.Send(mybuffer); //send file request
                                        }

                                    }
                                    catch (Exception ex) //fail to send
                                    {
                                        logs.AppendText("\n" + ex.Message);
                                    }
                                }

                            }
                            catch (Exception ex) //fail to send
                            {
                                logs.AppendText("\n" + ex.Message);
                            }
                        }


                        else if (incoming.Contains("downloadPublicFile")) //if incoming request is to download a public file
                        {
                            try
                            {
                                string f_name = incoming.Split(' ')[1]; // get file name
                                string tmpname = f_name;

                                if (tmpname != "")
                                {
                                    try
                                    {
                                        logs.AppendText("Client " + username + ": File " + f_name + " download request received." + "\n");
                                        //get the directory name the file is in

                                        logs.AppendText("Download " + f_name + " from Database\n");

                                        bool check = false;
                                        const Int32 bSize = 128;
                                        FileStream checkDB = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read); // open database
                                        string fileStatus = "";
                                        string user = "";

                                        using (var sReader = new StreamReader(checkDB, Encoding.UTF8, true, bSize)) // read database to get file status
                                        {
                                            String dbline;
                                            while ((dbline = sReader.ReadLine()) != null)
                                            {
                                                if (dbline.Contains(f_name))
                                                {
                                                    check = true;
                                                    user = dbline.Split(' ')[0];

                                                    if (dbline.Contains("public"))
                                                    {
                                                        fileStatus = "public";
                                                    }
                                                    else
                                                    {
                                                        fileStatus = "private";
                                                    }
                                                }
                                            }
                                        }

                                        if (check && (user == username || fileStatus == "public")) // check if the requested file is public
                                        {
                                            byte[] fileNameByte = Encoding.ASCII.GetBytes(tmpname);
                                            byte[] fileData = File.ReadAllBytes(folderName + "\\" + f_name);
                                            byte[] downloadData = new byte[4 + fileNameByte.Length + fileData.Length];
                                            byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                                            fileNameLen.CopyTo(downloadData, 0);
                                            fileNameByte.CopyTo(downloadData, 4);
                                            fileData.CopyTo(downloadData, 4 + fileNameByte.Length);

                                            Byte[] mybuffer = new Byte[64];
                                            mybuffer = Encoding.Default.GetBytes("Download request approved. Downloading file...");
                                            thisClient.Send(mybuffer); //send file request

                                            thisClient.Send(downloadData); // download operation is implemented in a similar way to the transfer/upload operation

                                        }

                                        else if (!check)
                                        {
                                            Byte[] mybuffer = new Byte[64];
                                            mybuffer = Encoding.Default.GetBytes("File does not exist.");
                                            thisClient.Send(mybuffer); //send file request

                                        }

                                        else
                                        {
                                            Byte[] mybuffer = new Byte[64];
                                            mybuffer = Encoding.Default.GetBytes("File download request denied -> The requested file is private\n");
                                            thisClient.Send(mybuffer); //send file request
                                        }

                                    }
                                    catch (Exception ex) //fail to send
                                    {
                                        logs.AppendText("\n" + ex.Message);
                                    }
                                }

                            }
                            catch (Exception ex) //fail to send
                            {
                                logs.AppendText("\n" + ex.Message);
                            }
                        }

                        else if (incoming.Contains("public")) // if the incoming request is to make a file public
                        {
                            string f_name = incoming.Split(' ')[1]; // get file name
                            string tmpname = f_name;

                            
                            if (f_name != "")
                                f_name = username + "-" + f_name; // append the username of client to the f_name
                            

                            if (tmpname != "" && File.Exists(folderName + "\\" + f_name))
                            {
                                logs.AppendText("Client " + username + ": request to make file " + f_name + " public received." + "\n");

                                const Int32 bSize = 128;
                                FileStream checkDB = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read); // open database
                                string user = "";

                                using (var sReader = new StreamReader(checkDB, Encoding.UTF8, true, bSize)) // read database to get username
                                {
                                    String dbline;
                                    while ((dbline = sReader.ReadLine()) != null)
                                    {
                                        if (dbline.Contains(f_name))
                                        {
                                            user = dbline.Split(' ')[0]; // get the owner of the file
                                        }
                                    }
                                }

                                if (user == username) // only the owner can make a file public
                                {
                                    //get the directory name the file is in
                                    string sourceDirectory = folderName;
                                    string filenameWithoutExtension = Path.GetFileNameWithoutExtension(tmpname);

                                    string text = "";
                                    using (StreamReader sr = new StreamReader(folderName + "\\" + "DataBase.txt"))
                                    {
                                        int i = 0;
                                        do
                                        {
                                            i++;
                                            string line = sr.ReadLine();
                                            if (line.Contains(f_name))
                                            {
                                                line = line.Replace("private", "public"); // make file public

                                            }
                                            text = text + line + Environment.NewLine;
                                        } while (sr.EndOfStream == false);
                                    }
                                    File.WriteAllText(folderName + "\\DataBase.txt", text);
                                    Byte[] mybuffer = new Byte[64];
                                    mybuffer = Encoding.Default.GetBytes("The file has been made public.\n");
                                    thisClient.Send(mybuffer); //send file request

                                }
                                else
                                {
                                    Byte[] mybuffer = new Byte[64];
                                    mybuffer = Encoding.Default.GetBytes("Request to make file public denied -> You do not own this file.\n");
                                    thisClient.Send(mybuffer); //send file request
                                }
                            }
                            else
                            {
                                Byte[] mybuffer = new Byte[64];
                                mybuffer = Encoding.Default.GetBytes("Request to make file public denied -> File does not exist.\n");
                                thisClient.Send(mybuffer); //send file request
                            }

                        }

                        else if (incoming.Contains("List")) // if the incoming request is to display public file list
                        {
                            List<String> fileList = new List<String>();
                            using (StreamReader sr = new StreamReader(folderName + "\\" + "DataBase.txt"))
                            {
                               
                                int i = 0;
                                do
                                {
                                    i++;
                                    string line = sr.ReadLine();
                                    if (line.Contains("public")) // find the public files
                                    {

                                        string[] fname;
                                        string[] words;

                                        if (line.Count(x => x == '-') == 2) // if there are 3 files with same name
                                        {
                                            words = line.Split(' '); //seperate username
                                            fname = words[1].Split('-'); //seperate username from filename i.e user1 from user1-file.txt
                                            string num = fname[2];
                                            string root = fname[1].Split('.')[0]; // remove extension (.txt) to get the root of file name 
                                            fileList.Add(words[0] + " " + root + "-" + fname[2] + " " + words[2] + " bytes" + " " + words[3] + " " + words[4]);
                                        }
                                        else // if there are no duplicate files
                                        {
                                            // similar split to get root of filename
                                            words = line.Split(' ');
                                            string required = words[1];
                                            fname = required.Split('-');
                                            string root = fname[1].Split('.')[0];
                                            fileList.Add(words[0] + " " + root + ".txt" + " " + words[2] + " bytes" + " " + words[3] + " " + words[4]); // add file details to the file list
                                        }


                                    }

                                } while (sr.EndOfStream == false);
                            }
                            if (fileList.Count == 0)
                            {
                                Byte[] buffer_3 = Encoding.Default.GetBytes("You have no files in the system");
                                thisClient.Send(buffer_3);
                            }

                            foreach (string text in fileList) // display public file list on client chat
                            {
                                Byte[] buffer_3 = Encoding.Default.GetBytes(text);
                                thisClient.Send(buffer_3);
                                logs.AppendText(text + '\n');
                            }
                        }
                        else // the client wants to transfer a file
                        {

                            int fileNameLen = BitConverter.ToInt32(clientData, 0);
                            string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
                            string[] arr = fileName.Split('.');
                            string temp = arr[0]; // root of file name i.e "file" of file.txt
                            fileName = username + "-" + fileName; // combine username with filename to add to database

                            try
                            {
                                int filecounter = 0; // keep track of duplicate files
                                const Int32 BufferSize = 128;
                                FileStream readDB = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read); // open database

                                using (var streamReader = new StreamReader(readDB, Encoding.UTF8, true, BufferSize)) // read database
                                {
                                    String line;
                                    while ((line = streamReader.ReadLine()) != null)
                                    {
                                        string[] fname;
                                        string[] words;

                                        if (line.Count(x => x == '-') == 2) // if there are 3 files with same name
                                        {
                                            words = line.Split(' '); //seperate username
                                            fname = words[1].Split('-'); //seperate username from filename i.e user1 from user1-file.txt
                                            string root = fname[1].Split('.')[0]; // remove extension (.txt) to get the root of file name 

                                            if (temp == root && username == words[0]) // if filenames and usernames are same
                                            {
                                                filecounter++;
                                            }

                                        }
                                        else // if there are no duplicate files
                                        {
                                            // similar split to get root of filename
                                            words = line.Split(' ');
                                            string required = words[1];
                                            fname = required.Split('-');
                                            string root = fname[1].Split('.')[0];

                                            if (temp == root && username == words[0]) // if filenames and usernames are same
                                            {
                                                filecounter++;
                                            }
                                        }

                                    }

                                }
                                if (filecounter != 0) //if we found duplicate files
                                {
                                    if (filecounter < 10)
                                    {
                                        fileName = username + "-" + temp + "-0" + filecounter + ".txt"; // if <10 duplicates add 0 to beginning!
                                    }
                                    else

                                        fileName = username + "-" + temp + "-" + filecounter + ".txt"; // if >10 duplicates no 0 needed to be added!
                                }

                                BinaryWriter bwrite = new BinaryWriter(File.Open(folderName + "/" + fileName, FileMode.Append)); // save file
                                bwrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
                                logs.AppendText("saving file... " + fileName + "\n"); // inform user
                                bwrite.Close();


                                FileInfo fi = new FileInfo(folderName + "/" + fileName);
                                Byte[] title = new UTF8Encoding(true).GetBytes((username + " " + fileName + "\n"));
                                long size = fi.Length;

                                File.AppendAllText(dbpath, username + " " + fileName + " " + size + " " + DateTime.Now + " private " + Environment.NewLine); // write file details on database

                                logs.AppendText("Added " + fileName + " to Database\n");
                                readDB.Close();
                            }
                            catch (Exception e)
                            {
                                if (!terminating)
                                {
                                    logs.AppendText("A client has disconnected\n");

                                    e.ToString();
                                }
                                thisClient.Close();
                                clientSockets.Remove(thisClient);
                                connected = false;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                if (!terminating)
                {
                    logs.AppendText("A client has disconnected\n");
                    ex.ToString();
                }
                thisClient.Close();
                clientSockets.Remove(thisClient);
                userNames.Remove(username);
                connected = false;
            }
        }


        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void textBox_message_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
