using System;
using System.Threading.Tasks;
using KickStarter.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace KickStarter.ConsoleApp
{
    internal class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static async Task Main(string[] args)
        {
            #region # Log Unhandled Exception
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            #endregion

            try
            {
                await CreateHostBuilder(args).RunCommandLineApplicationAsync<Runner>(args).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, ex.Message);
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                        .UseCustomAppSettings()
                        .ConfigureServices<ApplicationServices>()
                        .ConfigureLogging((hostContext, configLogging) =>
                        {
                            configLogging.ClearProviders(); // Clear log providers added by `CreateDefaultBuilder`
                                                            //configure NLog
                            configLogging.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                            LogManager.LoadConfiguration($"{AppDomain.CurrentDomain.FriendlyName}.nlog.config");
                        });
        }

        #region # Log Unhandled Exception

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                var aggregateEx = e.Exception.Flatten();
                foreach (var ex in aggregateEx.InnerExceptions)
                {
                    Logger.Fatal(ex, "an unobserved task exception has occurred.");
                }
                e.SetObserved();
            }
            finally
            {
                Environment.Exit(1);
            }
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = (Exception)e.ExceptionObject;
                Logger.Fatal(ex, "a current domain unhandled exception exception has occurred.");
            }
            finally
            {
                Environment.Exit(1);
            }
        }

        #endregion
    }
}
