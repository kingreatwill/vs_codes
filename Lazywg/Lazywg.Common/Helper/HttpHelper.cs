using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lazywg.Common.Helper
{
    /// <summary>
    /// http 请求帮助类
    /// </summary>
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
            return HttpGet(url, GetStrParams(paramDict));
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (string.IsNullOrEmpty(context) ? string.Empty : "?" + context));
                request.Method = "Get";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return GetResponseString(response);
            }
            catch (Exception e)
            {
                throw e;
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
            return HttpPost(url, paramDict, Encoding.UTF8);
        }

        /// <summary>
        /// http Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramDict"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> paramDict, Encoding encoding)
        {
            return HttpPost(url, GetStrParams(paramDict), encoding);
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
                webReq.ContentType = "application/x-www-form-urlencoded";//表单默认数据提交格式

                webReq.ContentLength = byteArray.Length;
                Stream stream = webReq.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);//写入参数
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                return GetResponseString(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string HttpClientGet(string url, Dictionary<string, string> paramDict)
        {
            return HttpClientGet(url, GetStrParams(paramDict));
        }

        public static string HttpClientGet(string url, string content)
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            HttpClient httpClient = new HttpClient();
            url = url + (string.IsNullOrEmpty(content) ? string.Empty : "?" + content);
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return string.Empty;
        }

        public static string HttpClientPost(string url, string context)
        {
            return HttpClientPost(url, context, Encoding.UTF8);
        }

        public static string HttpClientPost(string url, Dictionary<string, string> paramDict)
        {
            return HttpClientPost(url, paramDict, Encoding.UTF8);
        }

        public static string HttpClientPost(string url, Dictionary<string, string> paramDict, Encoding encoding)
        {
            return HttpClientPost(url, GetStrParams(paramDict), encoding);
        }

        public static string HttpClientPost(string url, string context, Encoding encoding)
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            HttpContent httpContent = new StringContent(context);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");//表单默认数据提交格式

            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return string.Empty;
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
                builder.AppendFormat("{0}={1}&", item.Key, HttpUtility.UrlEncode(item.Value));
            }
            string result = builder.ToString();
            return result.Substring(0, result.Length - 1);
        }
    }
}
