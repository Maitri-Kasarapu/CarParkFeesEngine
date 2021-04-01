using CarParkFeesEngine.API.Enumerations;
using System.Collections.Generic;

namespace CarParkFeesEngine.API.Dtos
{
    public class CarParkingRateDto : BaseDto
    {
        #region Constructor(s)
        public CarParkingRateDto() { }
        public CarParkingRateDto(ErrorCode errorCode) : base(errorCode) { }
        public CarParkingRateDto(List<ErrorCode> errorCodes) : base(errorCodes) { }
        #endregion

        #region Properties
        public double TotalPrice { get; set; }
        public string PackageName { get; set; }
        #endregion
    }
}
