using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace IOCO.BirthdayWishes.Contract.DependencyInjection
{
    public interface IRegisterClass
    {
        IServiceCollection Services { get; }
        IRegisterClass ConfigureSettings<TSettings>(params string[] optionsLocation)
            where TSettings : class, new();

        IRegisterClass RegisterClass(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime);

        IRegisterClass RegisterClass<TService, TImplementation>(ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService;

        IRegisterClass RegisterClass<TService>(
            Func<IServiceProvider, TService> provider, ServiceLifetime serviceLifetime)
            where TService : class;

        IRegisterClass RegisterClasses(Assembly assembly);
    }
}
