using CarParkFeesEngine.API.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CarParkFeesEngine.API.Dtos
{
    public class Error
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
