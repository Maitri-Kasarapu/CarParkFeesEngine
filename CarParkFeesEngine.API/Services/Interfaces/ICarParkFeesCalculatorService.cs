using CarParkFeesEngine.API.Dtos;

namespace CarParkFeesEngine.API.Services.Interfaces
{
    public interface ICarParkFeesCalculatorService
    {
        public CarParkingRateDto CalculateCarParkFees(CarParkingTimeInputDto carParkingTimeInputDto);
    }
}
