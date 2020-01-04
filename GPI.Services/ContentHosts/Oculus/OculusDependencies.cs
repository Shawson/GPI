using GPI.Services.ContentHosts.Oculus.DataExtraction;
using Microsoft.Extensions.DependencyInjection;

namespace GPI.Services.ContentHosts.Oculus
{
    public class OculusDependencies : IDependencyRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IOculusWebsiteScraper, OculusWebsiteScraper>();
            services.AddScoped<IOculusPathSniffer, OculusPathSniffer>();
        }
    }
}