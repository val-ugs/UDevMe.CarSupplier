using AutoMapper;
using CarSupplier.DataAccess.MSSQL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarSupplier.DataAccess.MSSQL.Tests
{
    public class CarDealerShipRepositoryTests
    {
        private CarDealershipRepository _carDealershipRepository;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<CarSupplierContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarSupplierDB;Trusted_Connection=True;")
                .Options;
            var context = new CarSupplierContext(contextOptions);

            var configuration = new MapperConfiguration(x => x.AddProfile<DataAccessMappingProfile>());
            var mapper = new Mapper(configuration);

            _carDealershipRepository = new CarDealershipRepository(context, mapper);
        }

        [Test]
        public void GetCarDealerships_ShouldReturnIsNotNull()
        {
            // arrange

            // act
            var carDealerships = _carDealershipRepository.GetCarDealerships();

            // assert
            Assert.IsNotNull(carDealerships);
        }
    }
}