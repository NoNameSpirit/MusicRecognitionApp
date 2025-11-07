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
            FABtnSettings = new MaterialSkin.Controls.MaterialFloatingActionButton();
            FABtnLibrary = new MaterialSkin.Controls.MaterialFloatingActionButton();
            BtnLibrary = new MaterialSkin.Controls.MaterialButton();
            PanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.AliceBlue;
            PanelMain.Controls.Add(FABtnSettings);
            PanelMain.Controls.Add(FABtnLibrary);
            PanelMain.Controls.Add(BtnLibrary);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(3, 24);
            PanelMain.Margin = new Padding(14);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(14);
            PanelMain.Size = new Size(594, 623);
            PanelMain.TabIndex = 0;
            // 
            // FABtnSettings
            // 
            FABtnSettings.Depth = 0;
            FABtnSettings.Icon = Properties.Resources.next_arrow;
            FABtnSettings.Location = new Point(520, 17);
            FABtnSettings.MouseState = MaterialSkin.MouseState.HOVER;
            FABtnSettings.Name = "FABtnSettings";
            FABtnSettings.Size = new Size(57, 58);
            FABtnSettings.TabIndex = 2;
            FABtnSettings.Text = "materialFloatingActionButton2";
            FABtnSettings.UseVisualStyleBackColor = true;
            FABtnSettings.Click += FABtnSettings_Click;
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
            BtnLibrary.Location = new Point(14, 573);
            BtnLibrary.Margin = new Padding(4, 6, 4, 6);
            BtnLibrary.MouseState = MaterialSkin.MouseState.HOVER;
            BtnLibrary.Name = "BtnLibrary";
            BtnLibrary.NoAccentTextColor = Color.Empty;
            BtnLibrary.Size = new Size(566, 36);
            BtnLibrary.TabIndex = 0;
            BtnLibrary.Text = "Library";
            BtnLibrary.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            BtnLibrary.UseAccentColor = false;
            BtnLibrary.UseVisualStyleBackColor = true;
            BtnLibrary.Click += BtnLibrary_Click;
            // 
            // WaitingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            ClientSize = new Size(600, 650);
            Controls.Add(PanelMain);
            Name = "WaitingForm";
            Text = "WaitingForm";
            PanelMain.ResumeLayout(false);
            PanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private MaterialSkin.Controls.MaterialFloatingActionButton FABtnLibrary;
        private MaterialSkin.Controls.MaterialButton BtnLibrary;
        private MaterialSkin.Controls.MaterialFloatingActionButton FABtnSettings;
    }
}
