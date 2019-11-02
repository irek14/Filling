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
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.LightColor = new System.Windows.Forms.Label();
            this.LightColorLabel = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LightVectorLabel = new System.Windows.Forms.Label();
            this.LMovingRadioButton = new System.Windows.Forms.RadioButton();
            this.LConstRadioButton = new System.Windows.Forms.RadioButton();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.FillColorLabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CoefficientRandomRadioButton = new System.Windows.Forms.RadioButton();
            this.CoefficientSameValueRadioButton = new System.Windows.Forms.RadioButton();
            this.mTrackBar = new System.Windows.Forms.TrackBar();
            this.mLabel = new System.Windows.Forms.Label();
            this.ksLabel = new System.Windows.Forms.Label();
            this.kdLabel = new System.Windows.Forms.Label();
            this.CoefficientsLabel = new System.Windows.Forms.Label();
            this.kTrackBar = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.HybridFillRadioButton = new System.Windows.Forms.RadioButton();
            this.InterpolationFillRadioButton = new System.Windows.Forms.RadioButton();
            this.PreciselyFillRadioButton = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.NConstRadioButton = new System.Windows.Forms.RadioButton();
            this.NFromTextureRadioButton = new System.Windows.Forms.RadioButton();
            this.NVectorLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.ObjectColorLabel = new System.Windows.Forms.Label();
            this.ConstColorRadioButton = new System.Windows.Forms.RadioButton();
            this.TextureColorRadioButton = new System.Windows.Forms.RadioButton();
            this.ColorDialog = new System.Windows.Forms.ColorDialog();
            this.WaitLabel = new System.Windows.Forms.Label();
            this.MainTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Photo)).BeginInit();
            this.MenuPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kTrackBar)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTable
            // 
            this.MainTable.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MainTable.ColumnCount = 2;
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1437F));
            this.MainTable.Controls.Add(this.Photo, 1, 0);
            this.MainTable.Controls.Add(this.MenuPanel, 0, 0);
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
            // MenuPanel
            // 
            this.MenuPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.MenuPanel.Controls.Add(this.WaitLabel);
            this.MenuPanel.Controls.Add(this.LightColor);
            this.MenuPanel.Controls.Add(this.LightColorLabel);
            this.MenuPanel.Controls.Add(this.panel5);
            this.MenuPanel.Controls.Add(this.SubmitButton);
            this.MenuPanel.Controls.Add(this.FillColorLabel);
            this.MenuPanel.Controls.Add(this.panel4);
            this.MenuPanel.Controls.Add(this.panel3);
            this.MenuPanel.Controls.Add(this.panel2);
            this.MenuPanel.Controls.Add(this.panel1);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuPanel.Location = new System.Drawing.Point(3, 3);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(219, 895);
            this.MenuPanel.TabIndex = 1;
            // 
            // LightColor
            // 
            this.LightColor.AutoSize = true;
            this.LightColor.BackColor = System.Drawing.Color.White;
            this.LightColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LightColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LightColor.Location = new System.Drawing.Point(137, 640);
            this.LightColor.MinimumSize = new System.Drawing.Size(30, 0);
            this.LightColor.Name = "LightColor";
            this.LightColor.Size = new System.Drawing.Size(30, 15);
            this.LightColor.TabIndex = 16;
            this.LightColor.Click += new System.EventHandler(this.LightColor_Click);
            // 
            // LightColorLabel
            // 
            this.LightColorLabel.AutoSize = true;
            this.LightColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LightColorLabel.Location = new System.Drawing.Point(5, 640);
            this.LightColorLabel.Name = "LightColorLabel";
            this.LightColorLabel.Size = new System.Drawing.Size(99, 16);
            this.LightColorLabel.TabIndex = 15;
            this.LightColorLabel.Text = "Kolor światła";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.LightVectorLabel);
            this.panel5.Controls.Add(this.LMovingRadioButton);
            this.panel5.Controls.Add(this.LConstRadioButton);
            this.panel5.Location = new System.Drawing.Point(10, 524);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 100);
            this.panel5.TabIndex = 5;
            // 
            // LightVectorLabel
            // 
            this.LightVectorLabel.AutoSize = true;
            this.LightVectorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LightVectorLabel.Location = new System.Drawing.Point(-4, 0);
            this.LightVectorLabel.Name = "LightVectorLabel";
            this.LightVectorLabel.Size = new System.Drawing.Size(134, 16);
            this.LightVectorLabel.TabIndex = 3;
            this.LightVectorLabel.Text = "Wektor do światła";
            // 
            // LMovingRadioButton
            // 
            this.LMovingRadioButton.AutoSize = true;
            this.LMovingRadioButton.Location = new System.Drawing.Point(9, 42);
            this.LMovingRadioButton.Name = "LMovingRadioButton";
            this.LMovingRadioButton.Size = new System.Drawing.Size(70, 17);
            this.LMovingRadioButton.TabIndex = 1;
            this.LMovingRadioButton.Text = "Ruchomy";
            this.LMovingRadioButton.UseVisualStyleBackColor = true;
            // 
            // LConstRadioButton
            // 
            this.LConstRadioButton.AutoSize = true;
            this.LConstRadioButton.Checked = true;
            this.LConstRadioButton.Location = new System.Drawing.Point(10, 19);
            this.LConstRadioButton.Name = "LConstRadioButton";
            this.LConstRadioButton.Size = new System.Drawing.Size(50, 17);
            this.LConstRadioButton.TabIndex = 2;
            this.LConstRadioButton.TabStop = true;
            this.LConstRadioButton.Text = "Stały";
            this.LConstRadioButton.UseVisualStyleBackColor = true;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(123, 672);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(75, 23);
            this.SubmitButton.TabIndex = 14;
            this.SubmitButton.Text = "Zastosuj";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // FillColorLabel
            // 
            this.FillColorLabel.AutoSize = true;
            this.FillColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FillColorLabel.Location = new System.Drawing.Point(7, 224);
            this.FillColorLabel.Name = "FillColorLabel";
            this.FillColorLabel.Size = new System.Drawing.Size(133, 16);
            this.FillColorLabel.TabIndex = 6;
            this.FillColorLabel.Text = "Kolor wypełniania";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.CoefficientRandomRadioButton);
            this.panel4.Controls.Add(this.CoefficientSameValueRadioButton);
            this.panel4.Controls.Add(this.mTrackBar);
            this.panel4.Controls.Add(this.mLabel);
            this.panel4.Controls.Add(this.ksLabel);
            this.panel4.Controls.Add(this.kdLabel);
            this.panel4.Controls.Add(this.CoefficientsLabel);
            this.panel4.Controls.Add(this.kTrackBar);
            this.panel4.Location = new System.Drawing.Point(8, 345);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(201, 158);
            this.panel4.TabIndex = 6;
            // 
            // CoefficientRandomRadioButton
            // 
            this.CoefficientRandomRadioButton.AutoSize = true;
            this.CoefficientRandomRadioButton.Location = new System.Drawing.Point(11, 123);
            this.CoefficientRandomRadioButton.Name = "CoefficientRandomRadioButton";
            this.CoefficientRandomRadioButton.Size = new System.Drawing.Size(62, 17);
            this.CoefficientRandomRadioButton.TabIndex = 13;
            this.CoefficientRandomRadioButton.Text = "Losowe";
            this.CoefficientRandomRadioButton.UseVisualStyleBackColor = true;
            // 
            // CoefficientSameValueRadioButton
            // 
            this.CoefficientSameValueRadioButton.AutoSize = true;
            this.CoefficientSameValueRadioButton.Checked = true;
            this.CoefficientSameValueRadioButton.Location = new System.Drawing.Point(11, 19);
            this.CoefficientSameValueRadioButton.Name = "CoefficientSameValueRadioButton";
            this.CoefficientSameValueRadioButton.Size = new System.Drawing.Size(80, 17);
            this.CoefficientSameValueRadioButton.TabIndex = 3;
            this.CoefficientSameValueRadioButton.TabStop = true;
            this.CoefficientSameValueRadioButton.Text = "Takie same";
            this.CoefficientSameValueRadioButton.UseVisualStyleBackColor = true;
            // 
            // mTrackBar
            // 
            this.mTrackBar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mTrackBar.Location = new System.Drawing.Point(29, 83);
            this.mTrackBar.Maximum = 100;
            this.mTrackBar.Minimum = 1;
            this.mTrackBar.Name = "mTrackBar";
            this.mTrackBar.Size = new System.Drawing.Size(161, 45);
            this.mTrackBar.TabIndex = 12;
            this.mTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.mTrackBar.Value = 1;
            // 
            // mLabel
            // 
            this.mLabel.AutoSize = true;
            this.mLabel.Location = new System.Drawing.Point(9, 93);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(15, 13);
            this.mLabel.TabIndex = 11;
            this.mLabel.Text = "m";
            // 
            // ksLabel
            // 
            this.ksLabel.AutoSize = true;
            this.ksLabel.Location = new System.Drawing.Point(180, 53);
            this.ksLabel.Name = "ksLabel";
            this.ksLabel.Size = new System.Drawing.Size(18, 13);
            this.ksLabel.TabIndex = 10;
            this.ksLabel.Text = "ks";
            // 
            // kdLabel
            // 
            this.kdLabel.AutoSize = true;
            this.kdLabel.Location = new System.Drawing.Point(9, 53);
            this.kdLabel.Name = "kdLabel";
            this.kdLabel.Size = new System.Drawing.Size(19, 13);
            this.kdLabel.TabIndex = 9;
            this.kdLabel.Text = "kd";
            // 
            // CoefficientsLabel
            // 
            this.CoefficientsLabel.AutoSize = true;
            this.CoefficientsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CoefficientsLabel.Location = new System.Drawing.Point(-2, 0);
            this.CoefficientsLabel.Name = "CoefficientsLabel";
            this.CoefficientsLabel.Size = new System.Drawing.Size(110, 16);
            this.CoefficientsLabel.TabIndex = 7;
            this.CoefficientsLabel.Text = "Współczynniki";
            this.CoefficientsLabel.Click += new System.EventHandler(this.CoefficientsLabel_Click);
            // 
            // kTrackBar
            // 
            this.kTrackBar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.kTrackBar.LargeChange = 20;
            this.kTrackBar.Location = new System.Drawing.Point(28, 42);
            this.kTrackBar.Maximum = 100;
            this.kTrackBar.Name = "kTrackBar";
            this.kTrackBar.Size = new System.Drawing.Size(137, 45);
            this.kTrackBar.SmallChange = 5;
            this.kTrackBar.TabIndex = 8;
            this.kTrackBar.TickFrequency = 5;
            this.kTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.kTrackBar.Value = 50;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.HybridFillRadioButton);
            this.panel3.Controls.Add(this.InterpolationFillRadioButton);
            this.panel3.Controls.Add(this.PreciselyFillRadioButton);
            this.panel3.Location = new System.Drawing.Point(10, 233);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 100);
            this.panel3.TabIndex = 6;
            // 
            // HybridFillRadioButton
            // 
            this.HybridFillRadioButton.AutoSize = true;
            this.HybridFillRadioButton.Location = new System.Drawing.Point(9, 56);
            this.HybridFillRadioButton.Name = "HybridFillRadioButton";
            this.HybridFillRadioButton.Size = new System.Drawing.Size(78, 17);
            this.HybridFillRadioButton.TabIndex = 2;
            this.HybridFillRadioButton.Text = "Hybrydowo";
            this.HybridFillRadioButton.UseVisualStyleBackColor = true;
            // 
            // InterpolationFillRadioButton
            // 
            this.InterpolationFillRadioButton.AutoSize = true;
            this.InterpolationFillRadioButton.Location = new System.Drawing.Point(9, 33);
            this.InterpolationFillRadioButton.Name = "InterpolationFillRadioButton";
            this.InterpolationFillRadioButton.Size = new System.Drawing.Size(126, 17);
            this.InterpolationFillRadioButton.TabIndex = 1;
            this.InterpolationFillRadioButton.Text = "Z użyciem interpolacji";
            this.InterpolationFillRadioButton.UseVisualStyleBackColor = true;
            // 
            // PreciselyFillRadioButton
            // 
            this.PreciselyFillRadioButton.AutoSize = true;
            this.PreciselyFillRadioButton.Checked = true;
            this.PreciselyFillRadioButton.Location = new System.Drawing.Point(10, 10);
            this.PreciselyFillRadioButton.Name = "PreciselyFillRadioButton";
            this.PreciselyFillRadioButton.Size = new System.Drawing.Size(75, 17);
            this.PreciselyFillRadioButton.TabIndex = 0;
            this.PreciselyFillRadioButton.TabStop = true;
            this.PreciselyFillRadioButton.Text = "Dokładnie";
            this.PreciselyFillRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.NConstRadioButton);
            this.panel2.Controls.Add(this.NFromTextureRadioButton);
            this.panel2.Controls.Add(this.NVectorLabel);
            this.panel2.Location = new System.Drawing.Point(9, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 5;
            // 
            // NConstRadioButton
            // 
            this.NConstRadioButton.AutoSize = true;
            this.NConstRadioButton.Location = new System.Drawing.Point(11, 42);
            this.NConstRadioButton.Name = "NConstRadioButton";
            this.NConstRadioButton.Size = new System.Drawing.Size(50, 17);
            this.NConstRadioButton.TabIndex = 1;
            this.NConstRadioButton.Text = "Stały";
            this.NConstRadioButton.UseVisualStyleBackColor = true;
            // 
            // NFromTextureRadioButton
            // 
            this.NFromTextureRadioButton.AutoSize = true;
            this.NFromTextureRadioButton.Checked = true;
            this.NFromTextureRadioButton.Location = new System.Drawing.Point(10, 19);
            this.NFromTextureRadioButton.Name = "NFromTextureRadioButton";
            this.NFromTextureRadioButton.Size = new System.Drawing.Size(72, 17);
            this.NFromTextureRadioButton.TabIndex = 0;
            this.NFromTextureRadioButton.TabStop = true;
            this.NFromTextureRadioButton.Text = "Z tekstury";
            this.NFromTextureRadioButton.UseVisualStyleBackColor = true;
            // 
            // NVectorLabel
            // 
            this.NVectorLabel.AutoSize = true;
            this.NVectorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.NVectorLabel.Location = new System.Drawing.Point(-4, 0);
            this.NVectorLabel.Name = "NVectorLabel";
            this.NVectorLabel.Size = new System.Drawing.Size(124, 16);
            this.NVectorLabel.TabIndex = 5;
            this.NVectorLabel.Text = "Wektor normalny";
            this.NVectorLabel.Click += new System.EventHandler(this.NVectorLabel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ColorLabel);
            this.panel1.Controls.Add(this.ObjectColorLabel);
            this.panel1.Controls.Add(this.ConstColorRadioButton);
            this.panel1.Controls.Add(this.TextureColorRadioButton);
            this.panel1.Location = new System.Drawing.Point(9, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 4;
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.BackColor = System.Drawing.Color.White;
            this.ColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ColorLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorLabel.Location = new System.Drawing.Point(77, 41);
            this.ColorLabel.MinimumSize = new System.Drawing.Size(30, 0);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(30, 15);
            this.ColorLabel.TabIndex = 4;
            this.ColorLabel.Click += new System.EventHandler(this.ColorLabel_Click);
            // 
            // ObjectColorLabel
            // 
            this.ObjectColorLabel.AutoSize = true;
            this.ObjectColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ObjectColorLabel.Location = new System.Drawing.Point(-4, -3);
            this.ObjectColorLabel.Name = "ObjectColorLabel";
            this.ObjectColorLabel.Size = new System.Drawing.Size(99, 16);
            this.ObjectColorLabel.TabIndex = 3;
            this.ObjectColorLabel.Text = "Kolor obiektu";
            // 
            // ConstColorRadioButton
            // 
            this.ConstColorRadioButton.AutoSize = true;
            this.ConstColorRadioButton.Location = new System.Drawing.Point(11, 39);
            this.ConstColorRadioButton.Name = "ConstColorRadioButton";
            this.ConstColorRadioButton.Size = new System.Drawing.Size(50, 17);
            this.ConstColorRadioButton.TabIndex = 1;
            this.ConstColorRadioButton.Text = "Stały";
            this.ConstColorRadioButton.UseVisualStyleBackColor = true;
            // 
            // TextureColorRadioButton
            // 
            this.TextureColorRadioButton.AutoSize = true;
            this.TextureColorRadioButton.Checked = true;
            this.TextureColorRadioButton.Location = new System.Drawing.Point(10, 16);
            this.TextureColorRadioButton.Name = "TextureColorRadioButton";
            this.TextureColorRadioButton.Size = new System.Drawing.Size(67, 17);
            this.TextureColorRadioButton.TabIndex = 2;
            this.TextureColorRadioButton.TabStop = true;
            this.TextureColorRadioButton.Text = "Tekstura";
            this.TextureColorRadioButton.UseVisualStyleBackColor = true;
            // 
            // ColorDialog
            // 
            this.ColorDialog.Color = System.Drawing.Color.White;
            // 
            // WaitLabel
            // 
            this.WaitLabel.AutoSize = true;
            this.WaitLabel.ForeColor = System.Drawing.Color.Red;
            this.WaitLabel.Location = new System.Drawing.Point(78, 698);
            this.WaitLabel.Name = "WaitLabel";
            this.WaitLabel.Size = new System.Drawing.Size(141, 13);
            this.WaitLabel.TabIndex = 17;
            this.WaitLabel.Text = "Pcozekaj, obraz ładuje się...";
            this.WaitLabel.Visible = false;
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
            this.MenuPanel.ResumeLayout(false);
            this.MenuPanel.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kTrackBar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTable;
        private System.Windows.Forms.PictureBox Photo;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.RadioButton TextureColorRadioButton;
        private System.Windows.Forms.RadioButton ConstColorRadioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label ObjectColorLabel;
        private System.Windows.Forms.ColorDialog ColorDialog;
        private System.Windows.Forms.Label ColorLabel;
        private System.Windows.Forms.Label FillColorLabel;
        private System.Windows.Forms.Label ksLabel;
        private System.Windows.Forms.Label kdLabel;
        private System.Windows.Forms.TrackBar kTrackBar;
        private System.Windows.Forms.Label CoefficientsLabel;
        private System.Windows.Forms.RadioButton HybridFillRadioButton;
        private System.Windows.Forms.RadioButton InterpolationFillRadioButton;
        private System.Windows.Forms.RadioButton PreciselyFillRadioButton;
        private System.Windows.Forms.Label NVectorLabel;
        private System.Windows.Forms.RadioButton NConstRadioButton;
        private System.Windows.Forms.RadioButton NFromTextureRadioButton;
        private System.Windows.Forms.TrackBar mTrackBar;
        private System.Windows.Forms.Label mLabel;
        private System.Windows.Forms.RadioButton CoefficientRandomRadioButton;
        private System.Windows.Forms.RadioButton CoefficientSameValueRadioButton;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Label LightColor;
        private System.Windows.Forms.Label LightColorLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label LightVectorLabel;
        private System.Windows.Forms.RadioButton LMovingRadioButton;
        private System.Windows.Forms.RadioButton LConstRadioButton;
        private System.Windows.Forms.Label WaitLabel;
    }
}

