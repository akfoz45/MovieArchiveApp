namespace MovieArchiveApp.Views
{
    partial class frmSignUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSignUp));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            frmSignUpPasswordTextBox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            frmSignUpUsernameTextBox = new TextBox();
            frmSignUpPasswordAgainTextBox = new TextBox();
            label4 = new Label();
            frmSignUpSignupBtn = new Button();
            frmSignUpGoBackBtn = new Button();
            SignUpShowHideButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(62, 67);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(350, 194);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(104, 18);
            label1.Name = "label1";
            label1.Size = new Size(277, 46);
            label1.TabIndex = 1;
            label1.Text = "Let's Get Started!";
            label1.Click += label1_Click;
            // 
            // frmSignUpPasswordTextBox
            // 
            frmSignUpPasswordTextBox.Location = new Point(170, 322);
            frmSignUpPasswordTextBox.Name = "frmSignUpPasswordTextBox";
            frmSignUpPasswordTextBox.PasswordChar = '●';
            frmSignUpPasswordTextBox.Size = new Size(183, 27);
            frmSignUpPasswordTextBox.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(68, 321);
            label3.Name = "label3";
            label3.Size = new Size(102, 28);
            label3.TabIndex = 8;
            label3.Text = "Password :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(62, 276);
            label2.Name = "label2";
            label2.Size = new Size(108, 28);
            label2.TabIndex = 7;
            label2.Text = "Username :";
            // 
            // frmSignUpUsernameTextBox
            // 
            frmSignUpUsernameTextBox.Location = new Point(170, 277);
            frmSignUpUsernameTextBox.Name = "frmSignUpUsernameTextBox";
            frmSignUpUsernameTextBox.Size = new Size(183, 27);
            frmSignUpUsernameTextBox.TabIndex = 6;
            // 
            // frmSignUpPasswordAgainTextBox
            // 
            frmSignUpPasswordAgainTextBox.Location = new Point(170, 361);
            frmSignUpPasswordAgainTextBox.Name = "frmSignUpPasswordAgainTextBox";
            frmSignUpPasswordAgainTextBox.PasswordChar = '●';
            frmSignUpPasswordAgainTextBox.Size = new Size(183, 27);
            frmSignUpPasswordAgainTextBox.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(12, 357);
            label4.Name = "label4";
            label4.Size = new Size(158, 28);
            label4.TabIndex = 10;
            label4.Text = "Password Again :";
            // 
            // frmSignUpSignupBtn
            // 
            frmSignUpSignupBtn.BackColor = Color.LimeGreen;
            frmSignUpSignupBtn.Location = new Point(208, 404);
            frmSignUpSignupBtn.Name = "frmSignUpSignupBtn";
            frmSignUpSignupBtn.Size = new Size(193, 66);
            frmSignUpSignupBtn.TabIndex = 12;
            frmSignUpSignupBtn.Text = "Create!";
            frmSignUpSignupBtn.UseVisualStyleBackColor = false;
            frmSignUpSignupBtn.Click += button1_Click;
            // 
            // frmSignUpGoBackBtn
            // 
            frmSignUpGoBackBtn.Location = new Point(41, 404);
            frmSignUpGoBackBtn.Name = "frmSignUpGoBackBtn";
            frmSignUpGoBackBtn.Size = new Size(112, 66);
            frmSignUpGoBackBtn.TabIndex = 13;
            frmSignUpGoBackBtn.Text = "Go Back";
            frmSignUpGoBackBtn.UseVisualStyleBackColor = true;
            frmSignUpGoBackBtn.Click += frmSignUpGoBackBtn_Click;
            // 
            // SignUpShowHideButton
            // 
            SignUpShowHideButton.Location = new Point(359, 358);
            SignUpShowHideButton.Name = "SignUpShowHideButton";
            SignUpShowHideButton.Size = new Size(59, 32);
            SignUpShowHideButton.TabIndex = 14;
            SignUpShowHideButton.Text = "Show";
            SignUpShowHideButton.UseVisualStyleBackColor = true;
            SignUpShowHideButton.Click += SignUpShowHideButton_Click;
            // 
            // frmSignUp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 504);
            Controls.Add(SignUpShowHideButton);
            Controls.Add(frmSignUpGoBackBtn);
            Controls.Add(frmSignUpSignupBtn);
            Controls.Add(frmSignUpPasswordAgainTextBox);
            Controls.Add(label4);
            Controls.Add(frmSignUpPasswordTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(frmSignUpUsernameTextBox);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "frmSignUp";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox frmSignUpPasswordTextBox;
        private Label label3;
        private Label label2;
        private TextBox frmSignUpUsernameTextBox;
        private TextBox frmSignUpPasswordAgainTextBox;
        private Label label4;
        private Button frmSignUpSignupBtn;
        private Button frmSignUpGoBackBtn;
        private Button SignUpShowHideButton;
    }
}