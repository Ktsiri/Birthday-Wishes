using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IOCO.BirthdayWishes.EntityFramework.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IOCO.BirthdayWishes.EntityFramework
{
    sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DomainContext>
    {
        public DomainContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) //Directory where the json files are located
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetSection("DatabaseSettings").GetSection("ConnectionString").Value;

            return new DomainContext(opt => opt.UseSqlServer(connectionString,
                x => x.MigrationsHistoryTable("__DomainMigrationsHistory", "dbo")));
        }
    }
}
