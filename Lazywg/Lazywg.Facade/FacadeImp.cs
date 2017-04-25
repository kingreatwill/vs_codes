using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Lazywg.Facade
{
    public class FacadeImp : FacadeObject,IFacade
    {
        public virtual bool Delete<T>(params object[] pkValues)
        {
            throw new NotImplementedException();
        }

        public virtual bool Insert<T>(T t)
        {
            throw new NotImplementedException();
        }

        public virtual T Select<T>(params object[] pkValues)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> SelectByWhere<T>(string sqlWhere)
        {
            throw new NotImplementedException();
        }

        public virtual bool Update<T>(T t)
        {
            throw new NotImplementedException();
        }

        public virtual string GetConnectStr() {
            return ConfigurationManager.AppSettings["ConStr"];
        }
    }
}
