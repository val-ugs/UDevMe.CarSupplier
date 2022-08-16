using AutoMapper;
using CarSupplier.Api.ResourceModels;
using CarSupplier.Domain.Abstractions;
using CarSupplier.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarSupplier.Api.Controllers
{
    /// <summary>
    /// Car Dealership Controller performs actions with car dealerships
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CarDealershipController : ControllerBase
    {
        private readonly ICarDealershipService _carDealershipService;
        private readonly IMapper _mapper;

        public CarDealershipController(ICarDealershipService carDealershipService, IMapper mapper)
        {
            _carDealershipService = carDealershipService;
            _mapper = mapper;
        }

        /// <summary>
        /// Add car dealership
        /// </summary>
        /// <param name="carDealership"></param>
        [HttpPost("addcardealership")]
        public ActionResult AddCarDealership(CarDealershipModel carDealership)
        {
            try
            {
                _carDealershipService.AddCarDealership(_mapper.Map<CarDealership>(carDealership));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add car
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost("addcar")]
        public ActionResult AddCar(CarModel car)
        {
            try
            {
                _carDealershipService.AddCar(_mapper.Map<Car>(car));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get car dealerships
        /// </summary>
        /// <returns> car dealerships </returns>
        [HttpGet("get")]
        public ActionResult<List<CarDealership>> Get()
        {
            return _carDealershipService.GetCarDealerships();
        }
    }
}
