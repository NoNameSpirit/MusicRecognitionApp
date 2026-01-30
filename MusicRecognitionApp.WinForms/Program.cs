using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Extensions;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Infrastructure.Extensions;
using MusicRecognitionApp.Infrastructure.Services;

namespace MusicRecognitionApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            var serviceProvider = ConfigureServices();

            EnsureDatabaseCreated(serviceProvider);

            using var scope = serviceProvider.CreateScope();
            MainForm mainForm = scope.ServiceProvider.GetRequiredService<MainForm>(); 
            System.Windows.Forms.Application.Run(mainForm); 
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services
                .AddDatabaseServices()
                .AddInfrustructureServices()
                .AddApplicationServices()
                .AddPresentationServices()
                .AddLogging(builder =>
                {
                    builder.AddDebug();
                });

            return services.BuildServiceProvider();
        }

        private static void EnsureDatabaseCreated(ServiceProvider serviceProvider)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();

                var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();

                initializer.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Launch error: {ex.Message}{Environment.NewLine}{Environment.NewLine}" +
                    $"Close the app and try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
    }
}