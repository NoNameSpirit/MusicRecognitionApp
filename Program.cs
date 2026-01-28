using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Extensions;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using Microsoft.Extensions.Logging;

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

                var context = scope.ServiceProvider.GetRequiredService<MusicRecognitionContext>();

                context.Database.EnsureCreated();
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