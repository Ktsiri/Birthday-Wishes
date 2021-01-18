using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BirthdayWishes.EntityFramework.Providers
{
    interface IModelBuilderConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
