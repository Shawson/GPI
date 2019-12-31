using GPI.Api.AppStart;
using GPI.Api.BackgroundServices;
using GPI.Services.BackgroundTasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GPI.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<LifetimeEventsHostedService>();

                    // setup background task runner
                    services.AddSingleton<IBackgroundTaskProgressTracker, BackgroundTaskProgressTracker>();

                    services.AddHostedService<QueuedHostedMediatrTaskService>();
                    services.AddSingleton<IBackgroundMediatrTaskQueue, BackgroundMediatrTaskQueue>();
                    
                });
    }
}
