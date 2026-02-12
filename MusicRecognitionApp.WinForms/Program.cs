using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicRecognitionApp.Extensions;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Extensions;
using MusicRecognitionApp.Infrastructure.Services;

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
            
            MainForm mainForm = formScope.ServiceProvider.GetRequiredService<MainForm>(); 
            
            System.Windows.Forms.Application.Run(mainForm); 
        }   

        private static IHostBuilder CreateHostBuidler(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureServices((context, services) => 
                       {
                            services.AddDatabaseServices(context.Configuration)
                                    .AddInfrustructureServices()
                                    .AddApplicationServices()
                                    .AddPresentationServices();
                       });
        }
    }
}