using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common.Extend
{
    /// <summary>
    /// 时间类型扩展
    /// </summary>
    public static class DateTimeExtend
    {
        /// <summary>
        /// 日期转yyyymmdd
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSimpleDateStr1(this DateTime dt) {
            return dt.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 日期转yyyy-MM-dd
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSimpleDateStr2(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 日期转yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSimpleDateStr3(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 日期转yyyy-MM-dd HH:mm:ss:fffffff
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToLongDateStr(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }
    }
}
