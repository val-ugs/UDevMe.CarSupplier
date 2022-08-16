using AutoMapper;
using CarSupplier.Api.ResourceModels;
using CarSupplier.Domain.Models;

namespace CarSupplier.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CarDealershipModel, CarDealership>();
            CreateMap<CarModel, Car>();
        }
    }
}
