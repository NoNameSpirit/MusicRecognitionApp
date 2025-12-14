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

            MainForm mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm); 
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDatabaseServices()
                    .AddAudioServices()
                    .AddUIServices()
                    .AddFormServices();

            return services.BuildServiceProvider();
        }
    }
}