using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KickStarter.DependencyInjection
{
    public delegate TService Factory<out TService, in TImplementation>() where TService : class
                                                                         where TImplementation : class;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services) where T : class, IStartupTask
        {
            return services.AddTransient<IStartupTask, T>();
        }

        public static IServiceCollection AddServices<T>(this IServiceCollection services, IConfiguration configuration = null)
            where T : IServiceRegistry, new()
        {
            var registry = new T();
            registry.Register(services, configuration);
            return services;
        }

        public static IServiceCollection AddTransientFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TImplementation>();
            return services.AddTransient<Factory<TService, TImplementation>>(provider => provider.GetService<TImplementation>);
        }

        public static IServiceCollection AddTransientFactory<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TImplementation>();
            return services.AddTransient<Factory<TService, TImplementation>>(provider => () => implementationFactory(provider));
        }

        public static IServiceCollection AddSingletonFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddSingleton<TImplementation>();
            return services.AddTransient<Factory<TService, TImplementation>>(provider => provider.GetService<TImplementation>);
        }

        public static IServiceCollection AddSingletonFactory<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddSingleton<TImplementation, TImplementation>(implementationFactory);
            return services.AddTransient<Factory<TService, TImplementation>>(provider => provider.GetService<TImplementation>);
        }
    }
}
