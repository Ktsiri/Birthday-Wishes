using BirthdayWishes.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;

namespace BirthdayWishes.EntityFramework.Configurations
{
    public sealed class BirthdayQueueConfiguration : BaseConfiguration<MessageQueue, Guid>
    {
        public override void Configure(ModelBuilder modelBuilder)
        {
            base.Configure(modelBuilder);

            EntityBuilder.Property(x => x.SystemUniqueId)
                .IsRequired()
                .HasDefaultValue(true);

            EntityBuilder.Property(x => x.SystemUniqueId)
                .IsRequired()
                .HasMaxLength(255);


            EntityBuilder.Property(x => x.IsBusyProcessing)
                .IsRequired();

            EntityBuilder.Property(x => x.RetryCount)
                .IsRequired();

            EntityBuilder.Property(x => x.MessageStatus)
                .IsRequired()
                .HasColumnType("tinyint");

            EntityBuilder.Property(x => x.MessageType)
                .IsRequired()
                .HasColumnType("tinyint");

            EntityBuilder.Property(x => x.CreatedDate)
                .IsRequired()
                .HasColumnType("datetime");

            EntityBuilder.Property(x => x.UpdatedDate)
                .IsRequired(false)
                .HasColumnType("datetime");
        }
    }
}