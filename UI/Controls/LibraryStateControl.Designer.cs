namespace MusicRecognitionApp.Controls
{
    partial class LibraryStateControl
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
            PanelTabs = new Panel();
            BtnAuthors = new MaterialSkin.Controls.MaterialButton();
            BtnSongs = new MaterialSkin.Controls.MaterialButton();
            FABtnReadyStateControl = new MaterialSkin.Controls.MaterialFloatingActionButton();
            PanelContent = new Panel();
            FLPanelOfCards = new FlowLayoutPanel();
            PanelTabs.SuspendLayout();
            PanelContent.SuspendLayout();
            SuspendLayout();
            // 
            // PanelTabs
            // 
            PanelTabs.Controls.Add(BtnAuthors);
            PanelTabs.Controls.Add(BtnSongs);
            PanelTabs.Controls.Add(FABtnReadyStateControl);
            PanelTabs.Dock = DockStyle.Top;
            PanelTabs.Location = new Point(0, 0);
            PanelTabs.Name = "PanelTabs";
            PanelTabs.Size = new Size(600, 80);
            PanelTabs.TabIndex = 0;
            // 
            // BtnAuthors
            // 
            BtnAuthors.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnAuthors.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BtnAuthors.Depth = 0;
            BtnAuthors.HighEmphasis = true;
            BtnAuthors.Icon = null;
            BtnAuthors.Location = new Point(287, 21);
            BtnAuthors.Margin = new Padding(4, 6, 4, 6);
            BtnAuthors.MouseState = MaterialSkin.MouseState.HOVER;
            BtnAuthors.Name = "BtnAuthors";
            BtnAuthors.NoAccentTextColor = Color.Empty;
            BtnAuthors.Size = new Size(191, 36);
            BtnAuthors.TabIndex = 1;
            BtnAuthors.Text = "👤 Recognized authors";
            BtnAuthors.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            BtnAuthors.UseAccentColor = false;
            BtnAuthors.UseVisualStyleBackColor = true;
            BtnAuthors.Click += BtnAuthors_Click;
            // 
            // BtnSongs
            // 
            BtnSongs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnSongs.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BtnSongs.Depth = 0;
            BtnSongs.HighEmphasis = true;
            BtnSongs.Icon = null;
            BtnSongs.Location = new Point(20, 21);
            BtnSongs.Margin = new Padding(4, 6, 4, 6);
            BtnSongs.MouseState = MaterialSkin.MouseState.HOVER;
            BtnSongs.Name = "BtnSongs";
            BtnSongs.NoAccentTextColor = Color.Empty;
            BtnSongs.Size = new Size(173, 36);
            BtnSongs.TabIndex = 0;
            BtnSongs.Text = "🎵 Recognized songs";
            BtnSongs.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            BtnSongs.UseAccentColor = false;
            BtnSongs.UseVisualStyleBackColor = true;
            BtnSongs.Click += BtnSongs_Click;
            // 
            // FABtnReadyStateControl
            // 
            FABtnReadyStateControl.Depth = 0;
            FABtnReadyStateControl.Icon = Properties.Resources.next_arrow;
            FABtnReadyStateControl.Location = new Point(523, 10);
            FABtnReadyStateControl.MouseState = MaterialSkin.MouseState.HOVER;
            FABtnReadyStateControl.Name = "FABtnReadyStateControl";
            FABtnReadyStateControl.Size = new Size(57, 58);
            FABtnReadyStateControl.TabIndex = 2;
            FABtnReadyStateControl.Text = "materialFloatingActionButton2";
            FABtnReadyStateControl.UseVisualStyleBackColor = true;
            FABtnReadyStateControl.Click += FABtnReadyStateControl_Click;
            // 
            // PanelContent
            // 
            PanelContent.Controls.Add(FLPanelOfCards);
            PanelContent.Dock = DockStyle.Fill;
            PanelContent.Location = new Point(0, 80);
            PanelContent.Name = "PanelContent";
            PanelContent.Padding = new Padding(20, 10, 20, 10);
            PanelContent.Size = new Size(600, 520);
            PanelContent.TabIndex = 1;
            // 
            // FLPanelOfCards
            // 
            FLPanelOfCards.AutoScroll = true;
            FLPanelOfCards.Dock = DockStyle.Fill;
            FLPanelOfCards.FlowDirection = FlowDirection.TopDown;
            FLPanelOfCards.Location = new Point(20, 10);
            FLPanelOfCards.Name = "FLPanelOfCards";
            FLPanelOfCards.Size = new Size(560, 500);
            FLPanelOfCards.TabIndex = 0;
            FLPanelOfCards.WrapContents = false;
            // 
            // LibraryStateControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(PanelContent);
            Controls.Add(PanelTabs);
            Name = "LibraryStateControl";
            Size = new Size(600, 600);
            PanelTabs.ResumeLayout(false);
            PanelTabs.PerformLayout();
            PanelContent.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelTabs;
        private MaterialSkin.Controls.MaterialButton BtnSongs;
        private MaterialSkin.Controls.MaterialButton BtnAuthors;
        private MaterialSkin.Controls.MaterialFloatingActionButton FABtnReadyStateControl;
        private Panel PanelContent;
        private FlowLayoutPanel FLPanelOfCards;
    }
}
