using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{

    /// <summary>
    /// 实体SqlParam特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlParamAttribute : Attribute
    {
        /// <summary>
        /// 是否作为参数
        /// </summary>
        public bool IsParam { get; set; }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPK { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 方向 为了分页类的DataCount加的
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public SqlDbType SqlType { get; set; }
    }
}
