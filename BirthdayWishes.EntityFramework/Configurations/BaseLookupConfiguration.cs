using BirthdayWishes.DomainObjects.Base;
using Microsoft.EntityFrameworkCore;

namespace BirthdayWishes.EntityFramework.Configurations
{
    public abstract class BaseLookupConfiguration<TEntity, TIdentity> : BaseActiveConfiguration<TEntity, TIdentity>
        where TEntity : BaseLookupDomainObject<TIdentity>
    {
        public override void Configure(ModelBuilder modelBuilder)
        {
            base.Configure(modelBuilder);

            EntityBuilder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
