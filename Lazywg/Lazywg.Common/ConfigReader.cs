using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common
{
    /// <summary>
    /// 配置项读取并缓存
    /// </summary>
    public class ConfigReader
    {
        private static Dictionary<string, string> _dicts = new Dictionary<string, string>();

        private static readonly object _locker = new object();

        private static DateTime defaultCacheTime = LazywgConfigs.DefaultTime;

        //对外不公开 内部方法为静态方法
        private ConfigReader() { }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            //缓存一小时自动重新获取
            if (defaultCacheTime.AddHours(1) <= LazywgConfigs.NowTime)
            {
                _dicts.Clear();
                defaultCacheTime = LazywgConfigs.NowTime;
            }

            //缓存读取
            string value = _dicts[key];
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }

            //配置项读取
            value = GetConfig(key);
            if (string.IsNullOrEmpty(value))
            {
                //数据库读取
                value = GetDBConfig(key);
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new LazywgException(string.Format("未找到{0}的值",key));
            }

            //加锁处理可能添加
            lock (_locker) {
                if (_dicts.ContainsKey(key))
                {
                    return _dicts[key];
                }
                _dicts.Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// 数据库获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetDBConfig(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 配置项获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 移除一项缓存
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCache(string key) {
            lock (_locker)
            {
                _dicts.Remove(key);
            }
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            lock (_locker)
            {
                _dicts.Clear();
            }
        }
    }
}
