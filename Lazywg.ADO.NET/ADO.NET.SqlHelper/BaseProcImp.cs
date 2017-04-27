using ADO.NET.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    /// <summary>
    /// 存储过程实现
    /// </summary>
    public class BaseProcImp : IBaseProc
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Delete<T>(List<SqlParam> where)
        {
            string proc_name = string.Format("dbo.Sp_{0}_Delete", typeof(T).Name);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), proc_name, SqlParamHelper.GetSqlParams(where)) > 0;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Insert<T>(T t)
        {
            string proc_name = string.Format("dbo.Sp_{0}_Insert", typeof(T).Name);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), proc_name, SqlParamHelper.GetSqlParams<T>(t)) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T Select<T>(List<SqlParam> where)
        {
            //string proc_name = string.Format("dbo.Sp_{0}_Select", typeof(T).Name);
            //DataSet ds = SqlHelper.ExecuteDataset(GetConnectStr(), proc_name, SqlParamsHelper.GetSqlParams(where));
            //if (ds != null)
            //{
            //    DataTable dt = ds.Tables[0];
            //    List<T> list = AutoMapper.MapToList<T>(dt);
            //    if (list.Count > 0)
            //    {
            //        return list[0];
            //    }
            //}
            List<T> list = SelectByWhere<T>(where);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return default(T);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public virtual List<T> SelectByPager<T>(List<SqlParam> where, PagerHelper pager)
        {
            string proc_name = string.Format("dbo.Sp_{0}_SelectByPager", typeof(T).Name);
            SqlParameter[] sqlParams1 = SqlParamHelper.GetSqlParams(where);
            SqlParameter[] sqlParams2 = SqlParamHelper.GetSqlParams<PagerHelper>(pager);
            DataSet ds = SqlHelper.ExecuteDataset(GetConnectStr(), proc_name, sqlParams1.Concat(sqlParams2));
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                return AutoMapper.MapToList<T>(dt);
            }
            return null;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<T> SelectByWhere<T>(List<SqlParam> where)
        {
            string proc_name = string.Format("dbo.Sp_{0}_SelectByWhere", typeof(T).Name);
            DataSet ds = SqlHelper.ExecuteDataset(GetConnectStr(), proc_name, SqlParamHelper.GetSqlParams(where));
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                return AutoMapper.MapToList<T>(dt);
            }
            return null;
        }

        /// <summary>
        /// 获取条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int SelectCount<T>(List<SqlParam> where)
        {
            string proc_name = string.Format("dbo.Sp_{0}_SelectCount", typeof(T).Name);
            object countObj = SqlHelper.ExecuteScalar(GetConnectStr(), proc_name, SqlParamHelper.GetSqlParams(where));
            int count = 0;
            int.TryParse(count.ToString(), out count);
            return count;
        }

        /// <summary>
        /// 实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Update<T>(T t)
        {
            string proc_name = string.Format("dbo.Sp_{0}_Update", typeof(T).Name);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), proc_name, SqlParamHelper.GetSqlParams<T>(t)) > 0;
        }

        /// <summary>
        /// 部分更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="update"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool UpdateByWhere<T>(List<SqlParam> update, List<SqlParam> where)
        {
            string proc_name = string.Format("dbo.Sp_{0}_UpdateByWhere", typeof(T).Name);
            SqlParameter[] sqlParams1 = SqlParamHelper.GetSqlParams(update);
            SqlParameter[] sqlParams2 = SqlParamHelper.GetSqlParams(where);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), proc_name, sqlParams1.Concat(sqlParams2)) > 0;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        /// <returns></returns>
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
