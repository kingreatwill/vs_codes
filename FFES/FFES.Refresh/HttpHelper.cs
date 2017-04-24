using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Helper
{
    public class HttpHelper
    {
        /// <summary>
        /// http Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramDict"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Dictionary<string, string> paramDict)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + GetStrParams(paramDict));
                request.Method = "Get";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return GetResponseString(response);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// http Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string HttpGet(string url, string context)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + context);
                request.Method = "Get";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return GetResponseString(response);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// http Post请求 utf-8
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramDict"></param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> paramDict)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(GetStrParams(paramDict)); //UTF8Encoding.GetBytes(GetStrParams(paramDict)); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream stream = webReq.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);//写入参数
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                return GetResponseString(response);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// http Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramDict"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpPost(string url,Dictionary<string, string> paramDict,Encoding encoding)
        {
            try
            {
                byte[] byteArray = encoding.GetBytes(paramDict.ToString()); 
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream stream = webReq.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);//写入参数
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                return GetResponseString(response);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// http Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="context"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string context, Encoding encoding)
        {
            try
            {
                byte[] byteArray = encoding.GetBytes(context);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream stream = webReq.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);//写入参数
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                return GetResponseString(response);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取响应的数据
        /// </summary>
        /// <param name="webresponse"></param>
        /// <returns></returns>
        private static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 获取字符串参数
        /// </summary>
        /// <param name="paramDict"></param>
        /// <returns></returns>
        private static string GetStrParams(Dictionary<string, string> paramDict)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in paramDict)
            {
                builder.AppendFormat("{0}={1}&", item.Key, item.Value);
            }
            string result = builder.ToString();
            return result.Substring(0, result.Length - 1);
        }
    }
}
