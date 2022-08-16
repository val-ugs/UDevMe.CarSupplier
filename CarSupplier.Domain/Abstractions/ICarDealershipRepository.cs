using CarSupplier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.Domain.Abstractions
{
    public interface ICarDealershipRepository
    {
        void AddCarDealership(CarDealership carDealership);
        List<CarDealership> GetAvailableCarDealerships();
        void AddCarToCarDealership(int carDealershipId, Car car);
        List<CarDealership> GetCarDealerships();
    }
}
