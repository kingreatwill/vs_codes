using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Facade
{
    public abstract class FacadeObject : IDisposable
    {
        private string _name;

        public string Name {
            get { return this._name; }
            set { this._name = value; }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public T Wrap<T>() {
            Type type = typeof(T);
            if (!type.IsInterface)
            {
                throw new NotSupportedException(string.Format("泛型【{0}】不是接口类型",type));
            }
            if (this.GetType().GetInterfaceMap(type).InterfaceType==null)
            {
                throw new NotSupportedException(string.Format("对象没有实现接口【{0}】", type));
            }

            TransparentProxy transp = new TransparentProxy();
            if (transp == null)
            {
                throw new NotSupportedException(string.Format("泛型【{0}】未配置正面接口类型", type));
            }
            else {
                transp.SetRealProxy(this);
                return (T)(transp as object);
            }
        }
    }
}
