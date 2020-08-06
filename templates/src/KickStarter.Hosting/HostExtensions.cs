using System.Threading;
using System.Threading.Tasks;
using KickStarter.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KickStarter.Hosting
{
    public static class HostExtensions
    {
        public static async Task RunWithTasksAsync(this IHost host, CancellationToken cancellationToken = default)
        {
            // Load all tasks from DI
            var startupTasks = host.Services.GetServices<IStartupTask>();

            // Execute all the tasks
            foreach (var startupTask in startupTasks)
            {
                await startupTask.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            }

            // Start the tasks as normal
            await host.RunAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
