using CarParkFeesEngine.API.Enumerations;
using System.Reflection;
using System.Resources;

namespace CarParkFeesEngine.API.Resources
{
    public static class LocalisationMessage
    {
        /// <summary>
        /// Get Error message value from Resource File non-property specific.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string GetErrorMsg(ErrorCode errorCode)
        {
            ResourceManager rm = new ResourceManager("CarParkFeesEngine.API.Resources.Resource", Assembly.GetExecutingAssembly());
            return rm.GetString(errorCode.ToString());
        }
    }
}
