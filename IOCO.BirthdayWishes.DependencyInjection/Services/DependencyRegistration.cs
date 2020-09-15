using IOCO.BirthdayWishes.Common.Helpers;
using IOCO.BirthdayWishes.Contract.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Contract.Common;

namespace IOCO.BirthdayWishes.DependencyInjection.Services
{
    sealed class DependencyRegistration : IRegisterClass
    {
        public IServiceCollection Services { get; }
        private readonly IConfiguration _configuration;

        public DependencyRegistration(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public DependencyRegistration(IServiceCollection services, IConfiguration configuration)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IRegisterClass ConfigureSettings<TSettings>(params string[] optionsLocation)
            where TSettings : class, new()
        {
            IConfigurationSection section = optionsLocation.Aggregate<string, IConfigurationSection>(null, (current, location) => current == null ? _configuration.GetSection(location) : current.GetSection(location));

            Services.Configure<TSettings>(options => section.Bind(options));
            Services.AddSingleton<ISettings<TSettings>, SettingsOptions<TSettings>>();

            return this;
        }

        public IRegisterClass RegisterClass(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime)
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    Services.AddSingleton(serviceType, implementationType);
                    break;
                case ServiceLifetime.Scoped:
                    Services.AddScoped(serviceType, implementationType);
                    break;
                case ServiceLifetime.Transient:
                    Services.AddTransient(serviceType, implementationType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null);
            }

            return this;
        }

        public IRegisterClass RegisterClass<TService, TImplementation>(ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return RegisterClass(typeof(TService), typeof(TImplementation), serviceLifetime);
        }

        public IRegisterClass RegisterClass<TService>(Func<IServiceProvider, TService> implementationFactory, ServiceLifetime serviceLifetime)
            where TService : class
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    Services.AddSingleton(implementationFactory);
                    break;
                case ServiceLifetime.Scoped:
                    Services.AddScoped(implementationFactory);
                    break;
                case ServiceLifetime.Transient:
                    Services.AddTransient(implementationFactory);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null);
            }

            return this;
        }

        public IRegisterClass RegisterClasses(Assembly assembly)
        {
            foreach (Type typeToRegister in GetTypesWitRegisterServiceAttribute(assembly))
            {
                var registerServiceAttribute =
                    ReflectionHelper.GetCustomAttributes<RegisterClassDependencyAttribute>(typeToRegister).First();

                ServiceLifetime serviceLifetime = registerServiceAttribute.ServiceLifetime;
                Type serviceType = registerServiceAttribute.ServiceType;

                if (serviceType == null)
                    serviceType = typeToRegister;

                RegisterClass(serviceType, typeToRegister, serviceLifetime);
            }

            return this;
        }

        IEnumerable<Type> GetTypesWitRegisterServiceAttribute(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(RegisterClassDependencyAttribute), true).Length > 0);
        }
    }
}
