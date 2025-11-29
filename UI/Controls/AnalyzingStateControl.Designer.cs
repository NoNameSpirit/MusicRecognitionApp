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
            BtnStopRecognition = new MaterialSkin.Controls.MaterialButton();
            PanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.AliceBlue;
            PanelMain.Controls.Add(LblProgressPercent);
            PanelMain.Controls.Add(LblAnalyzingStatus);
            PanelMain.Controls.Add(ProgressBarAnalyzing);
            PanelMain.Controls.Add(BtnStopRecognition);
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
            LblProgressPercent.Location = new Point(50, 320);
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
            LblAnalyzingStatus.Location = new Point(50, 250);
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
            ProgressBarAnalyzing.Location = new Point(50, 300);
            ProgressBarAnalyzing.MouseState = MaterialSkin.MouseState.HOVER;
            ProgressBarAnalyzing.Name = "ProgressBarAnalyzing";
            ProgressBarAnalyzing.Size = new Size(500, 5);
            ProgressBarAnalyzing.TabIndex = 1;
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
            // AnalyzingStateControl
            // 
            Controls.Add(PanelMain);
            Name = "AnalyzingStateControl";
            Size = new Size(600, 650);
            PanelMain.ResumeLayout(false);
            PanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private MaterialSkin.Controls.MaterialButton BtnStopRecognition;
        private MaterialSkin.Controls.MaterialLabel LblProgressPercent;
        private MaterialSkin.Controls.MaterialLabel LblAnalyzingStatus;
        private MaterialSkin.Controls.MaterialProgressBar ProgressBarAnalyzing;
    }
}
