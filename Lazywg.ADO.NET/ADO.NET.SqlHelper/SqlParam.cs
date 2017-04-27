using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    /// <summary>
    /// 数据库参数
    /// </summary>
    public class SqlParam
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType SqlType { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
