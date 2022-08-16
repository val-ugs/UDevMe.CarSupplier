using CarSupplier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.Domain.Abstractions
{
    public interface ICarDealershipService
    {
        void AddCarDealership(CarDealership carDealership);
        void AddCar(Car car);
        List<CarDealership> GetCarDealerships();
    }
}
