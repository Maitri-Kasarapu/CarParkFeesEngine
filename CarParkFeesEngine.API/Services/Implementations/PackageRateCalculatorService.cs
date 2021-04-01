using CarParkFeesEngine.API.Dtos;
using CarParkFeesEngine.API.Repositories.Implementations;
using CarParkFeesEngine.API.Services.Interfaces;
using System;
using System.Linq;

namespace CarParkFeesEngine.API.Services.Implementations
{
    public class PackageRateCalculatorService : IPackageRateCalculatorService
    {
        private readonly IPackageRatesRepository _packageRatesRepository;
        public PackageRateCalculatorService(IPackageRatesRepository packageRatesRepository)
        {
            _packageRatesRepository = packageRatesRepository;
        }
        public CarParkingRateDto CalculatePackageRate(DateTime entryTime, DateTime exitTime)
        {
            var packageRates = _packageRatesRepository.GetPackageRates();

            var dto = new CarParkingRateDto();

            foreach (var packageRate in packageRates)
            {
                var entryDayOfWeek = entryTime.DayOfWeek.ToString();
                var exitDayOfWeek = exitTime.DayOfWeek.ToString();

                //Check allowed days condition
                if (packageRate.EntryCondition.ParkingDays.Any(p => p.Equals(entryDayOfWeek, StringComparison.InvariantCultureIgnoreCase)) && packageRate.ExitCondition.ParkingDays.Any(p => p.Equals(exitDayOfWeek, StringComparison.InvariantCultureIgnoreCase)))
                {
                    //Check entry condition
                    if ((packageRate.EntryCondition.StartTime <= entryTime.TimeOfDay && packageRate.EntryCondition.EndTime >= entryTime.TimeOfDay) || 
                        (packageRate.MaxDaysAllowed > 0 && (packageRate.EntryCondition.StartTime <= entryTime.TimeOfDay && entryTime.TimeOfDay <= packageRate.EntryCondition.EndTime.Add(TimeSpan.FromDays(1))) || 
                        (packageRate.EntryCondition.StartTime.Subtract(TimeSpan.FromDays(1)) <= entryTime.TimeOfDay && entryTime.TimeOfDay <= packageRate.EntryCondition.EndTime)))
                    {
                        //alculate max exit date and time
                        var maxExitDateTime = entryTime.Date.AddDays(packageRate.MaxDaysAllowed).Add(packageRate.ExitCondition.EndTime);

                        //Check if exit time falls in the range
                        if (exitTime <= maxExitDateTime)
                        {
                            if (dto.TotalPrice == 0 || dto.TotalPrice > packageRate.TotalPrice)
                            {
                                dto.PackageName = packageRate.Name;
                                dto.TotalPrice = packageRate.TotalPrice;
                            }
                        }
                    }
                }
            }
            return dto;
        }
    }
}
