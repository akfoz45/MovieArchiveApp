namespace MovieArchiveApp.Views
{
    partial class frmHome
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
            dgvMovies = new DataGridView();
            btnAddMovie = new Button();
            btnEditMovie = new Button();
            btnDeleteMovie = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMovies).BeginInit();
            SuspendLayout();
            // 
            // dgvMovies
            // 
            dgvMovies.AllowUserToAddRows = false;
            dgvMovies.AllowUserToDeleteRows = false;
            dgvMovies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMovies.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvMovies.Location = new Point(14, 13);
            dgvMovies.Margin = new Padding(3, 4, 3, 4);
            dgvMovies.MultiSelect = false;
            dgvMovies.Name = "dgvMovies";
            dgvMovies.ReadOnly = true;
            dgvMovies.RowHeadersWidth = 51;
            dgvMovies.RowTemplate.Height = 25;
            dgvMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovies.Size = new Size(887, 521);
            dgvMovies.TabIndex = 3;
            // 
            // btnAddMovie
            // 
            btnAddMovie.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddMovie.Location = new Point(14, 541);
            btnAddMovie.Margin = new Padding(3, 4, 3, 4);
            btnAddMovie.Name = "btnAddMovie";
            btnAddMovie.Size = new Size(137, 43);
            btnAddMovie.TabIndex = 4;
            btnAddMovie.Text = "Yeni Film Ekle";
            btnAddMovie.UseVisualStyleBackColor = true;
            btnAddMovie.Click += btnAddMovie_Click;
            // 
            // btnEditMovie
            // 
            btnEditMovie.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditMovie.Location = new Point(158, 541);
            btnEditMovie.Margin = new Padding(3, 4, 3, 4);
            btnEditMovie.Name = "btnEditMovie";
            btnEditMovie.Size = new Size(137, 43);
            btnEditMovie.TabIndex = 5;
            btnEditMovie.Text = "Filmi Düzenle";
            btnEditMovie.UseVisualStyleBackColor = true;
            btnEditMovie.Click += btnEditMovie_Click;
            // 
            // btnDeleteMovie
            // 
            btnDeleteMovie.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDeleteMovie.Location = new Point(302, 541);
            btnDeleteMovie.Margin = new Padding(3, 4, 3, 4);
            btnDeleteMovie.Name = "btnDeleteMovie";
            btnDeleteMovie.Size = new Size(137, 43);
            btnDeleteMovie.TabIndex = 6;
            btnDeleteMovie.Text = "Filmi Sil";
            btnDeleteMovie.UseVisualStyleBackColor = true;
            btnDeleteMovie.Click += btnDeleteMovie_Click;
            // 
            // frmHome
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(btnDeleteMovie);
            Controls.Add(btnEditMovie);
            Controls.Add(btnAddMovie);
            Controls.Add(dgvMovies);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmHome";
            Text = "Ana Sayfa - Tüm Filmler";
            Load += frmHome_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMovies).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvMovies;
        private System.Windows.Forms.Button btnAddMovie;
        private System.Windows.Forms.Button btnEditMovie;
        private System.Windows.Forms.Button btnDeleteMovie;
    }
}