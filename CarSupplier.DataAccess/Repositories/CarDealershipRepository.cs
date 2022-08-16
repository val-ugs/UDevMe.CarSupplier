using AutoMapper;
using CarSupplier.Domain.Abstractions;
using CarSupplier.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.DataAccess.MSSQL.Repositories
{
    /// <summary>
    /// Car Dealership Repository performs actions with car dealerships
    /// </summary>
    public class CarDealershipRepository : ICarDealershipRepository
    {
        private readonly CarSupplierContext _context;
        private readonly IMapper _mapper;

        public CarDealershipRepository(CarSupplierContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Add car dealership
        /// </summary>
        /// <param name="carDealership"></param>
        public void AddCarDealership(CarDealership carDealership)
        {
            _context.CarDealerships.Add(_mapper.Map<Entities.CarDealership>(carDealership));

            _context.SaveChanges();
        }

        /// <summary>
        /// Get available car dealerships that have free places
        /// </summary>
        /// <returns> car dealerships </returns>
        public List<CarDealership> GetAvailableCarDealerships()
        {
            return _context.CarDealerships
                .Include(x => x.Cars)
                .Where(x => x.Cars.Count() < x.MaxNumberOfCars)
                .Select(x => _mapper.Map<CarDealership>(x))
                .ToList();
        }
        
        /// <summary>
        /// Add car to car dealership
        /// </summary>
        /// <param name="carDealershipId"></param>
        /// <param name="car"></param>
        public void AddCarToCarDealership(int carDealershipId, Car car)
        {
            var carEntity = _mapper.Map<Entities.Car>(car);
            carEntity.CarDealershipId = carDealershipId;

            _context.Cars.Add(carEntity);

            _context.SaveChanges();
        }

        /// <summary>
        /// Get a list of car dealerships
        /// </summary>
        /// <returns> list of car dealerships </returns>
        public List<CarDealership> GetCarDealerships()
        {
            return _context.CarDealerships
                .Include(x => x.Cars)
                .Select(x => _mapper.Map<CarDealership>(x))
                .ToList();
        }
    }
}
