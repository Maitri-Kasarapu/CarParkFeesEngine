using CarParkFeesEngine.API.Dtos;
using System;

namespace CarParkFeesEngine.API.Services.Interfaces
{
    public interface IHourlyRateCalculatorService
    {
        public CarParkingRateDto CalculateHourlyRate(DateTime entryTime, DateTime exitTime);
    }
}
