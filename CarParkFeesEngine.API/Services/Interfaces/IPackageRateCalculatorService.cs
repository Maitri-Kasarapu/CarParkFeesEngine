using CarParkFeesEngine.API.Dtos;
using System;

namespace CarParkFeesEngine.API.Services.Interfaces
{
    public interface IPackageRateCalculatorService
    {
        public CarParkingRateDto CalculatePackageRate(DateTime entryTime, DateTime exitTime);
    }
}
