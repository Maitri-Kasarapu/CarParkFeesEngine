using CarParkFeesEngine.API.Helpers;
using CarParkFeesEngine.API.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CarParkFeesEngine.API.Repositories.Implementations
{
    public class PackageRatesRepository : IPackageRatesRepository
    {
        public List<PackageRate> GetPackageRates()
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var filePath = $"{currentPath}{Constants.ContentsPath}{Constants.PackageRatesFileName}";

            var packageRatesJsonString = FileHelper.GetContentFromFile(filePath);

            return JsonSerializer.FromJson<List<PackageRate>>(packageRatesJsonString);
        }
    }
}
