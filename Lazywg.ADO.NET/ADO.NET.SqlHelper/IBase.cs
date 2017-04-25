using System.Collections.Generic;

namespace ADO.NET.SqlHelper
{
    public interface IBase
    {
        bool Insert<T>(T t);
        bool Delete<T>(List<SqlParam> parms);
        bool Update<T>(T t);
        T Select<T>(List<SqlParam> parms);
        List<T> SelectByWhere<T>(List<SqlParam> parms);
        List<T> SelectByPager<T>(List<SqlParam> parms, PagerHelper pager);
    }
}
