using CarParkFeesEngine.API.Dtos;
using CarParkFeesEngine.API.Helpers;
using CarParkFeesEngine.API.Repositories.Implementations;
using CarParkFeesEngine.API.Services.Interfaces;
using System;
using System.Linq;

namespace CarParkFeesEngine.API.Services.Implementations
{
    public class HourlyRateCalculatorService : IHourlyRateCalculatorService
    {
        private readonly IHourlyRatesRepository _hourlyRatesRepository;
        public HourlyRateCalculatorService(IHourlyRatesRepository hourlyRatesRepository)
        {
            _hourlyRatesRepository = hourlyRatesRepository;
        }
        public CarParkingRateDto CalculateHourlyRate(DateTime entryTime, DateTime exitTime)
        {
            var hourlyRates = _hourlyRatesRepository.GetHourlyRates();

            var dto = new CarParkingRateDto { PackageName = Constants.HourlyRatesName };

            var fullDayParkingRate = 0.0;
            var remainingHoursRate = 0.0;
            var parkedHours = (exitTime - entryTime).TotalHours;

            var maxHourlyRate = hourlyRates.OrderByDescending(h => h.MaxAllowedHours).FirstOrDefault();

            if (parkedHours > maxHourlyRate.MaxAllowedHours)
            {
                fullDayParkingRate = Math.Floor(parkedHours / maxHourlyRate.MaxAllowedHours) * maxHourlyRate.TotalPrice;
                parkedHours = parkedHours % maxHourlyRate.MaxAllowedHours;
            }

            if (parkedHours > 0)
                remainingHoursRate = hourlyRates.Where(h => parkedHours <= h.MaxAllowedHours).OrderBy(h => h.MaxAllowedHours).FirstOrDefault().TotalPrice;

            dto.TotalPrice = fullDayParkingRate + remainingHoursRate;

            return dto;
        }
    }
}
