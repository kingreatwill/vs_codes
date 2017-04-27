using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{
    /// <summary>
    /// data repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        void Insert<TEntity>(TEntity entity, bool isSubmit) where TEntity : class;

        /// <summary>
        /// 添加并提交
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Insert<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 批量添加并提交
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Insert<TEntity>(List<TEntity> list) where TEntity : class;

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update<TEntity>(TEntity entity, bool isSubmit) where TEntity : class;

        /// <summary>
        /// 更新并提交
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 更新列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        void Update<TEntity>(List<TEntity> list) where TEntity : class;

        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Update<TEntity>(Expression<Action<TEntity>> entity, bool isSubmit) where TEntity : class;

        /// <summary>
        /// 更新指定字段并提交
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Update<TEntity>(Expression<Action<TEntity>> entity) where TEntity : class;
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        void Delete<TEntity>(TEntity entity, bool isSubmit) where TEntity : class;

        /// <summary>
        /// 删除并提交
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 根据主键取一个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetEntity<TEntity>(params object[] id) where TEntity : class;

        /// <summary>
        /// 根据条件取一个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        /// <summary>
        /// 实体集对象的可查询结果集
        /// </summary>
        IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class;

        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        /// <summary>
        /// 返回结果集
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        /// <summary>
        /// 带有排序的结果集
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        //IEnumerable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order) where TEntity : class;

        /// <summary>
        /// 带分页和排序的结果集
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        //IEnumerable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order, int skip, int count) where TEntity : class;
    }
}
