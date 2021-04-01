using CarParkFeesEngine.API.Helpers;
using CarParkFeesEngine.API.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CarParkFeesEngine.API.Repositories.Implementations
{
    public class HourlyRatesRepository : IHourlyRatesRepository
    {
        public List<HourlyRate> GetHourlyRates()
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var filePath = $"{currentPath}{Constants.ContentsPath}{Constants.HourlyRatesFileName}";

            var hourlyRatesJsonString = FileHelper.GetContentFromFile(filePath);

            return JsonSerializer.FromJson<List<HourlyRate>>(hourlyRatesJsonString);
        }
    }
}
