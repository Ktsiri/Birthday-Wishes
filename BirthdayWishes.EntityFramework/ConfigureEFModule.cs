using System;
using System.Collections.Generic;
using System.Text;
using BirthdayWishes.Contract;
using BirthdayWishes.Contract.Common;
using BirthdayWishes.Contract.DependencyInjection;
using BirthdayWishes.EntityFramework.DataContexts;
using BirthdayWishes.EntityFramework.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayWishes.EntityFramework
{
    public class ConfigureEFModule : IConfigureDependency
    {
        private readonly IRegisterClass _registerClass;

        public ConfigureEFModule(IRegisterClass registerClass)
        {
            _registerClass = registerClass;
        }
        public void RegisterDependency()
        {
            _registerClass.ConfigureSettings<DatabaseSettings>("DatabaseSettings");
            RegisterDatabase();
        }

        private void RegisterDatabase(Action<DbContextOptionsBuilder> optionsBuilderConfig = null)
        {
            _registerClass
                .RegisterClass(
                    (services) => new DomainContext(optionsBuilderConfig ?? (opt => ConfigureDataContext(services, opt))),
                    ServiceLifetime.Scoped);
        }

        private void ConfigureDataContext(IServiceProvider serviceProvider, DbContextOptionsBuilder contextOptionsBuilder)
        {
            DatabaseSettings databaseSettings = serviceProvider.GetRequiredService<ISettings<DatabaseSettings>>().Value;
            string connectionString = databaseSettings.ConnectionString;

            contextOptionsBuilder
                .UseSqlServer(connectionString,
                    o => o.EnableRetryOnFailure(3));
        }
    }
}
