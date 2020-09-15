using System;
using System.Collections.Generic;
using System.Text;
using IOCO.BirthdayWishes.EntityFramework.Providers;
using Microsoft.EntityFrameworkCore;

namespace IOCO.BirthdayWishes.EntityFramework.Extensions
{
    static class ConfigurationExtensions
    {
        public static ModelBuilder ApplyConfiguration(
            this ModelBuilder modelBuilder, IModelBuilderConfiguration configuration)
        {
            configuration.Configure(modelBuilder);

            return modelBuilder;
        }
    }
}
