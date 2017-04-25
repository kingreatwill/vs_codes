using System;
using System.Reflection;

namespace Lazywg.Facade
{
    public class FilterDispatcher
    {
        private MethodInfo method;

        public FilterDispatcher(MethodInfo method)
        {
            this.method = method;
        }

        internal object PreDispatch(out bool modified, object[] parms)
        {
            throw new NotImplementedException();
        }

        internal object PostDispatch(out bool modified, object result)
        {
            throw new NotImplementedException();
        }
    }
}