using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core
{
    /// <summary>
    /// 实体模块统一接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 为了主键统一，而手动设置的
        /// </summary>
        string ID { get; }
    }
}
