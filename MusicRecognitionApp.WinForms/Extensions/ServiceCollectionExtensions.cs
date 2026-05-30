using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Presentation;
using MusicRecognitionApp.Presentation.Services.Implementation;
using MusicRecognitionApp.Presentation.Services.Interfaces;
using MusicRecognitionApp.WinForms.Presentation.Forms;

namespace MusicRecognitionApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBoxService, MessageBoxService>()
                    .AddSingleton<IAnimationService, AnimationService>()
                    .AddSingleton<ICardService, CardService>()
                    .AddSingleton<IResultDisplayService, ResultDisplayService>()
                    .AddSingleton<ISongAddingService, SongAddingService>()
                    .AddSingleton<IStateRegistry, StateRegistryService>()
                    .AddSingleton<IStateManagerService, StateManagerService>();


            services.AddTransient<BaseForm>()
                    .AddTransient<MainForm>()
                    .AddTransient<LoginForm>()
                    .AddTransient<RegistrationForm>();

            services.AddScoped<IApplicationForm, MainForm>();

            return services;
        }
    }
}
