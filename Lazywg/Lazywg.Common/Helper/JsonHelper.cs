
/***********************************************************************************************************************
    .NET下几种常见的解析JSON方法
    主要类	                                        命名空间	                            限制	                内建LINQ支持
    DataContractJsonSerializer	                    System.Runtime.Serialization.Json	通用	                    否
    JavaScriptSerializer	                        System.Web.Script.Serialization	    只能在Web环境使用	        否
    JsonArray、JsonObject、 JsonValue	            System.Json	                        只能在Silverlight中使用	是
    JsonConvert、JArray、JObject、JValue、JProperty	Newtonsoft.Json	                    通用	                    是 
 ************************************************************************************************************************/
namespace Lazywg.Common.Helper
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Web.Script.Serialization;

    /// <summary>
    /// json 帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// JavaScriptSerializer 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JSONDeserialize<T>(string json)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            return Serializer.Deserialize<T>(json);
        }

        /// <summary>
        /// JavaScriptSerializer 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string JSONSerializee<T>(object obj)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            return Serializer.Serialize(obj);
        }

        /// <summary>
        /// DataContractJsonSerializer 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// DataContractJsonSerializer 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            //序列化  
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// JsonConvert 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// JsonConvert 序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonSerialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 获取json 对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject GetJObject(string json) {
           return JObject.Parse(json);
        }

        public static JArray GetJArray(string json)
        {
            return JArray.Parse(json);
        }
    }

    public static class JObjectExtend {

        public static string GetKeyValue(this JObject jsonObj,string key) {
            JToken token = null;
            if (jsonObj.TryGetValue(key, out token))
            {
                return (string)token;
            }
            return string.Empty;
        }
    }   
}
