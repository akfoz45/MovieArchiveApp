namespace MovieArchiveApp.Views
{
    partial class frmMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenWatchList = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnOpenHome = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(888, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Çıkış Yap";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Movie Archive";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnOpenWatchList
            // 
            this.btnOpenWatchList.Location = new System.Drawing.Point(12, 90);
            this.btnOpenWatchList.Name = "btnOpenWatchList";
            this.btnOpenWatchList.Size = new System.Drawing.Size(150, 40);
            this.btnOpenWatchList.TabIndex = 2;
            this.btnOpenWatchList.Text = "İzleme Listem";
            this.btnOpenWatchList.UseVisualStyleBackColor = true;
            this.btnOpenWatchList.Click += new System.EventHandler(this.btnOpenWatchList_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.Location = new System.Drawing.Point(179, 57);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(809, 581);
            this.pnlContent.TabIndex = 3;
            // 
            // btnOpenHome
            // 
            this.btnOpenHome.Location = new System.Drawing.Point(12, 44);
            this.btnOpenHome.Name = "btnOpenHome";
            this.btnOpenHome.Size = new System.Drawing.Size(150, 40);
            this.btnOpenHome.TabIndex = 4;
            this.btnOpenHome.Text = "Ana Sayfa";
            this.btnOpenHome.UseVisualStyleBackColor = true;
            this.btnOpenHome.Click += new System.EventHandler(this.btnOpenHome_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.btnOpenHome);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.btnOpenWatchList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "frmMain";
            this.Text = "Movie Archive App";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenWatchList;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnOpenHome;
    }
}