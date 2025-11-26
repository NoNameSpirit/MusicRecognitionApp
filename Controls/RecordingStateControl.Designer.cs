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
            BtnStopRecognition = new MaterialSkin.Controls.MaterialButton();
            LblRecordingStatus = new MaterialSkin.Controls.MaterialLabel();
            ProgressBarRecording = new MaterialSkin.Controls.MaterialProgressBar();
            LblTimer = new MaterialSkin.Controls.MaterialLabel();
            PanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.AliceBlue;
            PanelMain.Controls.Add(LblTimer);
            PanelMain.Controls.Add(ProgressBarRecording);
            PanelMain.Controls.Add(LblRecordingStatus);
            PanelMain.Controls.Add(BtnStopRecognition);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 0);
            PanelMain.Margin = new Padding(14);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(14);
            PanelMain.Size = new Size(600, 650);
            PanelMain.TabIndex = 0;
            // 
            // BtnStopRecognition
            // 
            BtnStopRecognition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnStopRecognition.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BtnStopRecognition.Depth = 0;
            BtnStopRecognition.Dock = DockStyle.Bottom;
            BtnStopRecognition.HighEmphasis = true;
            BtnStopRecognition.Icon = null;
            BtnStopRecognition.Location = new Point(14, 600);
            BtnStopRecognition.Margin = new Padding(4, 6, 4, 6);
            BtnStopRecognition.MouseState = MaterialSkin.MouseState.HOVER;
            BtnStopRecognition.Name = "BtnStopRecognition";
            BtnStopRecognition.NoAccentTextColor = Color.Empty;
            BtnStopRecognition.Size = new Size(572, 36);
            BtnStopRecognition.TabIndex = 0;
            BtnStopRecognition.Text = "Stop recognition";
            BtnStopRecognition.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            BtnStopRecognition.UseAccentColor = false;
            BtnStopRecognition.UseVisualStyleBackColor = true;
            BtnStopRecognition.Click += BtnStopRecognition_Click;
            // 
            // LblRecordingStatus
            // 
            LblRecordingStatus.Depth = 0;
            LblRecordingStatus.Font = new Font("Roboto", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            LblRecordingStatus.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            LblRecordingStatus.Location = new Point(50, 250);
            LblRecordingStatus.MouseState = MaterialSkin.MouseState.HOVER;
            LblRecordingStatus.Name = "LblRecordingStatus";
            LblRecordingStatus.Size = new Size(500, 29);
            LblRecordingStatus.TabIndex = 1;
            LblRecordingStatus.Text = "Записываем аудио...";
            LblRecordingStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ProgressBarRecording
            // 
            ProgressBarRecording.Depth = 0;
            ProgressBarRecording.Location = new Point(50, 300);
            ProgressBarRecording.MouseState = MaterialSkin.MouseState.HOVER;
            ProgressBarRecording.Name = "ProgressBarRecording";
            ProgressBarRecording.Size = new Size(500, 5);
            ProgressBarRecording.TabIndex = 2;
            // 
            // LblTimer
            // 
            LblTimer.Depth = 0;
            LblTimer.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            LblTimer.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            LblTimer.Location = new Point(50, 320);
            LblTimer.MouseState = MaterialSkin.MouseState.HOVER;
            LblTimer.Name = "LblTimer";
            LblTimer.Size = new Size(500, 19);
            LblTimer.TabIndex = 3;
            LblTimer.Text = "00:15";
            LblTimer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RecordingStateControl
            // 
            Controls.Add(PanelMain);
            Name = "RecordingStateControl";
            Size = new Size(600, 650);
            PanelMain.ResumeLayout(false);
            PanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private MaterialSkin.Controls.MaterialButton BtnStopRecognition;
        private MaterialSkin.Controls.MaterialLabel LblTimer;
        private MaterialSkin.Controls.MaterialProgressBar ProgressBarRecording;
        private MaterialSkin.Controls.MaterialLabel LblRecordingStatus;
    }
}
