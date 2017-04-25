using ADO.NET.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace ADO.NET.SqlHelper
{
    public class BaseImp : IBase
    {
        public virtual bool Delete<T>(List<SqlParam> parms)
        {
            string sql = SqlTextHelper.GetDeleteSqlText<T>(parms);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), CommandType.Text, sql) > 1;
        }

        public virtual bool Insert<T>(T t)
        {
            string sql = SqlTextHelper.GetInsertSqlText<T>(t);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), CommandType.Text, sql) > 1;
        }

        public virtual T Select<T>(List<SqlParam> parms)
        {
            string sql = SqlTextHelper.GetSelectSqlText<T>(parms);
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

        public virtual List<T> SelectByPager<T>(List<SqlParam> parms, PagerHelper pager)
        {
            string sql = SqlTextHelper.GetSelectSqlText<T>(parms);
            DataSet ds = SqlHelper.ExecuteDataset(GetConnectStr(), CommandType.Text, sql);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                return AutoMapper.MapToList<T>(dt);
            }
            return null;
        }

        public virtual List<T> SelectByWhere<T>(List<SqlParam> parms)
        {
            string sql = SqlTextHelper.GetSelectSqlText<T>(parms);
            DataSet ds = SqlHelper.ExecuteDataset(GetConnectStr(), CommandType.Text, sql);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                return AutoMapper.MapToList<T>(dt);
            }
            return null;
        }

        public virtual bool Update<T>(T t)
        {
            string sql = SqlTextHelper.GetUpdateSqlText<T>(t);
            return SqlHelper.ExecuteNonQuery(GetConnectStr(), CommandType.Text, sql) > 1;
        }

        public virtual string GetConnectStr()
        {
            return ConfigurationManager.AppSettings["ConStr"];
        }
    }
}
