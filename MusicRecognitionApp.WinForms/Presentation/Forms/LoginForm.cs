using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Auth;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.WinForms.Presentation.Forms
{
    public partial class LoginForm : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserService _userService;
        private readonly IMessageBoxService _messageBoxService;

        public LoginForm(
            IServiceProvider serviceProvider,
            IUserService userService,
            IMessageBoxService messageBoxService)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _userService = userService;
            _messageBoxService = messageBoxService;

            tbLogIn.Text = "Admin2";
            tbPassword.Text = "12344321qw";
        }

        private void BtnSignUpNow_Click(object sender, EventArgs e)
        {
            Hide();

            using (var registrationForm = _serviceProvider.GetRequiredService<RegistrationForm>())
            {
                if (registrationForm.ShowDialog() == DialogResult.Cancel)
                {
                    ClearEnteredData();
                }

                Show();
            }
        }

        private async void BtnSignIn_Click(object sender, EventArgs e)
        {
            var username = tbLogIn.Text.Trim();
            var password = tbPassword.Text;

            try
            {
                var result = await _userService.LoginAsync(username, password);

                if (!result.IsSuccess)
                {
                    _messageBoxService.ShowInfo(result.Error);
                    return;
                }

                Hide();

                using (var mainForm = _serviceProvider.GetRequiredService<MainForm>())
                {
                    if (mainForm.ShowDialog() == DialogResult.Cancel)
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError($"Login error: {ex.Message}");
            }
        }

        private void ClearEnteredData()
        {
            tbLogIn.Text = string.Empty;
            tbPassword.Text = string.Empty;
        }
    }
}
