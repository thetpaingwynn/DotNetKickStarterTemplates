using System;
using KickStarter.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace KickStarter.Hosting
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Register services from IServiceRegistry
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder ConfigureServices<T>(this IHostBuilder hostBuilder) where T : IServiceRegistry, new()
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var registry = new T();
                registry.Register(services, hostContext.Configuration);
            });
        }

        /// <summary>
        /// Add Json configuration file {name}.appsettings.json
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="name">Configuration name</param>
        /// <returns></returns>
        public static IHostBuilder UseCustomAppSettings(this IHostBuilder hostBuilder, string name = null)
        {
            return hostBuilder.ConfigureAppConfiguration((hostContext, config) =>
            {
                name = name ?? AppDomain.CurrentDomain.FriendlyName;

                config.AddJsonFile($"{name}.appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{name}.appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
            });
        }
    }
}
