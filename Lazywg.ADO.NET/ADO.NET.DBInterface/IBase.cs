using ADO.NET.SqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.DBInterface
{
    public interface IBase
    {
        bool Insert<T>(T t);
        bool Delete(string pk);
        bool Update<T>(T t);
        T Select<T>(string pk);
        List<T> SelectByWhere<T>(string sqlWhere);
        List<T> SelectByPager<T>(string sqlWhere,PagerHelper pager);
    }
}
