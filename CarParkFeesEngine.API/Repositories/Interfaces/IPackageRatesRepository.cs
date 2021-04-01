using CarParkFeesEngine.API.Models;
using System.Collections.Generic;

namespace CarParkFeesEngine.API.Repositories.Implementations
{
    public interface IPackageRatesRepository
    {
        public List<PackageRate> GetPackageRates();
    }
}
