using KickStarter.ConsoleApp.Settings;
using KickStarter.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KickStarter.ConsoleApp
{
    public class ApplicationServices : IServiceRegistry
    {
        public void Register(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddOptions();
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
            services.AddSingleton(provider => provider.GetService<IOptions<AppConfig>>().Value);
        }
    }
}
