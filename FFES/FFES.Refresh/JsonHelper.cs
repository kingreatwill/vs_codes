using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FFES.Refresh
{
    public class JsonHelper
    {
        /// <summary>
        /// 返回字典
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Dictionary<string, string> DeserializeJsonToDict(string json)
        {
            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return dict;
        }

        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            List<T> t = new List<T>();
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                object o = serializer.Deserialize(reader, typeof(List<T>));
                t = o as List<T>;
                return t;
            }
        }
    }
}
