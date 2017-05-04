using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Lazywg.Common.Helper;

namespace Lazywg.Facade
{
    public class FacadeContainer
    {
        private static Dictionary<string, Facade> _facadeSet = new Dictionary<string, Facade>();

        private static Dictionary<string, FacadeObject> _cachedFacades = new Dictionary<string, FacadeObject>();

        private static readonly object _locker = new object();

        public static int Count { get { return _cachedFacades.Count; } }

        private FacadeContainer() { }

        public static void LoadFacade(string configFile)
        {
            if (!File.Exists(configFile))
            {
                configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile);
            }
            if (!File.Exists(configFile))
            {
                configFile = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, configFile);
            }
            if (!File.Exists(configFile))
            {
                configFile = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, configFile);
            }
            if (!File.Exists(configFile))
            {
                FacadeSet cfg = XmlHelper.DeserializeFromXml<FacadeSet>(configFile);
                List<Type> props = new List<Type>();
                foreach (var item in cfg.Item)
                {
                    Type itf = Type.GetType(item.Interface);
                    Type imp = Type.GetType(item.Implement);
                    if (null==itf)
                    {
                        throw new TypeLoadException(string.Format("接口定义不正确：{0}", item.Interface));
                    }
                    if (null==imp)
                    {
                        throw new TypeLoadException(string.Format("接口实现类型定义错误：{0}", item.Implement));
                    }
                    if (!_facadeSet.ContainsKey(item.Interface))
                    {
                        props.Add(itf);
                        _facadeSet.Add(itf.AssemblyQualifiedName, item);
                    }
                    else {
                        throw new Exception(string.Format("不能重复加载正面对象：{0}", item.Name));
                    }
                }
                if (props.Count() > 0)
                {
                    DynamicAssemblyBuilder.Build(props.ToArray());
                }
                else {
                    throw new ArgumentException("Facade配置文件不存在或者参数不正确");
                }
            }
            throw new FileNotFoundException();
        }

        //public static T Get<T>() where T : FacadeObject
        //{
        //    string key = typeof(T).FullName;
        //    if (_cachedFacades[key] == null)
        //    {
        //        lock (_locker)
        //        {
        //            if (_cachedFacades[key] == null)
        //            {

        //                _cachedFacades.Add(key, Activator.CreateInstance<T>());
        //            }
        //        }
        //    }
        //    return (T)_cachedFacades[key];
        //}

        //正面透明代理
        public static T Get<T>()
        {
            T realProxy = GetRealProxy<T>();
            TransparentProxy trsp = DynamicAssemblyBuilder.CreateInstance(typeof(T));
            trsp.SetRealProxy(realProxy);
            return (T)(trsp as object);
        }

        public static T GetRealProxy<T>()
        {
            if (!typeof(T).IsInterface)
            {
                throw new NotSupportedException("泛型参数T不是接口类型");
            }
            string type = typeof(T).AssemblyQualifiedName;
            FacadeObject obj = null;
            if (_facadeSet.ContainsKey(type))
            {
                Facade facade = _facadeSet[type];
                if (facade.LifeMode == LifeMode.PerRequest)
                {
                    obj = CreateInstance(facade);
                    obj.Name = facade.Name;
                }
                else
                {
                    lock (_locker)
                    {
                        if (_cachedFacades.ContainsKey(type))
                        {
                            obj = _cachedFacades[type];
                        }
                        else
                        {
                            obj = CreateInstance(facade);
                            _cachedFacades.Add(type, obj);
                            obj.Name = facade.Name;
                        }
                    }
                }
                return (T)(obj as object);
            }
            else {
                throw new Exception(string.Format("指定类型[{0}]的接口正面没有配置实现体"));
            }
            
        }

        private static FacadeObject CreateInstance(Facade facade)
        {
            throw new NotImplementedException();
        }
    }
}
