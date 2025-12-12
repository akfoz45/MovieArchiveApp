namespace MovieArchiveApp.Views
{
    partial class frmMovieDetail
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDirector = new System.Windows.Forms.Label();
            this.lblGenre = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnWatchList = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Film Adı";
            // 
            // lblDirector
            // 
            this.lblDirector.AutoSize = true;
            this.lblDirector.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDirector.Location = new System.Drawing.Point(30, 60);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(73, 19);
            this.lblDirector.TabIndex = 1;
            this.lblDirector.Text = "Yönetmen:";
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGenre.Location = new System.Drawing.Point(30, 90);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(31, 19);
            this.lblGenre.TabIndex = 2;
            this.lblGenre.Text = "Tür:";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblYear.Location = new System.Drawing.Point(30, 120);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(26, 19);
            this.lblYear.TabIndex = 3;
            this.lblYear.Text = "Yıl:";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(30, 160);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(600, 140);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.Text = "Film açıklaması burada yer alacak...";
            // 
            // btnWatchList
            // 
            this.btnWatchList.Location = new System.Drawing.Point(380, 310);
            this.btnWatchList.Name = "btnWatchList";
            this.btnWatchList.Size = new System.Drawing.Size(150, 40);
            this.btnWatchList.TabIndex = 5;
            this.btnWatchList.Text = "İzleme Listesine Ekle";
            this.btnWatchList.UseVisualStyleBackColor = true;
            this.btnWatchList.Click += new System.EventHandler(this.btnWatchList_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(540, 310);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 40);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Kapat";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMovieDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 370);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnWatchList);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.lblDirector);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMovieDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Film Detayı";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDirector;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnWatchList;
        private System.Windows.Forms.Button btnClose;
    }
}