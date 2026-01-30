using MaterialSkin.Controls;

namespace MusicRecognitionApp.Presentation.Controls
{
    partial class NoInfoCard
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.lblTitle = new MaterialLabel();
            this.toolTip = new ToolTip();
            this.divider = new MaterialDivider();
            //
            // lblTitle
            //
            lblTitle.Location = new Point(15, 60);
            lblTitle.AutoSize = true;
            //
            // toolTip
            //
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            //
            // divider
            //
            divider.Height = 2;
            divider.Dock = DockStyle.Bottom;

            this.Size = new Size(520, 100);
            this.Margin = new Padding(0, 0, 0, 10);
            this.Controls.AddRange(new Control[] { lblTitle, divider });
        }

        private MaterialLabel lblTitle;
        private ToolTip toolTip;
        private MaterialDivider divider;

        #endregion
    }
}
