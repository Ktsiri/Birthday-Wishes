using System;
using System.Collections.Generic;
using System.Text;
using BirthdayWishes.EntityFramework.Providers;
using Microsoft.EntityFrameworkCore;

namespace BirthdayWishes.EntityFramework.Extensions
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
