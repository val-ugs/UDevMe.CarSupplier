using AutoMapper;
using CarSupplier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.DataAccess.MSSQL
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<CarDealership, Entities.CarDealership>()
                .ReverseMap();

            CreateMap<Car, Entities.Car>()
                .ReverseMap();
        }
    }
}
