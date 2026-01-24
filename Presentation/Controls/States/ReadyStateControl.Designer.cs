namespace MusicRecognitionApp.Controls
{
    partial class ReadyStateControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PanelMain = new Panel();
            FABtnAddingTracks = new MaterialSkin.Controls.MaterialFloatingActionButton();
            PicRecordingGif = new PictureBox();
            BtnStartRecognition = new MaterialSkin.Controls.MaterialButton();
            FABtnLibrary = new MaterialSkin.Controls.MaterialFloatingActionButton();
            BtnLibrary = new MaterialSkin.Controls.MaterialButton();
            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicRecordingGif).BeginInit();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.AliceBlue;
            PanelMain.Controls.Add(FABtnAddingTracks);
            PanelMain.Controls.Add(PicRecordingGif);
            PanelMain.Controls.Add(BtnStartRecognition);
            PanelMain.Controls.Add(FABtnLibrary);
            PanelMain.Controls.Add(BtnLibrary);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 0);
            PanelMain.Margin = new Padding(14);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(14);
            PanelMain.Size = new Size(600, 650);
            PanelMain.TabIndex = 0;
            // 
            // FABtnAddingTracks
            // 
            FABtnAddingTracks.BackgroundImage = Properties.Resources.plusIcon;
            FABtnAddingTracks.Depth = 0;
            FABtnAddingTracks.Icon = Properties.Resources.plusIcon;
            FABtnAddingTracks.Location = new Point(528, 17);
            FABtnAddingTracks.MouseState = MaterialSkin.MouseState.HOVER;
            FABtnAddingTracks.Name = "FABtnAddingTracks";
            FABtnAddingTracks.Size = new Size(58, 58);
            FABtnAddingTracks.TabIndex = 5;
            FABtnAddingTracks.Text = "materialFloatingActionButton1";
            FABtnAddingTracks.UseVisualStyleBackColor = true;
            FABtnAddingTracks.Click += FABtnAddingTracks_Click;
            // 
            // PicRecordingGif
            // 
            PicRecordingGif.BackColor = Color.Transparent;
            PicRecordingGif.Image = Properties.Resources.rimuruStatic;
            PicRecordingGif.Location = new Point(150, 130);
            PicRecordingGif.Name = "PicRecordingGif";
            PicRecordingGif.Size = new Size(300, 300);
            PicRecordingGif.SizeMode = PictureBoxSizeMode.Zoom;
            PicRecordingGif.TabIndex = 4;
            PicRecordingGif.TabStop = false;
            PicRecordingGif.Click += PicRecordingGif_Click;
            // 
            // BtnStartRecognition
            // 
            BtnStartRecognition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnStartRecognition.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BtnStartRecognition.Depth = 0;
            BtnStartRecognition.HighEmphasis = true;
            BtnStartRecognition.Icon = null;
            BtnStartRecognition.Location = new Point(220, 439);
            BtnStartRecognition.Margin = new Padding(4, 6, 4, 6);
            BtnStartRecognition.MouseState = MaterialSkin.MouseState.HOVER;
            BtnStartRecognition.Name = "BtnStartRecognition";
            BtnStartRecognition.NoAccentTextColor = Color.Empty;
            BtnStartRecognition.Size = new Size(167, 36);
            BtnStartRecognition.TabIndex = 3;
            BtnStartRecognition.Text = "Start recognition";
            BtnStartRecognition.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            BtnStartRecognition.UseAccentColor = true;
            BtnStartRecognition.UseVisualStyleBackColor = true;
            BtnStartRecognition.Click += BtnStartRecognition_Click;
            // 
            // FABtnLibrary
            // 
            FABtnLibrary.Depth = 0;
            FABtnLibrary.Icon = Properties.Resources.prev_arrow;
            FABtnLibrary.Location = new Point(17, 17);
            FABtnLibrary.MouseState = MaterialSkin.MouseState.HOVER;
            FABtnLibrary.Name = "FABtnLibrary";
            FABtnLibrary.Size = new Size(58, 58);
            FABtnLibrary.TabIndex = 1;
            FABtnLibrary.Text = "materialFloatingActionButton1";
            FABtnLibrary.UseVisualStyleBackColor = true;
            FABtnLibrary.Click += FABtnLibrary_Click;
            // 
            // BtnLibrary
            // 
            BtnLibrary.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnLibrary.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BtnLibrary.Depth = 0;
            BtnLibrary.Dock = DockStyle.Bottom;
            BtnLibrary.HighEmphasis = true;
            BtnLibrary.Icon = null;
            BtnLibrary.Location = new Point(14, 600);
            BtnLibrary.Margin = new Padding(4, 6, 4, 6);
            BtnLibrary.MouseState = MaterialSkin.MouseState.HOVER;
            BtnLibrary.Name = "BtnLibrary";
            BtnLibrary.NoAccentTextColor = Color.Empty;
            BtnLibrary.Size = new Size(572, 36);
            BtnLibrary.TabIndex = 0;
            BtnLibrary.Text = "Library";
            BtnLibrary.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            BtnLibrary.UseAccentColor = false;
            BtnLibrary.UseVisualStyleBackColor = true;
            BtnLibrary.Click += BtnLibrary_Click;
            // 
            // ReadyStateControl
            // 
            Controls.Add(PanelMain);
            Name = "ReadyStateControl";
            Size = new Size(600, 650);
            Load += ReadyStateControl_Load;
            PanelMain.ResumeLayout(false);
            PanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PicRecordingGif).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private MaterialSkin.Controls.MaterialFloatingActionButton FABtnLibrary;
        private MaterialSkin.Controls.MaterialButton BtnLibrary;
        private MaterialSkin.Controls.MaterialButton BtnStartRecognition;
        private PictureBox PicRecordingGif;
        private MaterialSkin.Controls.MaterialFloatingActionButton FABtnAddingTracks;
    }
}
