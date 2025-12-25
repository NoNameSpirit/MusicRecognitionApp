using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Services;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services.Audio.Interfaces;
using MusicRecognitionApp.Services.Interfaces;
using MusicRecognitionApp.Extensions;

namespace MusicRecognitionApp
{
    internal static class Program
    {
        private const string ConnectionString = "ShzamDB";

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            var serviceProvider = ConfigureServices();

            EnsureDatabaseCreated(serviceProvider);

            MainForm mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm); 
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services
                .AddDatabaseServices()
                .AddDataServices()
                .AddBusinessServices()
                .AddAudioServices()
                .AddUIServices()
                .AddFormServices();

            return services.BuildServiceProvider();
        }

        private static void EnsureDatabaseCreated(ServiceProvider serviceProvider)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<MusicRecognitionContext>();

                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка запуска: {ex.Message}{Environment.NewLine}{Environment.NewLine}" +
                    $"Закройте приложение и попробуйте снова.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
    }
}