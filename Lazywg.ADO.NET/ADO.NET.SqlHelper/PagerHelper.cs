using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    /// <summary>
    /// 分页帮助类
    /// </summary>
    public class PagerHelper
    {
        /// <summary>
        /// 页码
        /// </summary>
        [SqlParam(IsParam = false)]
        public int PageIndex { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        [SqlParam(IsParam = false)]
        public int PageSize { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        [SqlParam(IsParam = false)]
        public int PageCount
        {
            get
            {
                return DataCount % PageSize == 0 ? DataCount / PageSize : DataCount / PageSize + 1;
            }
        }

        /// <summary>
        /// 开始下标
        /// </summary>
        public int StartIndex
        {
            get
            {
                return PageSize * (PageIndex - 1) + 1;
            }
        }

        /// <summary>
        /// 结束下标
        /// </summary>
        public int EndIndex
        {
            get
            {
                return PageSize * PageIndex;
            }
        }

        /// <summary>
        /// 排序 如:Name asc,ID desc
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 数据量
        /// </summary>
        [SqlParam(IsParam = true, Direction = ParameterDirection.Output)]
        public int DataCount { get; set; }

    }
}
