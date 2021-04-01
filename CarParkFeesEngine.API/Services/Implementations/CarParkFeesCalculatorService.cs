using CarParkFeesEngine.API.Dtos;
using CarParkFeesEngine.API.Services.Interfaces;

namespace CarParkFeesEngine.API.Services.Implementations
{
    public class CarParkFeesCalculatorService : ICarParkFeesCalculatorService
    {
        private readonly IPackageRateCalculatorService _packageRateCalculatorService;
        private readonly IHourlyRateCalculatorService _hourlyRateCalculatorService;
        public CarParkFeesCalculatorService(IPackageRateCalculatorService packageRateCalculatorService, IHourlyRateCalculatorService hourlyRateCalculatorService)
        {
            _packageRateCalculatorService = packageRateCalculatorService;
            _hourlyRateCalculatorService = hourlyRateCalculatorService;
        }
        public CarParkingRateDto CalculateCarParkFees(CarParkingTimeInputDto carParkingTimeInputDto)
        {
            var validationErrors = carParkingTimeInputDto.Validate();
            if (validationErrors.Count > 0)
                return new CarParkingRateDto(validationErrors);

            // calcualte package rate if it falls in that range
            var carParkingRateDto = _packageRateCalculatorService.CalculatePackageRate(carParkingTimeInputDto.EntryTime, carParkingTimeInputDto.ExitTime);

            // calcualte hourly rate for the number of hours
            var dtoByHourlyRate = _hourlyRateCalculatorService.CalculateHourlyRate(carParkingTimeInputDto.EntryTime, carParkingTimeInputDto.ExitTime);

            // decide which rate should be returned based on the total price
            if (dtoByHourlyRate.TotalPrice > 0 && (carParkingRateDto.TotalPrice == 0 || carParkingRateDto.TotalPrice > dtoByHourlyRate.TotalPrice))
                carParkingRateDto = dtoByHourlyRate;

            return carParkingRateDto;
        }
    }
}
