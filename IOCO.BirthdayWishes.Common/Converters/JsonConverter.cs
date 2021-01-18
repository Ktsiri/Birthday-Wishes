using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BirthdayWishes.Common.Converters
{
    public static class JsonConverter
    {
        public static T ConvertFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static string ConvertToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
        }
    }
}
