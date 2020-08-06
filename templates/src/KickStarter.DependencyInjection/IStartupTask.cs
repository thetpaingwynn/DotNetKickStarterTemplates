using System.Threading;
using System.Threading.Tasks;

namespace KickStarter.DependencyInjection
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
