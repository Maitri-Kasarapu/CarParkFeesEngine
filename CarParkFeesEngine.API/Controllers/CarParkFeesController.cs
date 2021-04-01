using CarParkFeesEngine.API.Dtos;
using CarParkFeesEngine.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarParkFeesEngine.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarParkFeesController : ControllerBase
    {
        private readonly ICarParkFeesCalculatorService _CarParkFeesCalculatorService;

        public CarParkFeesController(ICarParkFeesCalculatorService CarParkFeesCalculatorService)
        {
            _CarParkFeesCalculatorService = CarParkFeesCalculatorService;
        }

        [HttpGet]
        public CarParkingRateDto GetCarParkingRate([FromQuery]CarParkingTimeInputDto carParkingTimeDto) => 
             _CarParkFeesCalculatorService.CalculateCarParkFees(carParkingTimeDto);
    }
}
