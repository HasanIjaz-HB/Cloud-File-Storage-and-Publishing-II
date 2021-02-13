namespace Client
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_IP = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label_Port = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.button_Connect = new System.Windows.Forms.Button();
            this.richTextBox_msg = new System.Windows.Forms.RichTextBox();
            this.label_uname = new System.Windows.Forms.Label();
            this.textBox_uname = new System.Windows.Forms.TextBox();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.label_file = new System.Windows.Forms.Label();
            this.button_transfer = new System.Windows.Forms.Button();
            this.requestfile = new System.Windows.Forms.Button();
            this.downloadDirectory = new System.Windows.Forms.Label();
            this.textBoxDownloadDirectory = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.download = new System.Windows.Forms.Button();
            this.browseDownload = new System.Windows.Forms.Button();
            this.downloadFile = new System.Windows.Forms.Label();
            this.textBoxDownloadFile = new System.Windows.Forms.TextBox();
            this.copy = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.publicButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(60, 33);
            this.label_IP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(20, 13);
            this.label_IP.TabIndex = 0;
            this.label_IP.Text = "IP:";
            this.label_IP.Click += new System.EventHandler(this.label_IP_Click);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(82, 31);
            this.textBox_IP.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(188, 20);
            this.textBox_IP.TabIndex = 1;
            this.textBox_IP.TextChanged += new System.EventHandler(this.textBox_IP_TextChanged);
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(50, 65);
            this.label_Port.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(29, 13);
            this.label_Port.TabIndex = 2;
            this.label_Port.Text = "Port:";
            this.label_Port.Click += new System.EventHandler(this.label_Port_Click);
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(82, 63);
            this.textBox_Port.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(188, 20);
            this.textBox_Port.TabIndex = 3;
            this.textBox_Port.TextChanged += new System.EventHandler(this.textBox_Port_TextChanged);
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(82, 117);
            this.button_Connect.Margin = new System.Windows.Forms.Padding(2);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(73, 23);
            this.button_Connect.TabIndex = 4;
            this.button_Connect.Text = "Connect";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // richTextBox_msg
            // 
            this.richTextBox_msg.Location = new System.Drawing.Point(313, 31);
            this.richTextBox_msg.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox_msg.Name = "richTextBox_msg";
            this.richTextBox_msg.Size = new System.Drawing.Size(308, 370);
            this.richTextBox_msg.TabIndex = 5;
            this.richTextBox_msg.Text = "";
            this.richTextBox_msg.TextChanged += new System.EventHandler(this.richTextBox_msg_TextChanged);
            // 
            // label_uname
            // 
            this.label_uname.AutoSize = true;
            this.label_uname.Location = new System.Drawing.Point(20, 96);
            this.label_uname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_uname.Name = "label_uname";
            this.label_uname.Size = new System.Drawing.Size(58, 13);
            this.label_uname.TabIndex = 6;
            this.label_uname.Text = "Username:";
            this.label_uname.Click += new System.EventHandler(this.label_uname_Click);
            // 
            // textBox_uname
            // 
            this.textBox_uname.Location = new System.Drawing.Point(82, 93);
            this.textBox_uname.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_uname.Name = "textBox_uname";
            this.textBox_uname.Size = new System.Drawing.Size(188, 20);
            this.textBox_uname.TabIndex = 7;
            this.textBox_uname.TextChanged += new System.EventHandler(this.textBox_uname_TextChanged);
            // 
            // button_Browse
            // 
            this.button_Browse.Enabled = false;
            this.button_Browse.Location = new System.Drawing.Point(82, 187);
            this.button_Browse.Margin = new System.Windows.Forms.Padding(2);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(73, 23);
            this.button_Browse.TabIndex = 8;
            this.button_Browse.Text = "Browse";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            // 
            // textBox_file
            // 
            this.textBox_file.Location = new System.Drawing.Point(82, 154);
            this.textBox_file.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.Size = new System.Drawing.Size(188, 20);
            this.textBox_file.TabIndex = 9;
            this.textBox_file.TextChanged += new System.EventHandler(this.textBox_file_TextChanged);
            // 
            // label_file
            // 
            this.label_file.AutoSize = true;
            this.label_file.Location = new System.Drawing.Point(50, 157);
            this.label_file.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_file.Name = "label_file";
            this.label_file.Size = new System.Drawing.Size(26, 13);
            this.label_file.TabIndex = 10;
            this.label_file.Text = "File:";
            this.label_file.Click += new System.EventHandler(this.label_file_Click);
            // 
            // button_transfer
            // 
            this.button_transfer.Enabled = false;
            this.button_transfer.Location = new System.Drawing.Point(197, 187);
            this.button_transfer.Margin = new System.Windows.Forms.Padding(2);
            this.button_transfer.Name = "button_transfer";
            this.button_transfer.Size = new System.Drawing.Size(73, 23);
            this.button_transfer.TabIndex = 11;
            this.button_transfer.Text = "Transfer";
            this.button_transfer.UseVisualStyleBackColor = true;
            this.button_transfer.Click += new System.EventHandler(this.button_transfer_Click);
            // 
            // requestfile
            // 
            this.requestfile.Location = new System.Drawing.Point(125, 214);
            this.requestfile.Margin = new System.Windows.Forms.Padding(2);
            this.requestfile.Name = "requestfile";
            this.requestfile.Size = new System.Drawing.Size(101, 23);
            this.requestfile.TabIndex = 12;
            this.requestfile.Text = "Request File";
            this.requestfile.UseVisualStyleBackColor = true;
            this.requestfile.Click += new System.EventHandler(this.requestfile_Click);
            // 
            // downloadDirectory
            // 
            this.downloadDirectory.AutoSize = true;
            this.downloadDirectory.Location = new System.Drawing.Point(24, 249);
            this.downloadDirectory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.downloadDirectory.Name = "downloadDirectory";
            this.downloadDirectory.Size = new System.Drawing.Size(52, 13);
            this.downloadDirectory.TabIndex = 13;
            this.downloadDirectory.Text = "Directory:";
            this.downloadDirectory.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBoxDownloadDirectory
            // 
            this.textBoxDownloadDirectory.Location = new System.Drawing.Point(82, 246);
            this.textBoxDownloadDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDownloadDirectory.Name = "textBoxDownloadDirectory";
            this.textBoxDownloadDirectory.Size = new System.Drawing.Size(188, 20);
            this.textBoxDownloadDirectory.TabIndex = 14;
            this.textBoxDownloadDirectory.TextChanged += new System.EventHandler(this.textBoxDownloadDirectory_TextChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(197, 117);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Disconnect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // download
            // 
            this.download.Enabled = false;
            this.download.Location = new System.Drawing.Point(197, 326);
            this.download.Margin = new System.Windows.Forms.Padding(2);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(73, 23);
            this.download.TabIndex = 16;
            this.download.Text = "Download ";
            this.download.UseVisualStyleBackColor = true;
            this.download.Click += new System.EventHandler(this.download_Click);
            // 
            // browseDownload
            // 
            this.browseDownload.Location = new System.Drawing.Point(82, 270);
            this.browseDownload.Margin = new System.Windows.Forms.Padding(2);
            this.browseDownload.Name = "browseDownload";
            this.browseDownload.Size = new System.Drawing.Size(73, 23);
            this.browseDownload.TabIndex = 17;
            this.browseDownload.Text = "Browse";
            this.browseDownload.UseVisualStyleBackColor = true;
            this.browseDownload.Click += new System.EventHandler(this.browseDownload_Click);
            // 
            // downloadFile
            // 
            this.downloadFile.AutoSize = true;
            this.downloadFile.Location = new System.Drawing.Point(20, 302);
            this.downloadFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.downloadFile.Name = "downloadFile";
            this.downloadFile.Size = new System.Drawing.Size(55, 13);
            this.downloadFile.TabIndex = 18;
            this.downloadFile.Text = "File name:";
            // 
            // textBoxDownloadFile
            // 
            this.textBoxDownloadFile.Location = new System.Drawing.Point(82, 302);
            this.textBoxDownloadFile.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDownloadFile.Name = "textBoxDownloadFile";
            this.textBoxDownloadFile.Size = new System.Drawing.Size(188, 20);
            this.textBoxDownloadFile.TabIndex = 19;
            // 
            // copy
            // 
            this.copy.Location = new System.Drawing.Point(82, 326);
            this.copy.Margin = new System.Windows.Forms.Padding(2);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(73, 23);
            this.copy.TabIndex = 20;
            this.copy.Text = "Copy";
            this.copy.UseVisualStyleBackColor = true;
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(82, 353);
            this.delete.Margin = new System.Windows.Forms.Padding(2);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(73, 23);
            this.delete.TabIndex = 21;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // publicButton
            // 
            this.publicButton.Location = new System.Drawing.Point(197, 353);
            this.publicButton.Margin = new System.Windows.Forms.Padding(2);
            this.publicButton.Name = "publicButton";
            this.publicButton.Size = new System.Drawing.Size(73, 23);
            this.publicButton.TabIndex = 22;
            this.publicButton.Text = "Public";
            this.publicButton.UseVisualStyleBackColor = true;
            this.publicButton.Click += new System.EventHandler(this.publicButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(82, 378);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Public File List";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(197, 380);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "Download Public";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 438);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.publicButton);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.copy);
            this.Controls.Add(this.textBoxDownloadFile);
            this.Controls.Add(this.downloadFile);
            this.Controls.Add(this.browseDownload);
            this.Controls.Add(this.download);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxDownloadDirectory);
            this.Controls.Add(this.downloadDirectory);
            this.Controls.Add(this.requestfile);
            this.Controls.Add(this.button_transfer);
            this.Controls.Add(this.label_file);
            this.Controls.Add(this.textBox_file);
            this.Controls.Add(this.button_Browse);
            this.Controls.Add(this.textBox_uname);
            this.Controls.Add(this.label_uname);
            this.Controls.Add(this.richTextBox_msg);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.label_IP);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Client";
            this.Text = "Client App";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.RichTextBox richTextBox_msg;
        private System.Windows.Forms.Label label_uname;
        private System.Windows.Forms.TextBox textBox_uname;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.TextBox textBox_file;
        private System.Windows.Forms.Label label_file;
        private System.Windows.Forms.Button button_transfer;
        private System.Windows.Forms.Button requestfile;
        private System.Windows.Forms.Label downloadDirectory;
        private System.Windows.Forms.TextBox textBoxDownloadDirectory;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button download;
        private System.Windows.Forms.Button browseDownload;
        private System.Windows.Forms.Label downloadFile;
        private System.Windows.Forms.TextBox textBoxDownloadFile;
        private System.Windows.Forms.Button copy;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button publicButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

