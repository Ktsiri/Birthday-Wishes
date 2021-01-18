using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BirthdayWishes.Common.Helpers;
using BirthdayWishes.Contract;
using BirthdayWishes.Contract.DependencyInjection;
using BirthdayWishes.DependencyInjection.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayWishes.DependencyInjection
{
    public class DiBootstrapper
    {
        public DiBootstrapper() { }
        public void Bootstrap(IServiceCollection services, IConfiguration configuration = null)
        {
            var registerService = new DependencyRegistration(services, configuration);
            RegisterAllClasses(registerService);
        }

        public DiBootstrapper RegisterAllClasses(IRegisterClass registerClass)
        {
            // TODO:  fix this to load dynamically 
            registerClass.RegisterClasses(Assembly.Load("BirthdayWishes.Common"));
            registerClass.RegisterClasses(Assembly.Load("BirthdayWishes.DomainLogic"));
            registerClass.RegisterClasses(Assembly.Load("BirthdayWishes.ApiClient"));
            registerClass.RegisterClasses(Assembly.Load("BirthdayWishes.DependencyInjection"));
            registerClass.RegisterClasses(Assembly.Load("BirthdayWishes.EntityFramework"));
            registerClass.RegisterClasses(Assembly.Load("BirthdayWishes.SmtpClient"));
            //var referencedAssemblies = Assembly.Load("IOCO.BirthdayWishes.DependencyInjection").GetReferencedAssemblies();

            //var assemblyNames = (from assembly in referencedAssemblies
            //                     let filterAssembly = assembly.Name.Contains("IOCO.")
            //                     where filterAssembly
            //                     select assembly).ToList();

            //foreach (var assemblyName in assemblyNames)
            //{
            //    registerClass.RegisterClasses(Assembly.Load(assemblyName));
            //}

            ConfigureServices(registerClass);

            return this;
        }

        private void ConfigureServices(IRegisterClass registerClass)
        {
            var types = ReflectionHelper.GetTypes<IConfigureDependency>();

            foreach (var type in types)
            {
                var constructor = type.GetConstructors().Single();
                var method = constructor.Invoke(new object[] { registerClass });

                if (method is IConfigureDependency def)
                {
                    def.RegisterDependency();
                }
            }
        }
    }
}
