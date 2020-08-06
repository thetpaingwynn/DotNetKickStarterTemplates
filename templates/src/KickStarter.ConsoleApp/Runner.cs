using System.Threading.Tasks;
using KickStarter.ConsoleApp.Settings;
using McMaster.Extensions.CommandLineUtils;
using NLog;

namespace KickStarter.ConsoleApp
{
    [Command(Name = "KickStarter.ConsoleApp", FullName = "KickStarter.ConsoleApp", Description = "Dot Net Core Console App")]
    public class Runner
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Task OnExecute(AppConfig config)
        {
            Logger.Info($"Executed - Message: {config.Message}");

            return Task.CompletedTask;
        }
    }
}
