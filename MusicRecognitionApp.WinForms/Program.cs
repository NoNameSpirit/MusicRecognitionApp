using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicRecognitionApp.Extensions;
using MusicRecognitionApp.Infrastructure.Extensions;
using MusicRecognitionApp.WinForms.Presentation.Forms;

namespace MusicRecognitionApp
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            using var host = CreateHostBuidler(args).Build();
            using var formScope = host.Services.CreateScope();

            LoginForm loginForm = formScope.ServiceProvider.GetRequiredService<LoginForm>();

            System.Windows.Forms.Application.Run(loginForm);
        }

        private static IHostBuilder CreateHostBuidler(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureServices((context, services) =>
                       {
                           services.AddCoreServices()
                                   .AddDatabaseServices(context.Configuration)
                                   .AddInfrustructureServices()
                                   .AddApplicationServices()
                                   .AddPresentationServices();
                       });
        }
    }
}