using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core
{
    /// <summary>
    /// 数据操作统一接口（更新）
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class //这里使用泛型接口
    {
        /// <summary>
        /// 根据数据库实体—》更新记录
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 根据响应的属性名称进行更新
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        /// <param name="enums">要响应的列枚举</param>
        void Update(TEntity entity, Enum enums);

        /// <summary>
        /// 根据数据库实体—》[批量]更新记录
        /// </summary>
        /// <param name="entity"></param>
        void Update(IList<TEntity> list);

        /// <summary>
        /// 根据响应的属性名称进行批量更新
        /// </summary>
        /// <param name="list">要更新的实体IList<IDataEntity></param>
        /// <param name="enums">要响应的列枚举</param>
        void Update(IList<TEntity> list, Enum enums);
    }
}
