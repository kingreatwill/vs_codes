using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core
{
    /// <summary>
    /// 数据操作统一接口(插入，查詢，刪除）
    /// </summary>
    public interface IEntityRepository<TEntity> where TEntity : class //这里使用泛型接口
    {
        /// <summary>
        /// 根据数据库实体—》插入记录
        /// </summary>
        void Insert(TEntity entity);

        /// <summary>
        /// 根据数据库实体—》[批量]插入记录
        /// </summary>
        void Insert(IList<TEntity> list);

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除列表
        /// </summary>
        /// <param name="list"></param>
        void Delete(IList<TEntity> list);

        /// <summary>
        /// 得到实体列表
        /// </summary>
        /// <returns></returns>
        DbSet<TEntity> GetList();

        void Update(TEntity entity);

        void Update(IList<TEntity> list);
    }
}
