using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    public class SqlTextHelper
    {
        public static string GetInsertSqlText<T>(T t)
        {
            throw new NotImplementedException();
        }

        public static string GetUpdateSqlText<T>(T t)
        {
            throw new NotImplementedException();
        }

        public static string GetDeleteSqlText<T>(params object[] pkValues)
        {
            throw new NotImplementedException();
        }

        public static string GetSelectSqlText<T>(string sqlWhere)
        {
            throw new NotImplementedException();
        }

        public static string GetSelectPagerSqlText<T>(string sqlWhere,PagerHelper pager)
        {
            throw new NotImplementedException();
        }
    }
}
