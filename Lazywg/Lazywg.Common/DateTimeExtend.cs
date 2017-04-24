using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common
{
    /// <summary>
    /// 时间扩展
    /// </summary>
    public static class DateTimeExtend
    {
        /// <summary>
        /// 日期转yyyy-mm-dd字符串
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToShortSimpleDateString(this DateTime time) {
            return time.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 日期转yyyy-mm-dd hh:mm:ss字符串
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToSimpleDateString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
