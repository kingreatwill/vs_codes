using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Facade
{
    public interface IFacade
    {
        bool Insert<T>(T t);
        bool Update<T>(T t);
        T Select<T>(params object[] pkValues);
        bool Delete<T>(params object[] pkValues);
        List<T> SelectByWhere<T>(string sqlWhere);
    }
}
