using MusicRecognitionApp.WinForms.Properties;

namespace MusicRecognitionApp.Controls
{
    partial class RecordingStateControl
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
            ProgressBarRecording = new MaterialSkin.Controls.MaterialProgressBar();
            LblRecordingStatus = new MaterialSkin.Controls.MaterialLabel();
            BtnStopRecording = new MaterialSkin.Controls.MaterialButton();
            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicRecordingGif).BeginInit();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.AliceBlue;
            PanelMain.Controls.Add(PicRecordingGif);
            PanelMain.Controls.Add(ProgressBarRecording);
            PanelMain.Controls.Add(LblRecordingStatus);
            PanelMain.Controls.Add(BtnStopRecording);
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
            PicRecordingGif.Image = Resources.Rimuru;
            PicRecordingGif.Location = new Point(150, 130);
            PicRecordingGif.Name = "PicRecordingGif";
            PicRecordingGif.Size = new Size(300, 300);
            PicRecordingGif.SizeMode = PictureBoxSizeMode.Zoom;
            PicRecordingGif.TabIndex = 3;
            PicRecordingGif.TabStop = false;
            // 
            // ProgressBarRecording
            // 
            ProgressBarRecording.Depth = 0;
            ProgressBarRecording.Location = new Point(50, 467);
            ProgressBarRecording.MouseState = MaterialSkin.MouseState.HOVER;
            ProgressBarRecording.Name = "ProgressBarRecording";
            ProgressBarRecording.Size = new Size(500, 5);
            ProgressBarRecording.TabIndex = 2;
            // 
            // LblRecordingStatus
            // 
            LblRecordingStatus.Depth = 0;
            LblRecordingStatus.Font = new Font("Roboto", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            LblRecordingStatus.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            LblRecordingStatus.Location = new Point(50, 64);
            LblRecordingStatus.MouseState = MaterialSkin.MouseState.HOVER;
            LblRecordingStatus.Name = "LblRecordingStatus";
            LblRecordingStatus.Size = new Size(500, 29);
            LblRecordingStatus.TabIndex = 1;
            LblRecordingStatus.Text = "Recording audio...";
            LblRecordingStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BtnStopRecording
            // 
            BtnStopRecording.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnStopRecording.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BtnStopRecording.Depth = 0;
            BtnStopRecording.Dock = DockStyle.Bottom;
            BtnStopRecording.HighEmphasis = true;
            BtnStopRecording.Icon = null;
            BtnStopRecording.Location = new Point(14, 600);
            BtnStopRecording.Margin = new Padding(4, 6, 4, 6);
            BtnStopRecording.MouseState = MaterialSkin.MouseState.HOVER;
            BtnStopRecording.Name = "BtnStopRecording";
            BtnStopRecording.NoAccentTextColor = Color.Empty;
            BtnStopRecording.Size = new Size(572, 36);
            BtnStopRecording.TabIndex = 0;
            BtnStopRecording.Text = "Stop recognition";
            BtnStopRecording.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            BtnStopRecording.UseAccentColor = false;
            BtnStopRecording.UseVisualStyleBackColor = true;
            BtnStopRecording.Click += BtnStopRecording_Click;
            // 
            // RecordingStateControl
            // 
            Controls.Add(PanelMain);
            Name = "RecordingStateControl";
            Size = new Size(600, 650);
            PanelMain.ResumeLayout(false);
            PanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PicRecordingGif).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private MaterialSkin.Controls.MaterialButton BtnStopRecording;
        private MaterialSkin.Controls.MaterialProgressBar ProgressBarRecording;
        private MaterialSkin.Controls.MaterialLabel LblRecordingStatus;
        private PictureBox PicRecordingGif;
    }
}
