using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{

    /// <summary>
    /// 不作为SqlParam特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlParamAttribute : Attribute
    {
        public bool IsParam { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public ParameterDirection Direction { get; set; }

        public SqlDbType SqlType { get; set; }
    }
}
