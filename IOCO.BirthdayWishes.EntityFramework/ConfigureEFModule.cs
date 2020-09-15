using System;
using System.Collections.Generic;
using System.Text;
using IOCO.BirthdayWishes.Contract;
using IOCO.BirthdayWishes.Contract.Common;
using IOCO.BirthdayWishes.Contract.DependencyInjection;
using IOCO.BirthdayWishes.EntityFramework.DataContexts;
using IOCO.BirthdayWishes.EntityFramework.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace IOCO.BirthdayWishes.EntityFramework
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
                    o => o.EnableRetryOnFailure(3))
                .ConfigureWarnings(w => w.Throw(CoreEventId.IncludeIgnoredWarning));
        }
    }
}
