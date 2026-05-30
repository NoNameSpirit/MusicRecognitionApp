namespace MusicRecognitionApp.WinForms.Presentation.Forms
{
    partial class LoginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbLogIn = new MaterialSkin.Controls.MaterialTextBox2();
            tbPassword = new MaterialSkin.Controls.MaterialTextBox2();
            btnSignIn = new MaterialSkin.Controls.MaterialButton();
            btnSignUpNow = new MaterialSkin.Controls.MaterialButton();
            lblTitle = new MaterialSkin.Controls.MaterialLabel();
            SuspendLayout();
            // 
            // tbLogIn
            // 
            tbLogIn.AnimateReadOnly = false;
            tbLogIn.BackgroundImageLayout = ImageLayout.None;
            tbLogIn.CharacterCasing = CharacterCasing.Normal;
            tbLogIn.Depth = 0;
            tbLogIn.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            tbLogIn.HideSelection = true;
            tbLogIn.LeadingIcon = null;
            tbLogIn.Location = new Point(32, 154);
            tbLogIn.Margin = new Padding(4, 5, 4, 5);
            tbLogIn.MaxLength = 50;
            tbLogIn.MouseState = MaterialSkin.MouseState.OUT;
            tbLogIn.Name = "tbLogIn";
            tbLogIn.PasswordChar = '\0';
            tbLogIn.PrefixSuffixText = null;
            tbLogIn.ReadOnly = false;
            tbLogIn.RightToLeft = RightToLeft.No;
            tbLogIn.SelectedText = "";
            tbLogIn.SelectionLength = 0;
            tbLogIn.SelectionStart = 0;
            tbLogIn.ShortcutsEnabled = true;
            tbLogIn.Size = new Size(467, 48);
            tbLogIn.TabIndex = 0;
            tbLogIn.TabStop = false;
            tbLogIn.TextAlign = HorizontalAlignment.Left;
            tbLogIn.TrailingIcon = null;
            tbLogIn.UseSystemPasswordChar = false;
            // 
            // tbPassword
            // 
            tbPassword.AnimateReadOnly = false;
            tbPassword.BackgroundImageLayout = ImageLayout.None;
            tbPassword.CharacterCasing = CharacterCasing.Normal;
            tbPassword.Depth = 0;
            tbPassword.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            tbPassword.HideSelection = true;
            tbPassword.LeadingIcon = null;
            tbPassword.Location = new Point(32, 246);
            tbPassword.Margin = new Padding(4, 5, 4, 5);
            tbPassword.MaxLength = 50;
            tbPassword.MouseState = MaterialSkin.MouseState.OUT;
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '●';
            tbPassword.PrefixSuffixText = null;
            tbPassword.ReadOnly = false;
            tbPassword.RightToLeft = RightToLeft.No;
            tbPassword.SelectedText = "";
            tbPassword.SelectionLength = 0;
            tbPassword.SelectionStart = 0;
            tbPassword.ShortcutsEnabled = true;
            tbPassword.Size = new Size(467, 48);
            tbPassword.TabIndex = 1;
            tbPassword.TabStop = false;
            tbPassword.TextAlign = HorizontalAlignment.Left;
            tbPassword.TrailingIcon = null;
            tbPassword.UseSystemPasswordChar = true;
            // 
            // btnSignIn
            // 
            btnSignIn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSignIn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnSignIn.Depth = 0;
            btnSignIn.HighEmphasis = true;
            btnSignIn.Icon = null;
            btnSignIn.Location = new Point(32, 354);
            btnSignIn.Margin = new Padding(5, 9, 5, 9);
            btnSignIn.MouseState = MaterialSkin.MouseState.HOVER;
            btnSignIn.Name = "btnSignIn";
            btnSignIn.NoAccentTextColor = Color.Empty;
            btnSignIn.Size = new Size(73, 36);
            btnSignIn.TabIndex = 2;
            btnSignIn.Text = "Sign In";
            btnSignIn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnSignIn.UseAccentColor = false;
            btnSignIn.UseVisualStyleBackColor = true;
            btnSignIn.Click += BtnSignIn_Click;
            // 
            // btnSignUpNow
            // 
            btnSignUpNow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSignUpNow.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnSignUpNow.Depth = 0;
            btnSignUpNow.HighEmphasis = true;
            btnSignUpNow.Icon = null;
            btnSignUpNow.Location = new Point(352, 354);
            btnSignUpNow.Margin = new Padding(5, 9, 5, 9);
            btnSignUpNow.MouseState = MaterialSkin.MouseState.HOVER;
            btnSignUpNow.Name = "btnSignUpNow";
            btnSignUpNow.NoAccentTextColor = Color.Empty;
            btnSignUpNow.Size = new Size(147, 36);
            btnSignUpNow.TabIndex = 3;
            btnSignUpNow.Text = "Create Account";
            btnSignUpNow.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnSignUpNow.UseAccentColor = false;
            btnSignUpNow.UseVisualStyleBackColor = true;
            btnSignUpNow.Click += BtnSignUpNow_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Depth = 0;
            lblTitle.Font = new Font("Roboto", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            lblTitle.Location = new Point(32, 62);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.MouseState = MaterialSkin.MouseState.HOVER;
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(225, 41);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Welcome Back";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 462);
            Controls.Add(lblTitle);
            Controls.Add(btnSignUpNow);
            Controls.Add(btnSignIn);
            Controls.Add(tbPassword);
            Controls.Add(tbLogIn);
            Margin = new Padding(4, 5, 4, 5);
            Name = "LoginForm";
            Padding = new Padding(4, 37, 4, 5);
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox2 tbLogIn;
        private MaterialSkin.Controls.MaterialTextBox2 tbPassword;
        private MaterialSkin.Controls.MaterialButton btnSignIn;
        private MaterialSkin.Controls.MaterialButton btnSignUpNow;
        private MaterialSkin.Controls.MaterialLabel lblTitle;
    }
}