using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Services;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services.Audio.Interfaces;
using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            var serviceProvider = ConfigureServices();

            //add db context

            MainForm mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm); 
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //connectionstring = ...
            //adddbcontext...

            services.AddSingleton<IMessageBox, MessageBoxService>()
                    .AddScoped<ICardService, CardService>();

            services.AddSingleton<IStateRegistry, StateRegistryService>()
                    .AddTransient<MainForm>();

            services.AddScoped<IAudioHashGenerator, AudioHashGenerator>()
                    .AddScoped<IAudioProcessor, AudioProcessor>()
                    .AddScoped<IPeakDetector, PeakDetector>()
                    .AddScoped<ISpectrogramBuilder, SpectrogramBuilder>()
                    .AddScoped<IAudioDatabase, AudioDatabaseService>()
                    .AddScoped<IAudioRecognition, AudioRecognitionService>()
                    .AddScoped<IAudioRecorder, AudioRecorderService>();

            return services.BuildServiceProvider();
        }
    }
}