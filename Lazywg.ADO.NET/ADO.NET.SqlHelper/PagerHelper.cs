using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    public class PagerHelper
    {
        [SqlParam(IsParam = false)]
        public int PageIndex { get; set; }

        [SqlParam(IsParam = false)]
        public int PageSize
        {
            get
            {
                return DataCount % PageSize == 0 ? DataCount / PageSize : DataCount / PageSize + 1;
            }
        }

        [SqlParam(IsParam = false)]
        public int PageCount { get; set; }

        [SqlParam(IsParam = true, Direction = ParameterDirection.Output)]
        public int DataCount { get; set; }

        public int StartIndex
        {
            get
            {
                return PageSize * (PageIndex - 1) + 1;
            }
        }

        public int EndIndex
        {
            get
            {
                return PageSize * PageIndex;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public string Order { get; set; }
    }
}
