using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Common
{
    public class PagerHelper
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int DataCount { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}
