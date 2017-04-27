using System.Collections.Generic;

namespace ADO.NET.SqlHelper
{
    /// <summary>
    /// sql文本接口
    /// </summary>
    public interface IBaseSqlText
    {
       /// <summary>
       /// 添加
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="t"></param>
       /// <returns></returns>
        bool Insert<T>(T t);

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Delete<T>(List<SqlParam> where);

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update<T>(T t);

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="update"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        bool UpdateByWhere<T>(List<SqlParam> update,List<SqlParam> where);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        T Select<T>(List<SqlParam> where);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        List<T> SelectByWhere<T>(List<SqlParam> where);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        int SelectCount<T>(List<SqlParam> where);

        /// <summary>
        /// 获取分页数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<T> SelectByPager<T>(List<SqlParam> where, PagerHelper pager);
    }
}
