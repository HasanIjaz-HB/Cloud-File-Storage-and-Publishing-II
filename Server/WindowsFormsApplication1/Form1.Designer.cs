namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_listen = new System.Windows.Forms.Button();
            this.btn_directory = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // btn_listen
            // 
            this.btn_listen.Location = new System.Drawing.Point(354, 51);
            this.btn_listen.Margin = new System.Windows.Forms.Padding(2);
            this.btn_listen.Name = "btn_listen";
            this.btn_listen.Size = new System.Drawing.Size(65, 27);
            this.btn_listen.TabIndex = 1;
            this.btn_listen.Text = "Listen";
            this.btn_listen.UseVisualStyleBackColor = true;
            this.btn_listen.Click += new System.EventHandler(this.btn_listen_Click);
            // 
            // btn_directory
            // 
            this.btn_directory.Enabled = false;
            this.btn_directory.Location = new System.Drawing.Point(76, 415);
            this.btn_directory.Margin = new System.Windows.Forms.Padding(2);
            this.btn_directory.Name = "btn_directory";
            this.btn_directory.Size = new System.Drawing.Size(110, 30);
            this.btn_directory.TabIndex = 2;
            this.btn_directory.Text = "Directory";
            this.btn_directory.UseVisualStyleBackColor = true;
            this.btn_directory.Click += new System.EventHandler(this.button2_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(70, 119);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(550, 223);
            this.logs.TabIndex = 3;
            this.logs.Text = "";
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(116, 55);
            this.textBox_message.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(216, 22);
            this.textBox_message.TabIndex = 4;
            this.textBox_message.TextChanged += new System.EventHandler(this.textBox_message_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.1F);
            this.label2.Location = new System.Drawing.Point(72, 373);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "File Path:";
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(180, 377);
            this.textBox_path.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(253, 22);
            this.textBox_path.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 545);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.btn_directory);
            this.Controls.Add(this.btn_listen);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_listen;
        private System.Windows.Forms.Button btn_directory;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

