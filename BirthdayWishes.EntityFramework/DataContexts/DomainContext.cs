﻿using System;
using System.Collections.Generic;
using System.Text;
using BirthdayWishes.Common.Helpers;
using BirthdayWishes.EntityFramework.Extensions;
using BirthdayWishes.EntityFramework.Providers;
using Microsoft.EntityFrameworkCore;

namespace BirthdayWishes.EntityFramework.DataContexts
{
    sealed class DomainContext : DbContext
    {
        public Action<DbContextOptionsBuilder> OptionsBuilderConfig { get; }
        private readonly IList<IModelBuilderConfiguration> _iModelBuilderConfigurations = new List<IModelBuilderConfiguration>();

        public DomainContext(Action<DbContextOptionsBuilder> optionsBuilderConfig)
        {
            OptionsBuilderConfig = optionsBuilderConfig;
        }

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            OptionsBuilderConfig(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            var types = ReflectionHelper.GetTypes<IModelBuilderConfiguration>();

            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is IModelBuilderConfiguration def)
                {
                    _iModelBuilderConfigurations.Add(def);
                }
            }

            foreach (var item in _iModelBuilderConfigurations)
            {
                modelBuilder.ApplyConfiguration(item);
            }
        }

        #endregion
    }
}
