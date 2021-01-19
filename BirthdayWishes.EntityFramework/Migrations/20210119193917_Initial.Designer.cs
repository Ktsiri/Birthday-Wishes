﻿// <auto-generated />
using System;
using BirthdayWishes.EntityFramework.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BirthdayWishes.EntityFramework.Migrations
{
    [DbContext(typeof(DomainContext))]
    [Migration("20210119193917_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BirthdayWishes.DomainObjects.MessageQueue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsBusyProcessing")
                        .HasColumnType("bit");

                    b.Property<byte>("MessageStatus")
                        .HasColumnType("tinyint");

                    b.Property<byte>("MessageType")
                        .HasColumnType("tinyint");

                    b.Property<int>("RetryCount")
                        .HasColumnType("int");

                    b.Property<string>("SourceRawJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUniqueId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValue("True");

                    b.Property<DateTime?>("UpdatedDate")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("MessageQueue");
                });
#pragma warning restore 612, 618
        }
    }
}
