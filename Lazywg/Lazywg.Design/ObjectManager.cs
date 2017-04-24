using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Design
{
    /// <summary>
    /// 对象管理者
    /// </summary>
    public class ObjectManager
    {
        /// <summary>
        /// 多线程共享资源 创建互斥锁
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// 缓存对象字典 key 存储对象名称
        /// </summary>
        private Dictionary<int, object> objDict = new Dictionary<int, object>();

        /// <summary>
        /// 私有实例
        /// </summary>
        private static ObjectManager _instance = null;

        /// <summary>
        /// 私有构造
        /// </summary>
        private ObjectManager() {
            if (this.objDict == null)
            {
                objDict = new Dictionary<int,object>();
            }
        }

        /// <summary>
        /// 实例对外唯一获取路径
        /// </summary>
        public static ObjectManager Instance {
            get {
                if (_instance==null)
                {
                    lock (_locker) {
                        _instance = new ObjectManager();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 创建实例对象
        /// </summary>
        /// <typeparam name="T">需要实例化得对象</typeparam>
        /// <param name="t">对象实例</param>
        /// <returns></returns>
        public T CreateObject<T>() where T :new() {
            ObjectMonitor<T> monitor = null;
            object obj = null;
            int key = typeof(T).GetHashCode();//获取类型hashcode
            //判断是否存在字典中
            if (objDict.TryGetValue(key, out obj))
            {
                monitor =(ObjectMonitor<T>)obj;
                monitor.UseCount++;
                monitor.UseTime = DateTime.Now;
            }
            else {
                lock (objDict)
                {
                    //创建实例并存储到字典中
                    monitor = new ObjectMonitor<T>(new T());
                    monitor.UseCount = 1;
                    monitor.UseTime = DateTime.Now;
                    objDict.Add(key, monitor);
                }
            }
            return monitor.Obj;
        }
    }
}
