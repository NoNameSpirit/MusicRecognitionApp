namespace MusicRecognitionApp.Controls
{
    partial class AnalyzingStateControl
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
            LblProgressPercent = new MaterialSkin.Controls.MaterialLabel();
            LblAnalyzingStatus = new MaterialSkin.Controls.MaterialLabel();
            ProgressBarAnalyzing = new MaterialSkin.Controls.MaterialProgressBar();
            PicRecordingGif = new PictureBox();
            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicRecordingGif).BeginInit();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.AliceBlue;
            PanelMain.Controls.Add(PicRecordingGif);
            PanelMain.Controls.Add(LblProgressPercent);
            PanelMain.Controls.Add(LblAnalyzingStatus);
            PanelMain.Controls.Add(ProgressBarAnalyzing);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 0);
            PanelMain.Margin = new Padding(14);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(14);
            PanelMain.Size = new Size(600, 650);
            PanelMain.TabIndex = 0;
            // 
            // LblProgressPercent
            // 
            LblProgressPercent.Depth = 0;
            LblProgressPercent.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            LblProgressPercent.Location = new Point(65, 512);
            LblProgressPercent.MouseState = MaterialSkin.MouseState.HOVER;
            LblProgressPercent.Name = "LblProgressPercent";
            LblProgressPercent.Size = new Size(500, 19);
            LblProgressPercent.TabIndex = 3;
            LblProgressPercent.Text = "0%";
            LblProgressPercent.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LblAnalyzingStatus
            // 
            LblAnalyzingStatus.Depth = 0;
            LblAnalyzingStatus.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            LblAnalyzingStatus.Location = new Point(50, 111);
            LblAnalyzingStatus.MouseState = MaterialSkin.MouseState.HOVER;
            LblAnalyzingStatus.Name = "LblAnalyzingStatus";
            LblAnalyzingStatus.Size = new Size(500, 29);
            LblAnalyzingStatus.TabIndex = 2;
            LblAnalyzingStatus.Text = "Analyzing audio...";
            LblAnalyzingStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ProgressBarAnalyzing
            // 
            ProgressBarAnalyzing.Depth = 0;
            ProgressBarAnalyzing.Location = new Point(65, 550);
            ProgressBarAnalyzing.MouseState = MaterialSkin.MouseState.HOVER;
            ProgressBarAnalyzing.Name = "ProgressBarAnalyzing";
            ProgressBarAnalyzing.Size = new Size(500, 5);
            ProgressBarAnalyzing.TabIndex = 1;
            // 
            // PicRecordingGif
            // 
            PicRecordingGif.BackColor = Color.Transparent;
            PicRecordingGif.Image = Properties.Resources.rimuru;
            PicRecordingGif.Location = new Point(150, 175);
            PicRecordingGif.Name = "PicRecordingGif";
            PicRecordingGif.Size = new Size(300, 300);
            PicRecordingGif.SizeMode = PictureBoxSizeMode.Zoom;
            PicRecordingGif.TabIndex = 4;
            PicRecordingGif.TabStop = false;
            // 
            // AnalyzingStateControl
            // 
            Controls.Add(PanelMain);
            Name = "AnalyzingStateControl";
            Size = new Size(600, 650);
            PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicRecordingGif).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private MaterialSkin.Controls.MaterialLabel LblProgressPercent;
        private MaterialSkin.Controls.MaterialLabel LblAnalyzingStatus;
        private MaterialSkin.Controls.MaterialProgressBar ProgressBarAnalyzing;
        private PictureBox PicRecordingGif;
    }
}
