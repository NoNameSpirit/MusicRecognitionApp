using MusicRecognitionApp.Services.Audio.Interfaces;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services;
using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Services.Interfaces;
using MusicRecognitionApp.Services.UI.Interfaces;
using MusicRecognitionApp.Services.UI;
using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Services.Data.Repositories;
using MusicRecognitionApp.Services.Data.Interfaces;
using MusicRecognitionApp.Services.Data;
using MusicRecognitionApp.Services.Data.Import;
using MusicRecognitionApp.Services.History;
using MusicRecognitionApp.Services.Import;
using MusicRecognitionApp.Services.Search;

namespace MusicRecognitionApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services) 
        {
            var exeDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShazamDB.sqlite");
            var connectionString = $"Data Source={exeDirectory};";

            services.AddDbContext<MusicRecognitionContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<ISongRepository, SongRepository>()
                    .AddScoped<IAudioHashRepository, AudioHashRepository>()
                    .AddScoped<IRecognizedSongRepository, RecognizedSongRepository>();

            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            // (работа с БД, преобразование Entity <-> Model)
            services.AddScoped<ISongService, SongService>()
                    .AddScoped<IAudioHashService, AudioHashService>()
                    .AddScoped<IRecognizedSongService, RecognizedSongService>();

            return services;
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // (бизнес-логика приложения)
            services.AddScoped<ISongImportService, SongImportService>()
                    .AddScoped<ISongSearchService, SongSearchService>()
                    .AddScoped<IRecognitionSongService, RecognitionSongService>();

            return services;
        }

        public static IServiceCollection AddAudioServices(this IServiceCollection services)
        {
            services.AddScoped<IAudioHashGenerator, AudioHashGenerator>()
                    .AddScoped<IAudioProcessor, AudioProcessor>()
                    .AddScoped<IPeakDetector, PeakDetector>()
                    .AddScoped<ISpectrogramBuilder, SpectrogramBuilder>()
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
