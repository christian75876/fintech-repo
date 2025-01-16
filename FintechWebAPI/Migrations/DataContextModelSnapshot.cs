﻿// <auto-generated />
using System;
using FintechWebAPI.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FintechWebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FintechWebAPI.Class.Cuenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("cuentas");
                });

            modelBuilder.Entity("FintechWebAPI.Entity.FintechTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DestinationAccountId")
                        .HasColumnType("int");

                    b.Property<int?>("OriginAccountId")
                        .HasColumnType("int");

                    b.Property<int>("TypeTransaction")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAccountId");

                    b.HasIndex("OriginAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FintechWebAPI.Entity.FintechTransaction", b =>
                {
                    b.HasOne("FintechWebAPI.Class.Cuenta", "DestinationAccount")
                        .WithMany("DestinationTransactions")
                        .HasForeignKey("DestinationAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FintechWebAPI.Class.Cuenta", "OriginAccount")
                        .WithMany("OriginTransactions")
                        .HasForeignKey("OriginAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("DestinationAccount");

                    b.Navigation("OriginAccount");
                });

            modelBuilder.Entity("FintechWebAPI.Class.Cuenta", b =>
                {
                    b.Navigation("DestinationTransactions");

                    b.Navigation("OriginTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
