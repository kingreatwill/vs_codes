using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Design
{
    /// <summary>
    /// 对象控制
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class ObjectMonitor<T> where T : new()
    {
        /// <summary>
        /// 唯一构造
        /// </summary>
        /// <param name="t"></param>
        public ObjectMonitor(T t) {
            this.Obj = t;
        }

        /// <summary>
        /// 对象
        /// </summary>
        public T Obj { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseTime { get; set; }
    }
}
