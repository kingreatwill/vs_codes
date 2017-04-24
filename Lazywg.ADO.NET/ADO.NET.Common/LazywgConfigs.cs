using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Common
{
    /// <summary>
    /// 配置
    /// </summary>
    public class LazywgConfigs
    {

        #region guid

        /// <summary>
        /// 默认 guid 字符串
        /// </summary>
        public static string DefaultGuid
        {
            get
            {
                return Guid.Empty.ToString();
            }
        }

        /// <summary>
        /// 新的guid字符串
        /// </summary>
        public static string NewGuidStr
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// 新的guid
        /// </summary>
        public static Guid NewGuid
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        /// <summary>
        /// 随机字符串 guid 去除 -
        /// </summary>
        public static string RandomStr
        {
            get
            {
                return Guid.NewGuid().ToString().Replace("-", string.Empty);
            }
        }

        #endregion

        #region 时间

        /// <summary>
        /// 当前时间
        /// </summary>
        public static DateTime NowTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 默认时间
        /// </summary>
        public static DateTime DefaultTime
        {
            get
            {
                return new DateTime(1970, 1, 1);
            }
        }

        #endregion

        public static string LogPath {
            get {
                return ConfigReader.GetValue("LogPath");
            }
        }
    }
}
