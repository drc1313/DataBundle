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

            modelBuilder.Entity("DataBundle.BL.APIAccounts", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApiHostName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("DocumenationLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("APIAccounts");
                });

            modelBuilder.Entity("DataBundle.BL.SingleAPIRequest", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RequestName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.HasIndex("AccountId");

                    b.ToTable("APIRequest");
                });

            modelBuilder.Entity("DataBundle.BL.Tokens", b =>
                {
                    b.Property<Guid>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Categories")
                        .HasColumnType("int");

                    b.Property<string>("ConfigData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TokenId");

                    b.HasIndex("RequestId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("DataBundle.BL.SingleAPIRequest", b =>
                {
                    b.HasOne("DataBundle.BL.APIAccounts", "APIAccounts")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("APIAccounts");
                });

            modelBuilder.Entity("DataBundle.BL.Tokens", b =>
                {
                    b.HasOne("DataBundle.BL.SingleAPIRequest", "SingleAPIRequest")
                        .WithMany("Tokens")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SingleAPIRequest");
                });

            modelBuilder.Entity("DataBundle.BL.SingleAPIRequest", b =>
                {
                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
