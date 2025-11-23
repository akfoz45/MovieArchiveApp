namespace MovieArchiveApp
{
    partial class frmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            frmLoginUsernameTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            frmLoginPasswordTextBox = new TextBox();
            frmLoginGoToSignIn = new LinkLabel();
            LogInButton = new Button();
            LogInShowHideButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(52, 81);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(361, 192);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(173, 21);
            label1.Name = "label1";
            label1.Size = new Size(139, 46);
            label1.TabIndex = 1;
            label1.Text = "Enjoy it!";
            label1.Click += label1_Click;
            // 
            // frmLoginUsernameTextBox
            // 
            frmLoginUsernameTextBox.Location = new Point(160, 294);
            frmLoginUsernameTextBox.Name = "frmLoginUsernameTextBox";
            frmLoginUsernameTextBox.Size = new Size(183, 27);
            frmLoginUsernameTextBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(52, 293);
            label2.Name = "label2";
            label2.Size = new Size(108, 28);
            label2.TabIndex = 3;
            label2.Text = "Username :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(58, 338);
            label3.Name = "label3";
            label3.Size = new Size(102, 28);
            label3.TabIndex = 4;
            label3.Text = "Password :";
            // 
            // frmLoginPasswordTextBox
            // 
            frmLoginPasswordTextBox.Location = new Point(160, 339);
            frmLoginPasswordTextBox.Name = "frmLoginPasswordTextBox";
            frmLoginPasswordTextBox.PasswordChar = '●';
            frmLoginPasswordTextBox.Size = new Size(183, 27);
            frmLoginPasswordTextBox.TabIndex = 5;
            frmLoginPasswordTextBox.TextChanged += frmLoginPasswordTextBox_TextChanged;
            // 
            // frmLoginGoToSignIn
            // 
            frmLoginGoToSignIn.AutoSize = true;
            frmLoginGoToSignIn.Font = new Font("Segoe UI", 15F);
            frmLoginGoToSignIn.Location = new Point(115, 460);
            frmLoginGoToSignIn.Name = "frmLoginGoToSignIn";
            frmLoginGoToSignIn.Size = new Size(275, 35);
            frmLoginGoToSignIn.TabIndex = 6;
            frmLoginGoToSignIn.TabStop = true;
            frmLoginGoToSignIn.Text = "Don't have an account?";
            frmLoginGoToSignIn.LinkClicked += frmLoginGoToSignIn_LinkClicked;
            // 
            // LogInButton
            // 
            LogInButton.BackColor = Color.MediumSeaGreen;
            LogInButton.Location = new Point(160, 385);
            LogInButton.Name = "LogInButton";
            LogInButton.Size = new Size(183, 60);
            LogInButton.TabIndex = 7;
            LogInButton.Text = "Log in";
            LogInButton.UseVisualStyleBackColor = false;
            LogInButton.Click += LogInButton_Click;
            // 
            // LogInShowHideButton
            // 
            LogInShowHideButton.Location = new Point(349, 336);
            LogInShowHideButton.Name = "LogInShowHideButton";
            LogInShowHideButton.Size = new Size(59, 32);
            LogInShowHideButton.TabIndex = 8;
            LogInShowHideButton.Text = "Show";
            LogInShowHideButton.UseVisualStyleBackColor = true;
            LogInShowHideButton.Click += ShowHideButton_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(488, 504);
            Controls.Add(LogInShowHideButton);
            Controls.Add(LogInButton);
            Controls.Add(frmLoginGoToSignIn);
            Controls.Add(frmLoginPasswordTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(frmLoginUsernameTextBox);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "frmLogin";
            Text = "Form1";
            Load += frmLogin_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox frmLoginUsernameTextBox;
        private Label label2;
        private Label label3;
        private TextBox frmLoginPasswordTextBox;
        private LinkLabel frmLoginGoToSignIn;
        private Button LogInButton;
        private Button LogInShowHideButton;
    }
}
