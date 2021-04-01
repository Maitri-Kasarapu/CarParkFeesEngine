using CarParkFeesEngine.API.Enumerations;
using CarParkFeesEngine.API.Resources;
using System.Collections.Generic;
using System.Linq;

namespace CarParkFeesEngine.API.Dtos
{
    public class BaseDto
    {
        #region Constructor(s)
        public BaseDto() { }

        public BaseDto(ErrorCode errorCode)
        {
            AddError(errorCode);
        }

        public BaseDto(List<ErrorCode> errorCodes)
        {
            foreach (var errorCode in errorCodes)
                AddError(errorCode);
        }
        #endregion

        public List<Error> Errors { get; } = new List<Error>();

        public void AddError(ErrorCode errorCode)
        {
            if (!Errors.Any(x => x.ErrorCode == errorCode))
                Errors.Add(new Error
                {
                    ErrorCode = errorCode,
                    ErrorMessage = LocalisationMessage.GetErrorMsg(errorCode)
                });
        }

        public bool HasErrors()
        {
            return Errors.Count > 0;
        }
    }
}
