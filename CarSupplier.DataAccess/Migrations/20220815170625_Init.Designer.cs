﻿// <auto-generated />
using CarSupplier.DataAccess.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarSupplier.DataAccess.MSSQL.Migrations
{
    [DbContext(typeof(CarSupplierContext))]
    [Migration("20220815170625_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CarSupplier.DataAccess.MSSQL.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarDealershipId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarDealershipId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Mercedes",
                            CarDealershipId = 1,
                            Color = "серая"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Mercedes",
                            CarDealershipId = 1,
                            Color = "оранжевая"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Bmw",
                            CarDealershipId = 1,
                            Color = "серая"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Bmw",
                            CarDealershipId = 1,
                            Color = "оранжевая"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Mercedes",
                            CarDealershipId = 2,
                            Color = "серая"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "Mercedes",
                            CarDealershipId = 2,
                            Color = "оранжевая"
                        },
                        new
                        {
                            Id = 7,
                            Brand = "Bmw",
                            CarDealershipId = 2,
                            Color = "серая"
                        },
                        new
                        {
                            Id = 8,
                            Brand = "Bmw",
                            CarDealershipId = 2,
                            Color = "оранжевая"
                        });
                });

            modelBuilder.Entity("CarSupplier.DataAccess.MSSQL.Entities.CarDealership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MaxNumberOfCars")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarDealerships");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MaxNumberOfCars = 10,
                            Name = "Автосалон 1"
                        },
                        new
                        {
                            Id = 2,
                            MaxNumberOfCars = 10,
                            Name = "Автосалон 2"
                        });
                });

            modelBuilder.Entity("CarSupplier.DataAccess.MSSQL.Entities.Car", b =>
                {
                    b.HasOne("CarSupplier.DataAccess.MSSQL.Entities.CarDealership", "CarDealership")
                        .WithMany("Cars")
                        .HasForeignKey("CarDealershipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarDealership");
                });

            modelBuilder.Entity("CarSupplier.DataAccess.MSSQL.Entities.CarDealership", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
