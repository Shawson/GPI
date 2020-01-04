using Microsoft.Extensions.DependencyInjection;

namespace GPI.Services.ContentHosts
{
    public interface IDependencyRegistration
    {
        void RegisterServices(IServiceCollection serviceCollection);
    }
}