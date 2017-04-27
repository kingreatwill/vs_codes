using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common
{
    public static class DateTimeExtend
    {
        public static string ToSimpleDateStr(this DateTime dt) {
            return dt.ToString("yyyyMMdd");
        }

        public static string ToLongDateStr(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }
    }
}
