using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Lazywg.Common.Helper
{
    public class CacheHelper
    {
        private static Cache cache = new Cache();
        public static DateTime AbsoluteExpiration = LazywgConfigs.NowTime.AddMinutes(20);
        public static TimeSpan SlidingExpiration = TimeSpan.Zero;
        public static CacheItemPriority Priority = CacheItemPriority.AboveNormal;

        public static void AddCache(string key, object value, CacheItemRemovedCallback onRemoveCallback)
        {
            cache.Add(key, value, null, AbsoluteExpiration, SlidingExpiration, Priority, onRemoveCallback);
        }

        public static void InsertCache(string key, object value)
        {
            cache.Insert(key, value, null, AbsoluteExpiration, SlidingExpiration, Priority, null);
        }

        public static void InsertCache(string key, object value, CacheItemRemovedCallback onRemoveCallback)
        {
            cache.Insert(key, value, null, AbsoluteExpiration, SlidingExpiration, Priority, onRemoveCallback);
        }

        public static void InsertCache(string key, object value, CacheItemUpdateCallback onUpdateCallback)
        {
            cache.Insert(key, value, null, AbsoluteExpiration, SlidingExpiration, onUpdateCallback);
        }

        public static T GetCache<T>(string key) where T : class
        {
            return cache.Get(key) as T;
        }

        public static object GetCache(string key)
        {
            return cache.Get(key);
        }

        public static object RemoveCache(string key)
        {
            return cache.Remove(key);
        }
    }
}
