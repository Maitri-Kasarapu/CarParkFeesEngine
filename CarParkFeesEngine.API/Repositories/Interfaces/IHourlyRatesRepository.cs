using CarParkFeesEngine.API.Models;
using System.Collections.Generic;

namespace CarParkFeesEngine.API.Repositories.Implementations
{
    public interface IHourlyRatesRepository
    {
        public List<HourlyRate> GetHourlyRates();
    }
}
