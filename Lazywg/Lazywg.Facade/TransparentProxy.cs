using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Facade
{
    public delegate object InvokeHandler(MethodInfo method, Delegate dgt, params object[] pams);

    public class TransparentProxy : IDisposable
    {
        protected object _realProxy;

        protected bool _dispose = false;

        public TransparentProxy() { }

        ~TransparentProxy() { this.Dispose(false); }

        public object UnWrap()
        {
            return this._realProxy;
        }

        protected virtual FilterDispatcher GetFilterDispatcher(MethodInfo method)
        {
            return new FilterDispatcher(method);
        }

        protected T InternalInvoke<T>(string propTypeName, string methodName, Delegate dgt, params object[] parms)
        {
            MethodInfo method = this.GetMethodInfo(propTypeName, methodName);
            object result = this.Invoke(method, dgt, parms);
            return (T)result;
        }

        protected void InternalInvoke(string propTypeName, string methodName, Delegate dgt, params object[] parms)
        {
            MethodInfo method = this.GetMethodInfo(propTypeName, methodName);
            if (method.IsDefined(typeof(Oneway), false))
            {
                InvokeHandler handler = new InvokeHandler(this.Invoke);
                KeyValuePair<MethodInfo, object[]> state = new KeyValuePair<MethodInfo, object[]>(dgt.Method, parms);
                handler.BeginInvoke(method, dgt, parms, new AsyncCallback(AsyncCallback), state);
            }
            else
            {
                this.Invoke(method, dgt, parms);
            }
        }

        protected virtual void AsyncCallback(IAsyncResult iar)
        {

            AsyncResult ar = iar as AsyncResult;
            InvokeHandler handler = ar.AsyncDelegate as InvokeHandler;
            try
            {
                handler.EndInvoke(iar);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected virtual object Invoke(MethodInfo method, Delegate dgt, params object[] parms)
        {

            FilterDispatcher filter = this.GetFilterDispatcher(method);
            bool modified;
            object result = filter.PreDispatch(out modified, parms);
            if (!modified)
            {
                result = dgt.DynamicInvoke(parms);
            }
            object temp = filter.PostDispatch(out modified, result);
            if (modified)
            {
                result = temp;
            }
            return result;
        }

        protected MethodInfo GetMethodInfo(string propTypeName, string methodName)
        {
            Type type = Type.GetType(propTypeName);
            foreach (var item in type.GetMethods())
            {
                if (item.ToString().Equals(methodName))
                {
                    return item;
                }
            }
            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                this._dispose = true;
            }
        }

        public void SetRealProxy(FacadeObject facadeObject)
        {
            _realProxy = facadeObject;
        }
    }
}
