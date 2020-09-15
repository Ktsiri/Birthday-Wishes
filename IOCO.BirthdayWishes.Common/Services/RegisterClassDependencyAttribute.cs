using System;
using Microsoft.Extensions.DependencyInjection;

namespace IOCO.BirthdayWishes.Common.Services
{
    /// <summary>
    /// Registers a class with its corresponding specified Service Type in the Dependency Injection Container
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RegisterClassDependencyAttribute : Attribute
    {
        public Type ServiceType { get; }
        public ServiceLifetime ServiceLifetime { get; }

        public RegisterClassDependencyAttribute(Type serviceType = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            ServiceLifetime = serviceLifetime;
            ServiceType = serviceType;
        }
    }
}
