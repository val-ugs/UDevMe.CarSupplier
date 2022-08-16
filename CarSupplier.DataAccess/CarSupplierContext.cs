using CarSupplier.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.DataAccess.MSSQL
{
    public class CarSupplierContext : DbContext
    {
        public DbSet<Car> Cars { get; set; } = null;
        public DbSet<CarDealership> CarDealerships { get; set; } = null;

        public CarSupplierContext(DbContextOptions<CarSupplierContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDealership>()
                .HasKey(cd => cd.Id);

            modelBuilder.Entity<Car>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarDealership)
                .WithMany(cd => cd.Cars)
                .HasForeignKey(c => c.CarDealershipId);

            CarDealership carDealership = new CarDealership()
            {
                Id = 1,
                Name = "Автосалон 1",
                MaxNumberOfCars = 10
            };
            CarDealership carDealership2 = new CarDealership()
            {
                Id = 2,
                Name = "Автосалон 2",
                MaxNumberOfCars = 10
            };

            modelBuilder.Entity<CarDealership>()
                .HasData(new[]
                {
                    carDealership,
                    carDealership2
                });

            Car greyMercedesCar = new Car()
            {
                Id = 1,
                Brand = "Mercedes",
                Color = "серая",
                CarDealershipId = carDealership.Id
            };
            Car orangeMercedesCar = new Car()
            {
                Id = 2,
                Brand = "Mercedes",
                Color = "оранжевая",
                CarDealershipId = carDealership.Id
            };
            Car greyBmwCar = new Car()
            {
                Id = 3,
                Brand = "Bmw",
                Color = "серая",
                CarDealershipId = carDealership.Id
            };
            Car orangeBmwCar = new Car()
            {
                Id = 4,
                Brand = "Bmw",
                Color = "оранжевая",
                CarDealershipId = carDealership.Id
            };
            Car greyMercedesCar2 = new Car()
            {
                Id = 5,
                Brand = "Mercedes",
                Color = "серая",
                CarDealershipId = carDealership2.Id
            };
            Car orangeMercedesCar2 = new Car()
            {
                Id = 6,
                Brand = "Mercedes",
                Color = "оранжевая",
                CarDealershipId = carDealership2.Id
            };
            Car greyBmwCar2 = new Car()
            {
                Id = 7,
                Brand = "Bmw",
                Color = "серая",
                CarDealershipId = carDealership2.Id
            };
            Car orangeBmwCar2 = new Car()
            {
                Id = 8,
                Brand = "Bmw",
                Color = "оранжевая",
                CarDealershipId = carDealership2.Id
            };


            modelBuilder.Entity<Car>()
                .HasData(new[]
                {
                    greyMercedesCar,
                    orangeMercedesCar,
                    greyBmwCar,
                    orangeBmwCar,
                    greyMercedesCar2,
                    orangeMercedesCar2,
                    greyBmwCar2,
                    orangeBmwCar2
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
