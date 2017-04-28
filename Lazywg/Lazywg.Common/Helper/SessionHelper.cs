using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lazywg.Common.Helper
{
    public class SessionHelper
    {
        /// <summary>
        /// 获取session值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key) where T : class
        {
            return HttpContext.Current.Session[key] as T;
        }
        /// <summary>
        /// 获取session值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValue(string key)
        {
            return HttpContext.Current.Session[key];
        }

        /// <summary>
        /// 设置session值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}
