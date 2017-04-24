using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Helper
{
    public class FormateHelper
    {
        /// <summary>
        /// 简单日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string SimpleShortDate(DateTime date) {
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 简单日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string SimpleDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
