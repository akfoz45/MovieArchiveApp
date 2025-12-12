namespace MovieArchiveApp.Views
{
    partial class frmWatchList
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
            dgvList = new DataGridView();
            btnRemove = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvList).BeginInit();
            SuspendLayout();
            // 
            // dgvList
            // 
            dgvList.AllowUserToAddRows = false;
            dgvList.AllowUserToDeleteRows = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.BackgroundColor = Color.White;
            dgvList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvList.Dock = DockStyle.Left;
            dgvList.Location = new Point(0, 0);
            dgvList.Name = "dgvList";
            dgvList.ReadOnly = true;
            dgvList.RowHeadersWidth = 51;
            dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvList.Size = new Size(469, 591);
            dgvList.TabIndex = 0;
            dgvList.CellContentClick += dgvList_CellContentClick;
            // 
            // btnRemove
            // 
            btnRemove.Anchor = AnchorStyles.None;
            btnRemove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRemove.Location = new Point(521, 270);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(169, 62);
            btnRemove.TabIndex = 1;
            btnRemove.Text = "Listeden Kaldır";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // frmWatchList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(724, 591);
            Controls.Add(btnRemove);
            Controls.Add(dgvList);
            Name = "frmWatchList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmWatchList";
            ((System.ComponentModel.ISupportInitialize)dgvList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvList;
        private Button btnRemove;
    }
}