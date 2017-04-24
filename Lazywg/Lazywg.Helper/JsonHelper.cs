
/***********************************************************************************************************************
    .NET下几种常见的解析JSON方法
    主要类	                                        命名空间	                            限制	                内建LINQ支持
    DataContractJsonSerializer	                    System.Runtime.Serialization.Json	通用	                    否
    JavaScriptSerializer	                        System.Web.Script.Serialization	    只能在Web环境使用	        否
    JsonArray、JsonObject、 JsonValue	            System.Json	                        只能在Silverlight中使用	是
    JsonConvert、JArray、JObject、JValue、JProperty	Newtonsoft.Json	                    通用	                    是 
 ************************************************************************************************************************/
namespace Lazywg.Helper
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
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

    /// <summary>
    /// 显式 隐式转换
    /// </summary>
    public class ImplicitExpl{
        public string StrValue { get; set; }

        public ImplicitExpl(string val) {
            this.StrValue = val;
        }

        public static implicit operator ImplicitExpl(string str) {
            return new ImplicitExpl(str);
        }

        public static implicit operator string(ImplicitExpl str)
        {
            return str.StrValue;
        }

        public static explicit operator double(ImplicitExpl str)
        {
            double value = 0;
            if (double.TryParse(str.StrValue,out value))
            {
                return value;
            }
            return 0;
        }
    }

    public class TestImplicit {

        public void Test() {

            //隐式转换
            ImplicitExpl strExt = "nihao";
            //隐式转换
            string str = new ImplicitExpl("nihao");

            //显示转换
            double dou = (double)new ImplicitExpl("123");
            //不支持 未定义该显示转换
            //decimal dec = (decimal)new StringExtend("123");
        }
    }
}
