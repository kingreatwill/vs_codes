using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{

    /// <summary>
    /// 不作为SqlParam特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotSqlParamAttribute : Attribute
    {

    }
}
