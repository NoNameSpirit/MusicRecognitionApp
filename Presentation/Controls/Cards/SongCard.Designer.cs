using MaterialSkin.Controls;

namespace MusicRecognitionApp.Presentation.Controls
{
    partial class SongCard
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
            components = new System.ComponentModel.Container();
            AutoScaleMode = AutoScaleMode.Font;
            lblTitle = new MaterialLabel();
            lblArtist = new MaterialLabel();
            lblMatches = new MaterialLabel();
            toolTip = new ToolTip();
            notificationToolTip = new ToolTip();
            btnCopy = new Button();
            divider = new MaterialDivider();
            // 
            // lblTitle
            // 
            lblTitle.Location = new Point(15, 60);
            lblTitle.AutoSize = true;
            // 
            // lblArtist
            // 
            lblArtist.Location = new Point(15, 15);
            lblArtist.AutoSize = true;
            // 
            // lblMatches
            // 
            lblMatches.Location = new Point(360, 60);
            lblMatches.AutoSize = true;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // notificationToolTip
            // 
            notificationToolTip.ToolTipIcon = ToolTipIcon.Info;
            notificationToolTip.AutoPopDelay = 1000;
            notificationToolTip.InitialDelay = 500;
            notificationToolTip.ShowAlways = false;
            notificationToolTip.UseFading = true;
            // 
            // btnCopySong
            // 
            btnCopy.Text = "📋";
            btnCopy.Size = new Size(35, 40);
            btnCopy.Location = new Point(480, 10);
            // 
            // divider
            // 
            divider.Height = 2;
            divider.Dock = DockStyle.Bottom;
            Size = new Size(520, 100);
            Margin = new Padding(0, 0, 0, 10);
            Controls.AddRange(new Control[] { lblTitle, lblArtist, lblMatches, btnCopy, divider });
        }

        private MaterialLabel lblTitle;
        private MaterialLabel lblArtist;
        private MaterialLabel lblMatches;
        private ToolTip toolTip;
        private ToolTip notificationToolTip;
        private Button btnCopy;
        private MaterialDivider divider;

        #endregion
    }
}
