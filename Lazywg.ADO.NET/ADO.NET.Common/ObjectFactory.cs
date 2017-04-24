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

        public ObjectFactory Instance
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

        public T CreateObject<T>(params object[] parms) where T : class
        {
            Type type = typeof(T);
            string key = type.FullName;
            T t = _dicts[key] as T;
            if (t != null)
            {
                return t;
            }
            t = Activator.CreateInstance(type, parms) as T;
            lock (_locker)
            {
                t = _dicts[key] as T;
                if (t != null)
                {
                    return t;
                }
                _dicts.Add(key, t);
            }
            return t;
        }
    }
}
