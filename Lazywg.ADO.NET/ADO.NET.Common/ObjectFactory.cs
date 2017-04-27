using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Common
{
    /// <summary>
    /// 对象实例工厂
    /// </summary>
    public class ObjectFactory
    {
        private static Dictionary<string, object> _dicts = new Dictionary<string, object>();
        private static readonly object _locker = new object();
        private static ObjectFactory _instance = new ObjectFactory();
        private ObjectFactory() { }

        public static ObjectFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new ObjectFactory();
                        }
                    }
                }
                return _instance;
            }
        }

        public T CreateObject<T>(params object[] parms) where T : class,new()
        {
            Type type = typeof(T);
            string key = type.FullName;
            object t = null;
            if (_dicts.TryGetValue(key, out t))
            {
                return t as T;
            }
            lock (_locker)
            {
                if (_dicts.TryGetValue(key, out t))
                {
                    return t as T;
                }
                if (parms == null)
                {
                    t = Activator.CreateInstance(type) as T;
                }
                else
                {
                    t = Activator.CreateInstance(type, parms) as T;
                }
                _dicts.Add(key, t);
            }
            return t as T;
        }
    }
}
