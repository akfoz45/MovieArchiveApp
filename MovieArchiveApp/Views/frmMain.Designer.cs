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
            frmMainLogOut = new Button();
            label1 = new Label();
            btnOpenWatchList = new Button();
            SuspendLayout();
            // 
            // frmMainLogOut
            // 
            frmMainLogOut.BackColor = Color.Red;
            frmMainLogOut.Location = new Point(928, 12);
            frmMainLogOut.Name = "frmMainLogOut";
            frmMainLogOut.Size = new Size(200, 83);
            frmMainLogOut.TabIndex = 0;
            frmMainLogOut.Text = "Log out";
            frmMainLogOut.UseVisualStyleBackColor = false;
            frmMainLogOut.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 50F);
            label1.Location = new Point(318, 216);
            label1.Name = "label1";
            label1.Size = new Size(436, 112);
            label1.TabIndex = 1;
            label1.Text = "Main Page";
            label1.Click += label1_Click;
            // 
            // btnOpenWatchList
            // 
            btnOpenWatchList.BackColor = Color.PaleTurquoise;
            btnOpenWatchList.Font = new Font("Calibri", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnOpenWatchList.ForeColor = Color.DarkSlateGray;
            btnOpenWatchList.Location = new Point(14, 12);
            btnOpenWatchList.Name = "btnOpenWatchList";
            btnOpenWatchList.Size = new Size(151, 52);
            btnOpenWatchList.TabIndex = 2;
            btnOpenWatchList.Text = "İzleme Listem";
            btnOpenWatchList.UseVisualStyleBackColor = false;
            btnOpenWatchList.Click += btnOpenWatchList_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1142, 558);
            Controls.Add(btnOpenWatchList);
            Controls.Add(label1);
            Controls.Add(frmMainLogOut);
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMain";
            Load += frmMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button frmMainLogOut;
        private Label label1;
        private Button btnOpenWatchList;
    }
}