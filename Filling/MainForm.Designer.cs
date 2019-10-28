namespace Filling
{
    partial class MainForm
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
            this.MainTable = new System.Windows.Forms.TableLayoutPanel();
            this.Photo = new System.Windows.Forms.PictureBox();
            this.MainTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Photo)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTable
            // 
            this.MainTable.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MainTable.ColumnCount = 2;
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1437F));
            this.MainTable.Controls.Add(this.Photo, 1, 0);
            this.MainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTable.Location = new System.Drawing.Point(0, 0);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowCount = 1;
            this.MainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTable.Size = new System.Drawing.Size(1662, 901);
            this.MainTable.TabIndex = 0;
            // 
            // Photo
            // 
            this.Photo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Photo.Location = new System.Drawing.Point(228, 3);
            this.Photo.Name = "Photo";
            this.Photo.Size = new System.Drawing.Size(1431, 895);
            this.Photo.TabIndex = 0;
            this.Photo.TabStop = false;
            this.Photo.Paint += new System.Windows.Forms.PaintEventHandler(this.Photo_Paint);
            this.Photo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Photo_MouseDown);
            this.Photo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Photo_MouseMove);
            this.Photo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Photo_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1662, 901);
            this.Controls.Add(this.MainTable);
            this.MaximumSize = new System.Drawing.Size(1700, 940);
            this.MinimumSize = new System.Drawing.Size(1678, 940);
            this.Name = "MainForm";
            this.Text = "Filling editor";
            this.MainTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Photo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTable;
        private System.Windows.Forms.PictureBox Photo;
    }
}

