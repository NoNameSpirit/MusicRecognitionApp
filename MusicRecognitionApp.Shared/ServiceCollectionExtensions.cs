using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Interfaces.Audio;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Services.Implementations;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Infrastructure.Audio.Implementations;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;
using MusicRecognitionApp.Infrastructure.Services;
using MusicRecognitionApp.Infrastructure.Services.Implementations;

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
            services.AddSingleton<IAudioHashGenerator, AudioHashGenerator>() 
                    .AddSingleton<IAudioProcessor, AudioProcessor>()         
                    .AddSingleton<IPeakDetector, PeakDetector>()             
                    .AddSingleton<ISpectrogramBuilder, SpectrogramBuilder>() 
                    .AddScoped<IAudioHashService, AudioHashService>()
                    .AddScoped<IRecognizedSongService, RecognizedSongService>()
                    .AddScoped<ISongService, SongService>();

            services.AddScoped<DatabaseInitializer>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISongImportService, SongImportService>()
                    .AddScoped<ISongSearchService, SongSearchService>()
                    .AddScoped<IRecognitionSongService, RecognitionSongService>()
                    .AddScoped<IAnalyzingSessionService, AnalyzingSessionService>()
                    .AddScoped<IRecordingSessionService, RecordingSessionService>()
                    .AddScoped<IRecognitionService, RecognitionService>() 
                    .AddScoped<IRecorderService, RecorderService>()
                    .AddScoped<IProcessingAudio, ProcessingAudio>();

            return services;
        }
    }
}
