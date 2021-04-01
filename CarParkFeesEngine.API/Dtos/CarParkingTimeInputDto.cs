using CarParkFeesEngine.API.Enumerations;
using System;
using System.Collections.Generic;

namespace CarParkFeesEngine.API.Dtos
{
    public class CarParkingTimeInputDto
    {
        #region Properties
        public DateTime EntryTime { get; set; }     

        public DateTime ExitTime { get; set; }
        #endregion

        #region Helpers
        public List<ErrorCode> Validate()
        {
            var errorCodes = new List<ErrorCode>();

            if (EntryTime == DateTime.MinValue)
                errorCodes.Add(ErrorCode.CAR_PARKING_ENTRY_TIME_IS_REQUIRED);

            if (ExitTime == DateTime.MinValue)
                errorCodes.Add(ErrorCode.CAR_PARKING_EXIT_TIME_IS_REQUIRED);

            if (EntryTime > ExitTime)
                errorCodes.Add(ErrorCode.CAR_PARKING_DATE_RANGE_IS_NOT_VALID);

            return errorCodes;
        }
        #endregion
    }
}
