using CarSupplier.BusinessLogic.Exceptions;
using CarSupplier.BusinessLogic.Services;
using CarSupplier.Domain.Abstractions;
using CarSupplier.Domain.Models;
using Moq;

namespace CarSupplier.Tests
{
    public class Tests
    {
        private CarDealershipService _carDealershipService;
        private Mock<ICarDealershipRepository> _carDealershipRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _carDealershipRepositoryMock = new Mock<ICarDealershipRepository>();
            _carDealershipService = new CarDealershipService(_carDealershipRepositoryMock.Object);
        }

        [Test]
        public void AddCarDealership_ShouldReturnTrue()
        {
            // arrange
            CarDealership carDealership = new CarDealership()
            {
                Name = "Автосалон 3",
                MaxNumberOfCars = 10
            };
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarDealership(carDealership))
                .Verifiable();

            // act
            _carDealershipService.AddCarDealership(carDealership);

            // assert
            _carDealershipRepositoryMock.Verify(x => x.AddCarDealership(carDealership), Times.Once);
        }

        [Test]
        public void AddCarDealership_DoesNotExist_ShouldThrowArgumentNullException()
        {
            // arrange
            CarDealership carDealership = null;
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarDealership(carDealership))
                .Verifiable();

            // act
            Assert.Throws<ArgumentNullException>(() => _carDealershipService.AddCarDealership(carDealership));

            // assert
            _carDealershipRepositoryMock.Verify(x => x.AddCarDealership(carDealership), Times.Never);
        }

        [TestCase(null, 10)]
        [TestCase("", 10)]
        public void AddCarDealership_NotValid_ShouldThrowArgumentNullException(string name, int maxNumberOfCars)
        {
            // arrange
            CarDealership carDealership = new CarDealership()
            {
                Name = name,
                MaxNumberOfCars = maxNumberOfCars
            };
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarDealership(carDealership))
                .Verifiable();

            // act
            Assert.Throws<ArgumentNullException>(() => _carDealershipService.AddCarDealership(carDealership));

            // assert
            _carDealershipRepositoryMock.Verify(x => x.AddCarDealership(carDealership), Times.Never);
        }


        [TestCase("Автосалон 3", 0)]
        public void AddCarDealership_NotValid_ShouldThrowArgumentException(string name, int maxNumberOfCars)
        {
            // arrange
            CarDealership carDealership = new CarDealership()
            {
                Name = name,
                MaxNumberOfCars = maxNumberOfCars
            };
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarDealership(carDealership))
                .Verifiable();

            // act
            Assert.Throws<Exception>(() => _carDealershipService.AddCarDealership(carDealership));

            // assert
            _carDealershipRepositoryMock.Verify(x => x.AddCarDealership(carDealership), Times.Never);
        }

        [Test]
        public void AddCar_Equal_ShouldReturnTrue()
        {
            // arrange
            List<CarDealership> carDealerships = GetListOfCarDealerships();
            Car newCar = new Car()
            {
                Id = 9,
                Brand = "Bmw",
                Color = "серая"
            };

            _carDealershipRepositoryMock
                .Setup(x => x.GetAvailableCarDealerships())
                .Returns(carDealerships)
                .Verifiable();
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarToCarDealership(It.IsAny<int>(), It.Is<Car>(y => y == newCar)))
                .Verifiable();

            // act
            _carDealershipService.AddCar(newCar);

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetAvailableCarDealerships(), Times.Once);
            _carDealershipRepositoryMock.Verify(x => x.AddCarToCarDealership(It.IsAny<int>(), newCar), Times.Once);
        }

        [Test]
        public void AddCar_LessOne_ShouldReturnTrue()
        {
            // arrange
            List<CarDealership> carDealerships = GetListOfCarDealerships();
            Car newCar = new Car()
            {
                Id = 9,
                Brand = "Bmw",
                Color = "серая"
            };
            Car newCar2 = new Car()
            {
                Id = 10,
                Brand = "Bmw",
                Color = "серая"
            };
            carDealerships[0].Cars.Add(newCar);

            _carDealershipRepositoryMock
                .Setup(x => x.GetAvailableCarDealerships())
                .Returns(carDealerships)
                .Verifiable();
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarToCarDealership(It.IsAny<int>(), It.Is<Car>(y => y == newCar2)))
                .Verifiable();

            // act
            _carDealershipService.AddCar(newCar2);

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetAvailableCarDealerships(), Times.Once);
            _carDealershipRepositoryMock.Verify(x => x.AddCarToCarDealership(It.IsAny<int>(), newCar2), Times.Once);
        }

        [Test]
        public void AddCar_LessTwo_ShouldReturnTrue()
        {
            // arrange
            List<CarDealership> carDealerships = GetListOfCarDealerships();
            Car newCar = new Car()
            {
                Id = 9,
                Brand = "Bmw",
                Color = "серая"
            };
            Car newCar2 = new Car()
            {
                Id = 10,
                Brand = "Bmw",
                Color = "серая"
            };
            Car newCar3 = new Car()
            {
                Id = 11,
                Brand = "Bmw",
                Color = "серая"
            };
            carDealerships[0].Cars.AddRange(new[] { newCar, newCar2 });

            _carDealershipRepositoryMock
                .Setup(x => x.GetAvailableCarDealerships())
                .Returns(carDealerships)
                .Verifiable();
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarToCarDealership(It.IsAny<int>(), It.Is<Car>(y => y == newCar3)))
                .Verifiable();

            // act
            _carDealershipService.AddCar(newCar3);

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetAvailableCarDealerships(), Times.Once);
            _carDealershipRepositoryMock.Verify(x => x.AddCarToCarDealership(It.IsAny<int>(), newCar3), Times.Once);
        }

        [Test]
        public void AddCar_LessThree_ShouldReturnTrue()
        {
            // arrange
            List<CarDealership> carDealerships = GetListOfCarDealerships();
            Car newCar = new Car()
            {
                Id = 9,
                Brand = "Bmw",
                Color = "серая",

            };
            Car newCar2 = new Car()
            {
                Id = 10,
                Brand = "Bmw",
                Color = "серая"
            };
            Car newCar3 = new Car()
            {
                Id = 11,
                Brand = "Bmw",
                Color = "серая"
            };
            Car newCar4 = new Car()
            {
                Id = 12,
                Brand = "Bmw",
                Color = "серая"
            };
            carDealerships[0].Cars.AddRange(new[] { newCar, newCar2, newCar3 });

            _carDealershipRepositoryMock
                .Setup(x => x.GetAvailableCarDealerships())
                .Returns(carDealerships)
                .Verifiable();
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarToCarDealership(It.IsAny<int>(), It.Is<Car>(y => y == newCar4)))
                .Verifiable();

            // act
            _carDealershipService.AddCar(newCar4);

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetAvailableCarDealerships(), Times.Once);
            _carDealershipRepositoryMock.Verify(x => x.AddCarToCarDealership(2, newCar4), Times.Once);
        }

        [Test]
        public void AddCar_DoesNotExist_ShouldThrowArgumentNullException()
        {
            // arrange
            List<CarDealership> carDealerships = GetListOfCarDealerships();
            Car newCar = null;

            _carDealershipRepositoryMock
                .Setup(x => x.GetAvailableCarDealerships())
                .Returns(carDealerships)
                .Verifiable();
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarToCarDealership(It.IsAny<int>(), It.Is<Car>(y => y == newCar)))
                .Verifiable();

            // act
            Assert.Throws<ArgumentNullException>(() => _carDealershipService.AddCar(newCar));

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetAvailableCarDealerships(), Times.Never);
            _carDealershipRepositoryMock.Verify(x => x.AddCarToCarDealership(It.IsAny<int>(), newCar), Times.Never);
        }

        [TestCase(null, "оранжевая")]
        [TestCase("", "оранжевая")]
        [TestCase("Bmv", null)]
        [TestCase("Bmv", "")]
        public void AddCar_NotValid_ShouldThrowArgumentNullException(string brand, string color)
        {
            // arrange
            List<CarDealership> carDealerships = GetListOfCarDealerships();
            Car newCar = new Car
            {
                Brand = brand,
                Color = color
            };

            _carDealershipRepositoryMock
                .Setup(x => x.GetAvailableCarDealerships())
                .Returns(carDealerships)
                .Verifiable();
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarToCarDealership(It.IsAny<int>(), It.Is<Car>(y => y == newCar)))
                .Verifiable();

            // act
            Assert.Throws<ArgumentNullException>(() => _carDealershipService.AddCar(newCar));

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetAvailableCarDealerships(), Times.Never);
            _carDealershipRepositoryMock.Verify(x => x.AddCarToCarDealership(It.IsAny<int>(), newCar), Times.Never);
        }

        [Test]
        public void AddCar_ShouldThrowNotFoundException()
        {
            // arrange
            List<CarDealership> carDealerships = GetListOfCarDealerships();
            Car newCar = new Car()
            {
                Id = 9,
                Brand = "Bmw",
                Color = "серая"
            };

            _carDealershipRepositoryMock
                .Setup(x => x.GetAvailableCarDealerships())
                .Verifiable();
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarToCarDealership(It.IsAny<int>(), It.Is<Car>(y => y == newCar)))
                .Verifiable();

            // act
            Assert.Throws<NotFoundException>(() => _carDealershipService.AddCar(newCar));

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetAvailableCarDealerships(), Times.Once);
            _carDealershipRepositoryMock.Verify(x => x.AddCarToCarDealership(It.IsAny<int>(), newCar), Times.Never);
        }

        [Test]
        public void AddCar_ShouldThrowException()
        {
            // arrange
            Car newCar = new Car()
            {
                Id = 9,
                Brand = "Bmw",
                Color = "серая"
            };

            _carDealershipRepositoryMock
                .Setup(x => x.GetAvailableCarDealerships())
                .Returns(new List<CarDealership>())
                .Verifiable();
            _carDealershipRepositoryMock
                .Setup(x => x.AddCarToCarDealership(It.IsAny<int>(), It.Is<Car>(y => y == newCar)))
                .Verifiable();

            // act
            Assert.Throws<Exception>(() => _carDealershipService.AddCar(newCar));

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetAvailableCarDealerships(), Times.Once);
            _carDealershipRepositoryMock.Verify(x => x.AddCarToCarDealership(It.IsAny<int>(), newCar), Times.Never);
        }

        [Test]
        public void GetCarDealerships_ShouldReturnTrue()
        {
            // arrange
            List<CarDealership> carDealerships = GetListOfCarDealerships();

            _carDealershipRepositoryMock.Setup(x => x.GetCarDealerships())
                .Returns(() => carDealerships)
                .Verifiable();

            // act
            var result = _carDealershipService.GetCarDealerships();

            // assert
            _carDealershipRepositoryMock.Verify(x => x.GetCarDealerships(), Times.Once);
            Assert.IsTrue(result == carDealerships);
        }

        private List<CarDealership> GetListOfCarDealerships()
        {
            Car greyMercedesCar = new Car()
            {
                Id = 1,
                Brand = "Mercedes",
                Color = "серая"
            };
            Car orangeMercedesCar = new Car()
            {
                Id = 2,
                Brand = "Mercedes",
                Color = "оранжевая"
            };
            Car greyBmwCar = new Car()
            {
                Id = 3,
                Brand = "Bmw",
                Color = "серая"
            };
            Car orangeBmwCar = new Car()
            {
                Id = 4,
                Brand = "Bmw",
                Color = "оранжевая"
            };
            Car greyMercedesCar2 = new Car()
            {
                Id = 5,
                Brand = "Mercedes",
                Color = "серая"
            };
            Car orangeMercedesCar2 = new Car()
            {
                Id = 6,
                Brand = "Mercedes",
                Color = "оранжевая"
            };
            Car greyBmwCar2 = new Car()
            {
                Id = 7,
                Brand = "Bmw",
                Color = "серая"
            };
            Car orangeBmwCar2 = new Car()
            {
                Id = 8,
                Brand = "Bmw",
                Color = "оранжевая"
            };

            CarDealership carDealership = new CarDealership()
            {
                Id = 1,
                Name = "Автосалон 1",
                MaxNumberOfCars = 100,
                Cars = new List<Car>()
                {
                    greyMercedesCar,
                    orangeMercedesCar,
                    greyBmwCar,
                    orangeBmwCar
                }
            };
            CarDealership carDealership2 = new CarDealership()
            {
                Id = 2,
                Name = "Автосалон 2",
                MaxNumberOfCars = 100,
                Cars = new List<Car>()
                {
                    greyMercedesCar2,
                    orangeMercedesCar2,
                    greyBmwCar2,
                    orangeBmwCar2
                }
            };
            List<CarDealership> carDealerships = new List<CarDealership>()
            {
                carDealership, carDealership2
            };

            return carDealerships;
        }
    }
}