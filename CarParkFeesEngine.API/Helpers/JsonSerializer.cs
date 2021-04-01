using Newtonsoft.Json;

namespace CarParkFeesEngine.API.Helpers
{
    public class JsonSerializer
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T FromJson<T>(object json)
        {
            return JsonConvert.DeserializeObject<T>(json.ToString());
        }
    }
}
