using BirthdayWishes.DomainObjects.Base;
using BirthdayWishes.EntityFramework.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirthdayWishes.EntityFramework.Configurations
{
    public abstract class BaseConfiguration<TEntity, TIdentity> : IModelBuilderConfiguration
        where TEntity : BaseDomainObject<TIdentity>
    {
        protected EntityTypeBuilder<TEntity> EntityBuilder { get; private set; }

        public virtual void Configure(ModelBuilder modelBuilder)
        {
            EntityBuilder = modelBuilder.Entity<TEntity>();

            EntityBuilder.HasKey(c => c.Id);
        }
    }
}
