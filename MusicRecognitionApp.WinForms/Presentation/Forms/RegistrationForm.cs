using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Auth;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.WinForms.Presentation.Forms
{
    public partial class RegistrationForm : BaseForm
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RegistrationForm(
            IMessageBoxService messageBoxService,
            IServiceScopeFactory serviceScopeFactory)
        {
            InitializeComponent();

            _messageBoxService = messageBoxService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        private void RegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }

        private async void BtnSignUpNow_Click(object sender, EventArgs e)
        {
            var username = tbLogIn.Text.Trim();
            var password = tbPassword.Text;
            var confirmPassword = tbConfirmPassword.Text;

            if (password != confirmPassword)
            {
                _messageBoxService.ShowInfo("Passwords do not match.");
                return;
            }

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                    var result = await userService.RegisterAsync(username, password);

                    if (!result.IsSuccess)
                    {
                        _messageBoxService.ShowInfo(result.Error);
                        return;
                    }

                    _messageBoxService.ShowInfo("Registration successful! Please sign in.");

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError($"Registration error: {ex.Message}");
            }
        }

        private void btnBackToSignIn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}