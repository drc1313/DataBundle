﻿// <auto-generated />
using System;
using DataBundle.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataBundle.BL.APIAccount", b =>
                {
                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("DateFormat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Delimiter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumenationLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountName");

                    b.ToTable("APIAccount");
                });

            modelBuilder.Entity("DataBundle.BL.APIRequest", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExpectedProperty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.HasIndex("AccountName");

                    b.ToTable("APIRequest");
                });

            modelBuilder.Entity("DataBundle.BL.APIUsage", b =>
                {
                    b.Property<Guid>("UsageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CurrentUsage")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastCallDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxUsage")
                        .HasColumnType("int");

                    b.Property<int>("UsageDuration")
                        .HasColumnType("int");

                    b.HasKey("UsageID");

                    b.HasIndex("AccountName");

                    b.ToTable("APIUsage");
                });

            modelBuilder.Entity("DataBundle.BL.APIRequest", b =>
                {
                    b.HasOne("DataBundle.BL.APIAccount", "APIAccount")
                        .WithMany()
                        .HasForeignKey("AccountName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("APIAccount");
                });

            modelBuilder.Entity("DataBundle.BL.APIUsage", b =>
                {
                    b.HasOne("DataBundle.BL.APIAccount", "APIAccount")
                        .WithMany()
                        .HasForeignKey("AccountName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("APIAccount");
                });
#pragma warning restore 612, 618
        }
    }
}
