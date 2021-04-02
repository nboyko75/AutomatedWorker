using Newtonsoft.Json;

namespace JsonFlatFileDataStore
{
    public class JsonUtils
    {
        public static string GetJsonOfObject(object obj) 
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
