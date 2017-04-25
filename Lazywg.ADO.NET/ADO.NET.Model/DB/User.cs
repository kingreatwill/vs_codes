using ADO.NET.Common;
using ADO.NET.SqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Model.DB
{
    public class User
    {
        [SqlParam(IsParam =true,IsPK =true)]
        public string ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
