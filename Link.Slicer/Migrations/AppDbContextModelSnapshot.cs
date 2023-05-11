﻿// <auto-generated />
using System;
using Link.Slicer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Link.Slicer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Link.Slicer.Entities.Url", b =>
                {
                    b.Property<Guid>("UrlId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("bigint");

                    b.Property<long?>("DeletedAt")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DomainName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ExpiresIn")
                        .HasColumnType("bigint");

                    b.Property<string>("Protocol")
                        .HasColumnType("text");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UpdatedAt")
                        .HasColumnType("bigint");

                    b.HasKey("UrlId");

                    b.HasIndex("UrlId")
                        .IsUnique();

                    b.ToTable("Urls");
                });
#pragma warning restore 612, 618
        }
    }
}
