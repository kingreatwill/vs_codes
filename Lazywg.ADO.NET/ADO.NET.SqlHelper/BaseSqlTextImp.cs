using ADO.NET.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ADO.NET.SqlHelper
{
    /// <summary>
    /// 基础sql文本命令操作实现
    /// </summary>
    public class BaseSqlTextImp : IBaseSqlText
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Delete<T>(List<SqlParam> where)
        {
            string sql = SqlTextHelper.GetDeleteSqlText<T>(where);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Insert<T>(T t)
        {
            string sql = SqlTextHelper.GetInsertSqlText<T>(t);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual bool BatchInsert<T>(List<T> list)
        {
            using(SqlConnection conn = new SqlConnection(GetConnectStr()))
            {
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    PagerHelper page = new PagerHelper() { DataCount = list.Count, PageSize = 20000 };
                    int count = page.PageCount;
                    int insertCount = 0;
                    for (int i = 1; i < count + 1; i++)
                    {
                        page.PageIndex = i;
                        List<T> pageList = list.Skip(page.StartIndex).Take(page.PageSize).ToList();
                        string sql = SqlTextHelper.GetBatchInsertSqlText<T>(pageList);
                        insertCount += SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
                    }

                    tran.Commit();
                    return insertCount > 0;
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    throw ex;
                }
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T Select<T>(List<SqlParam> where)
        {
            string sql = SqlTextHelper.GetSelectSqlText<T>(where);
            DataSet ds = SqlHelper.ExecuteDataset(GetConnectStr(), CommandType.Text, sql);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                List<T> list = AutoMapper.MapToList<T>(dt);
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            return default(T);
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public virtual List<T> SelectByPager<T>(List<SqlParam> where, PagerHelper pager)
        {
            string sql = SqlTextHelper.GetSelectPagerSqlText<T>(where, pager);
            pager.DataCount = SelectCount<T>(where);
            DataSet ds = SqlHelper.ExecuteDataset(GetConnectStr(), CommandType.Text, sql);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                return AutoMapper.MapToList<T>(dt);
            }
            return null;
        }

        /// <summary>
        /// 查询条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int SelectCount<T>(List<SqlParam> where)
        {
            string sql = SqlTextHelper.GetSelectCountSqlText<T>(where);
            object countObj = SqlHelper.ExecuteScalar(GetConnectStr(), CommandType.Text, sql);
            int count = 0;
            int.TryParse(count.ToString(), out count);
            return count;
        }

        /// <summary>
        /// 查询条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int SelectCount<T>(string where)
        {
            string sql = SqlTextHelper.GetSelectCountSqlText<T>(where);
            object countObj = SqlHelper.ExecuteScalar(GetConnectStr(), CommandType.Text, sql);
            int count = 0;
            int.TryParse(count.ToString(), out count);
            return count;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<T> SelectByWhere<T>(List<SqlParam> where)
        {
            string sql = SqlTextHelper.GetSelectSqlText<T>(where);
            DataSet ds = SqlHelper.ExecuteDataset(GetConnectStr(), CommandType.Text, sql);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                return AutoMapper.MapToList<T>(dt);
            }
            return null;
        }

        /// <summary>
        /// 实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Update<T>(T t)
        {
            string sql = SqlTextHelper.GetUpdateSqlText<T>(t);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 条件更新 部分更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="update"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool UpdateByWhere<T>(List<SqlParam> update, List<SqlParam> where)
        {
            string sql = SqlTextHelper.GetUpdateSqlText<T>(update, where);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), CommandType.Text, sql) > 0;
        }

        public virtual string GetConnectStr()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            if (string.IsNullOrEmpty(constr))
            {
                constr = ConfigurationManager.AppSettings["ConStr"];
            }
            return constr;
        }
    }
}
