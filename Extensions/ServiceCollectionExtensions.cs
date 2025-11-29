using MusicRecognitionApp.Services.Audio.Interfaces;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services;
using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Services.Interfaces;
using MusicRecognitionApp.Services.UI.Interfaces;
using MusicRecognitionApp.Services.UI;

namespace MusicRecognitionApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAudioServices(this IServiceCollection services)
        {
            var exeDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShazamDB.sqlite");
            var connectionString = $"Data Source={exeDirectory};Version=3;";

            services.AddSingleton(connectionString)
                    .AddScoped<IAudioHashGenerator, AudioHashGenerator>()
                    .AddScoped<IAudioProcessor, AudioProcessor>()
                    .AddScoped<IPeakDetector, PeakDetector>()
                    .AddScoped<ISpectrogramBuilder, SpectrogramBuilder>()
                    .AddScoped<IAudioDatabase, AudioDatabaseService>()
                    .AddScoped<IAudioRecognition, AudioRecognitionService>()
                    .AddScoped<IAudioRecorder, AudioRecorderService>();

            return services;
        }

        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            services.AddSingleton<IStateRegistry, StateRegistryService>()
                    .AddSingleton<IMessageBox, MessageBoxService>()
                    .AddScoped<ICardService, CardService>()
                    .AddScoped<IAnimationService, AnimationService>()
                    .AddScoped<IResultCardBuilder, ResultCardBuilder>();

            return services;
        }

        public static IServiceCollection AddFormServices(this IServiceCollection services)
        {
            services.AddTransient<MainForm>();

            return services;
        }
    }
}
