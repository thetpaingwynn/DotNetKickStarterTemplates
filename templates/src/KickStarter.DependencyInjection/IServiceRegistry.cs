using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KickStarter.DependencyInjection
{
    public interface IServiceRegistry
    {
        void Register(IServiceCollection services, IConfiguration configuration = null);
    }
}
