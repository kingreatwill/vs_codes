using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Lazywg.Facade
{
    public class FacadeContainer
    {
        private static Dictionary<string, FacadeObject> _dicts = new Dictionary<string, FacadeObject>();

        private static readonly object _locker = new object();

        private FacadeContainer() { }

        public static T Get<T>() where T : FacadeObject
        {
            string key = typeof(T).FullName;
            if (_dicts[key] == null)
            {
                lock (_locker) {
                    if (_dicts[key]==null)
                    {

                        _dicts.Add(key, Activator.CreateInstance<T>());
                    }
                }
            }
            return (T)_dicts[key];
        }
    }
}
