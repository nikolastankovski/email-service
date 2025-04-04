﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NxEmailService.DbContexts;

#nullable disable

namespace NxEmailService.Migrations
{
    [DbContext(typeof(NxEmailServiceDbContext))]
    [Migration("20250329205959_InitialNxEmailService")]
    partial class InitialNxEmailService
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NxEmailService.Models.EmailHistory", b =>
                {
                    b.Property<int>("EmailHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmailHistoryID"));

                    b.Property<string>("Attachments")
                        .HasColumnType("text");

                    b.Property<string>("BCC")
                        .HasColumnType("text");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CC")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOnUTC")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("timestamp(3) with time zone")
                        .HasDefaultValueSql("(NOW() AT TIME ZONE 'UTC')");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool?>("IsSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Template")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EmailHistoryID")
                        .HasName("PK_EmailHistory_EmailHistoryID");

                    b.ToTable("EmailHistory", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
