using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ADO.NET.Common
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public class CookieHelper
    {
        #region 获取

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
            {
                return GetCookieValue(cookieName, null);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName, string key)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
            {
                return GetCookieValue(request.Cookies[cookieName], key);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookieValue(HttpCookie cookie, string key)
        {
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                {
                    return cookie.Values[key];
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static HttpCookie GetCookie(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
            {
                return request.Cookies[cookieName];
            }
            return null;
        }

        #endregion

        /// <summary>
        /// 移除cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        public static void RemoveCookie(string cookieName, string key)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                    {
                        cookie.Values.Remove(key);
                    }
                    else
                    {
                        cookie.Expires = LazywgConfigs.DefaultTime;//立即过期
                    }
                }
            }
        }

        #region 设置

        public static void SetCookie(string cookieName,string key,string value) {
            SetCookie(cookieName, key, value, null);
        }

        public static void SetCookie(string key, string value)
        {
            SetCookie(key, null, value, null);
        }

        public static void SetCookie(string key, string value,DateTime? expires)
        {
            SetCookie(key, null, value, expires);
        }

        public static void SetCookie(string cookieName, string key,string value, DateTime? expires) {

            HttpResponse response = HttpContext.Current.Response;
            if (response!=null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                    {
                        cookie.Values.Set(key, value);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            cookie.Value = value;
                        }
                        if (expires != null)
                        {
                            cookie.Expires = expires.Value;
                        }
                    }
                }
                else {
                    AddCookie(cookieName, key, value, expires.Value);
                }
            }
        }

        #endregion

        #region 添加

        public static void AddCookie(string key, string value)
        {
            AddCookie(new HttpCookie(key, value));
        }

        public static void AddCookie(string key, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            cookie.Expires = expires;
            AddCookie(cookie);
        }

        public static void AddCookie(string cookieName, string key, string value)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Add(key, value);
            AddCookie(cookie);
        }

        public static void AddCookie(string cookieName, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = expires;
            AddCookie(cookie);
        }

        public static void AddCookie(string cookieName, string key, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Add(key, value);
            cookie.Expires = expires;
            AddCookie(cookie);
        }
      
        public static void AddCookie(HttpCookie cookie) {

            HttpResponse response = HttpContext.Current.Response;
            if (response!=null)
            {
                cookie.HttpOnly = true;
                cookie.Path = "/";//指定统一的路径 以便通存通取
                //cookie.Domain = "*.lazywg.com";
                response.AppendCookie(cookie);//设置跨域这样在其他二级域名下就可以访问了
            }
        }

        #endregion
    }
}
