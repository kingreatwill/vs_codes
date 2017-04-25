using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    public class SqlParam
    {
        public string Param { get; set; }

        public string Value { get; set; }

        public DbType SqlType { get; set; }

        public ParameterDirection Direction { get; set; }

        public int Size { get; set; }
    }
}
