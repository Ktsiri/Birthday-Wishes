using BirthdayWishes.DomainObjects.Base;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BirthdayWishes.EntityFramework.Configurations
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseActiveConfiguration<TEntity, TIdentity> : BaseConfiguration<TEntity, TIdentity>
        where TEntity : BaseActiveDomainObject<TIdentity>
    {
        public override void Configure(ModelBuilder modelBuilder)
        {
            base.Configure(modelBuilder);

            EntityBuilder.Property(c => c.IsActive)
                .HasDefaultValue(false);
        }
    }
}
