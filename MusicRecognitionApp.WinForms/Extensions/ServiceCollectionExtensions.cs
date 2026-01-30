using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Presentation.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Implementation;
using MusicRecognitionApp.Presentation;

namespace MusicRecognitionApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBoxService, MessageBoxService>()
                    .AddSingleton<IAnimationService, AnimationService>()
                    .AddScoped<IStateRegistry, StateRegistryService>()
                    .AddScoped<IStateManagerService, StateManagerService>()
                    .AddScoped<ICardService, CardService>()
                    .AddScoped<IResultDisplayService, ResultDisplayService>()
                    .AddTransient<ISongAddingService, SongAddingService>();

            services.AddScoped<MainForm>();
            
            services.AddScoped<IApplicationForm, MainForm>();

            return services;
        }
    }
}
