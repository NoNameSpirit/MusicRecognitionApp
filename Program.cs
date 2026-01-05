using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Extensions;
using MusicRecognitionApp.Infrastructure.Data.Contexts;

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
                .AddPresentationServices();

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