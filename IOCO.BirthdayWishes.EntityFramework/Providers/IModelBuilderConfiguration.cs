using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace IOCO.BirthdayWishes.EntityFramework.Providers
{
    interface IModelBuilderConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
