namespace MusicRecognitionApp.Controls
{
    partial class ResultStateControl
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
            PicRecordingGif = new PictureBox();
            BtnBackToReady = new MaterialSkin.Controls.MaterialButton();
            PanelResults = new FlowLayoutPanel();
            LblResultTitle = new MaterialSkin.Controls.MaterialLabel();
            BtnLibrary = new MaterialSkin.Controls.MaterialButton();
            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicRecordingGif).BeginInit();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.AliceBlue;
            PanelMain.Controls.Add(PicRecordingGif);
            PanelMain.Controls.Add(BtnBackToReady);
            PanelMain.Controls.Add(PanelResults);
            PanelMain.Controls.Add(LblResultTitle);
            PanelMain.Controls.Add(BtnLibrary);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 0);
            PanelMain.Margin = new Padding(14);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(14);
            PanelMain.Size = new Size(600, 650);
            PanelMain.TabIndex = 0;
            // 
            // PicRecordingGif
            // 
            PicRecordingGif.BackColor = Color.Transparent;
            PicRecordingGif.Image = Properties.Resources.rimuruHasResults;
            PicRecordingGif.Location = new Point(41, 295);
            PicRecordingGif.Name = "PicRecordingGif";
            PicRecordingGif.Size = new Size(520, 198);
            PicRecordingGif.SizeMode = PictureBoxSizeMode.Zoom;
            PicRecordingGif.TabIndex = 6;
            PicRecordingGif.TabStop = false;
            // 
            // BtnBackToReady
            // 
            BtnBackToReady.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnBackToReady.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BtnBackToReady.Depth = 0;
            BtnBackToReady.HighEmphasis = true;
            BtnBackToReady.Icon = null;
            BtnBackToReady.Location = new Point(180, 515);
            BtnBackToReady.Margin = new Padding(4, 6, 4, 6);
            BtnBackToReady.MouseState = MaterialSkin.MouseState.HOVER;
            BtnBackToReady.Name = "BtnBackToReady";
            BtnBackToReady.NoAccentTextColor = Color.Empty;
            BtnBackToReady.Size = new Size(254, 36);
            BtnBackToReady.TabIndex = 4;
            BtnBackToReady.Text = "GO BACK TO MUSIC RECOGNITION";
            BtnBackToReady.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            BtnBackToReady.UseAccentColor = false;
            BtnBackToReady.UseVisualStyleBackColor = true;
            BtnBackToReady.Click += BtnBackToReady_Click;
            // 
            // PanelResults
            // 
            PanelResults.AutoScroll = true;
            PanelResults.FlowDirection = FlowDirection.TopDown;
            PanelResults.Location = new Point(41, 103);
            PanelResults.Name = "PanelResults";
            PanelResults.Size = new Size(520, 186);
            PanelResults.TabIndex = 5;
            PanelResults.WrapContents = false;
            // 
            // LblResultTitle
            // 
            LblResultTitle.Depth = 0;
            LblResultTitle.Font = new Font("Roboto", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
            LblResultTitle.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            LblResultTitle.Location = new Point(77, 34);
            LblResultTitle.MouseState = MaterialSkin.MouseState.HOVER;
            LblResultTitle.Name = "LblResultTitle";
            LblResultTitle.Size = new Size(437, 41);
            LblResultTitle.TabIndex = 3;
            LblResultTitle.Text = "Recognition result";
            LblResultTitle.TextAlign = ContentAlignment.MiddleCenter;
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
            // ResultStateControl
            // 
            Controls.Add(PanelMain);
            Name = "ResultStateControl";
            Size = new Size(600, 650);
            PanelMain.ResumeLayout(false);
            PanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PicRecordingGif).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private MaterialSkin.Controls.MaterialButton BtnLibrary;
        private MaterialSkin.Controls.MaterialButton BtnBackToReady;
        private FlowLayoutPanel PanelResults;
        private MaterialSkin.Controls.MaterialLabel LblResultTitle;
        private PictureBox PicRecordingGif;
    }
}
