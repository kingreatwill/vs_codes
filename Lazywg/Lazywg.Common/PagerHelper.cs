using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common
{
    /// <summary>
    /// 分页帮助类
    /// </summary>
    public class PagerHelper
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 开始位置
        /// </summary>
        public int StartIndex { get { return PageSize * (PageIndex-1); } }

        /// <summary>
        /// 结束位置
        /// </summary>
        public int EndIndex { get { return PageIndex * PageSize; } }

        /// <summary>
        /// 数据条数
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount { get; set; }
    }
}
