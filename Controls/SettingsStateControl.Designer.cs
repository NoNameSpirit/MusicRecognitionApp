namespace MusicRecognitionApp.Controls
{
    partial class SettingsStateControl
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
            FABtnReadyStateControl = new MaterialSkin.Controls.MaterialFloatingActionButton();
            PanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.AliceBlue;
            PanelMain.Controls.Add(FABtnReadyStateControl);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 0);
            PanelMain.Margin = new Padding(14);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(14);
            PanelMain.Size = new Size(600, 650);
            PanelMain.TabIndex = 0;
            // 
            // FABtnReadyStateControl
            // 
            FABtnReadyStateControl.Depth = 0;
            FABtnReadyStateControl.Icon = Properties.Resources.prev_arrow;
            FABtnReadyStateControl.Location = new Point(17, 17);
            FABtnReadyStateControl.MouseState = MaterialSkin.MouseState.HOVER;
            FABtnReadyStateControl.Name = "FABtnReadyStateControl";
            FABtnReadyStateControl.Size = new Size(58, 58);
            FABtnReadyStateControl.TabIndex = 1;
            FABtnReadyStateControl.Text = "materialFloatingActionButton1";
            FABtnReadyStateControl.UseVisualStyleBackColor = true;
            FABtnReadyStateControl.Click += FABtnReadyStateControl_Click;
            // 
            // SettingsStateControl
            // 
            Controls.Add(PanelMain);
            Name = "SettingsStateControl";
            Size = new Size(600, 650);
            PanelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private MaterialSkin.Controls.MaterialFloatingActionButton FABtnReadyStateControl;
    }
}
