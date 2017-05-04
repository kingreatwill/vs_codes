using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lazywg.Common.Helper
{
    /// <summary>
    /// 请求解析数据帮助
    /// </summary>
    public class RequestParseHelper
    {
        private Dictionary<string, object> data = new Dictionary<string, object>();
        private HttpContext httpContext = null;

        public Dictionary<string, object> GetRequestData(HttpContext context)
        {
            data.Clear();
            this.httpContext = context ?? HttpContext.Current;
            NameValueCollection collection;

            //post data
            if (this.httpContext.Request.HttpMethod.ToUpper().Equals("POST"))
            {
                collection = this.httpContext.Request.Form;
                foreach (string k in collection)
                {
                    object v = collection[k];
                    this.SetParameter(k, v);
                }
            }

            //query string
            collection = this.httpContext.Request.QueryString;
            foreach (string k in collection)
            {
                object v = collection[k];
                this.SetParameter(k, v);
            }

            return data;
        }


        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetParameter(string key, object value)
        {
            if (!data.ContainsKey(key))
                data.Add(key, value);
        }

        public Dictionary<string, object> GetParamDict()
        {
            return data;
        }
    }
}
