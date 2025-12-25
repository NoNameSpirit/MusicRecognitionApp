using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Interfaces;
using MusicRecognitionApp.Infrastructure.Data.Repositories;
using MusicRecognitionApp.Infrastructure.Audio.Interfaces;
using MusicRecognitionApp.Infrastructure.Audio.Implementations;
using MusicRecognitionApp.Infrastructure.Services.Interfaces;
using MusicRecognitionApp.Infrastructure.Services.Implementations;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Application.Services.Implementations;
using MusicRecognitionApp.Presentation.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Implementation;

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

        public static IServiceCollection AddInfrustructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAudioHashGenerator, AudioHashGenerator>()
                    .AddScoped<IAudioProcessor, AudioProcessor>()
                    .AddScoped<IPeakDetector, PeakDetector>()
                    .AddScoped<ISpectrogramBuilder, SpectrogramBuilder>()
                    .AddScoped<IAudioHashService, AudioHashService>()
                    .AddScoped<IRecognizedSongService, RecognizedSongService>()
                    .AddScoped<ISongService, SongService>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAudioRecognition, AudioRecognitionService>()
                    .AddScoped<IAudioRecorder, AudioRecorderService>()
                    .AddScoped<ISongImportService, SongImportService>()
                    .AddScoped<ISongSearchService, SongSearchService>()
                    .AddScoped<IRecognitionSongService, RecognitionSongService>();

            return services;
        }

        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddSingleton<IStateRegistry, StateRegistryService>()
                    .AddSingleton<IMessageBox, MessageBoxService>()
                    .AddScoped<ICardService, CardService>()
                    .AddScoped<IAnimationService, AnimationService>()
                    .AddScoped<IResultCardBuilder, ResultCardBuilder>();
           
            services.AddTransient<MainForm>();

            return services;
        }
    }
}
