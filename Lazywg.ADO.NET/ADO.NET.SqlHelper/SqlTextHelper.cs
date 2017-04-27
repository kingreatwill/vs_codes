using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    /// <summary>
    /// sql 文本命令帮助类
    /// </summary>
    public class SqlTextHelper
    {
        /// <summary>
        /// 获取插入命令文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetInsertSqlText<T>(T t)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> paramProps = new List<PropertyInfo>();
            foreach (var item in props)
            {
                SqlParamAttribute attr = item.GetCustomAttribute(typeof(SqlParamAttribute)) as SqlParamAttribute;
                if (attr == null || attr.IsParam == true)
                {
                    paramProps.Add(item);
                }
            }

            return GetSqlInsert(type.Name, paramProps) + GetSqlInsertValues<T>(paramProps, t);
        }

        /// <summary>
        /// 获取更新命令文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetUpdateSqlText<T>(T t)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> paramProps = new List<PropertyInfo>();
            List<PropertyInfo> pkProps = new List<PropertyInfo>();

            foreach (var item in props)
            {
                SqlParamAttribute attr = item.GetCustomAttribute(typeof(SqlParamAttribute)) as SqlParamAttribute;
                if (attr != null)
                {
                    if (attr.IsPK)
                    {
                        pkProps.Add(item);
                        continue;
                    }
                }
                if (attr == null || attr.IsParam == true)
                {
                    paramProps.Add(item);
                }
            }
            if (pkProps.Count < 1)
            {
                throw new ArgumentNullException(string.Format("{0}的主键不可为空", type.Name));
            }

            return string.Format(" update dbo.[{0}] {1} {2}", type.Name,GetSqlUpdateSet<T>(paramProps, t),GetSqlWhere<T>(pkProps, t));
        }

        /// <summary>
        /// 获取更新命令文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="update"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string GetUpdateSqlText<T>(List<SqlParam> update, List<SqlParam> where)
        {
            Type type = typeof(T);
            return string.Format(" update dbo.[{0}] {1} {2} ", type.Name, GetSqlUpdateSet(update),GetSqlWhere(where));
        }

        /// <summary>
        /// 获取删除文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string GetDeleteSqlText<T>(List<SqlParam> where)
        {
            Type type = typeof(T);
            return string.Format(" delete from dbo.[{0}] {1} ", type.Name, GetSqlWhere(where));
        }

        public static string GetDeleteSqlText<T>(string sqlWhere)
        {
            Type type = typeof(T);
            return string.Format(" delete from dbo.[{0}] where 1=1 {1} ", type.Name, sqlWhere);
        }

        public static string GetSelectSqlText<T>(List<SqlParam> where)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> paramProps = new List<PropertyInfo>();
            foreach (var item in props)
            {
                SqlParamAttribute attr = item.GetCustomAttribute(typeof(SqlParamAttribute)) as SqlParamAttribute;
                if (attr == null || attr.IsParam == true)
                {
                    paramProps.Add(item);
                }
            }
            return string.Format("select {0} from dbo.[{1}] {2} ", GetSqlField(paramProps), type.Name, GetSqlWhere(where));
        }

        public static string GetSelectSqlText<T>(string sqlWhere)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> paramProps = new List<PropertyInfo>();
            foreach (var item in props)
            {
                SqlParamAttribute attr = item.GetCustomAttribute(typeof(SqlParamAttribute)) as SqlParamAttribute;
                if (attr == null || attr.IsParam == true)
                {
                    paramProps.Add(item);
                }
            }
            return string.Format("select {0} from dbo.[{1}] where 1=1 {2} ", GetSqlField(paramProps), type.Name, sqlWhere);
        }

        public static string GetSelectPagerSqlText<T>(List<SqlParam> where, PagerHelper pager)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> paramProps = new List<PropertyInfo>();
            foreach (var item in props)
            {
                SqlParamAttribute attr = item.GetCustomAttribute(typeof(SqlParamAttribute)) as SqlParamAttribute;
                if (attr == null || attr.IsParam == true)
                {
                    paramProps.Add(item);
                }
            }
            return string.Format(" select {0} from(select {0},row_number()over(order by {1}) as RowIndex from dbo.[{2}] {3}) as t where RowIndex betwen {4} and {5} ", GetSqlField(paramProps), pager.Order, type.Name, GetSqlWhere(where), pager.StartIndex, pager.EndIndex);
        }

        public static string GetSelectPagerSqlText<T>(string sqlWhere, PagerHelper pager)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> paramProps = new List<PropertyInfo>();
            foreach (var item in props)
            {
                SqlParamAttribute attr = item.GetCustomAttribute(typeof(SqlParamAttribute)) as SqlParamAttribute;
                if (attr == null || attr.IsParam == true)
                {
                    paramProps.Add(item);
                }
            }
            return string.Format(" select {0} from(select {0},row_number()over(order by {1}) as RowIndex from dbo.[{2}] where 1=1 {3}) as t where RowIndex betwen {4} and {5} ", GetSqlField(paramProps), pager.Order, type.Name,sqlWhere, pager.StartIndex, pager.EndIndex);

        }

        public static string GetSelectCountSqlText<T>(string where)
        {
            Type type = typeof(T);
            return string.Format("select count(1) from dbo.[{0}] where 1=1 {1} ", type.Name, where);
        }

        public static string GetSelectCountSqlText<T>(List<SqlParam> where)
        {
            Type type = typeof(T);
            return string.Format("select count(1) from dbo.[{0}] {1} ", type.Name, GetSqlWhere(where));
        }

        private static string GetSqlWhere(List<SqlParam> where)
        {
            StringBuilder whereBuilder = new StringBuilder();
            foreach (var item in where)
            {
                if (GetSqlTypeIsNeedChar(item.SqlType))
                {
                    whereBuilder.AppendFormat(" and {0}='{1}'", item.Param, item.Value);
                }
                else
                {
                    whereBuilder.AppendFormat(" and {0}={1}", item.Param, item.Value);
                }
            }

            return " where 1=1 " + whereBuilder.ToString();
        }

        private static string GetSqlWhere<T>(List<PropertyInfo> where, T t)
        {
            StringBuilder whereBuilder = new StringBuilder();
            foreach (var item in where)
            {
                if (GetSqlTypeIsNeedChar(item.PropertyType))
                {
                    whereBuilder.AppendFormat(" and {0}='{1}'", item.Name, item.GetValue(t));
                }
                else
                {
                    whereBuilder.AppendFormat(" and {0}={1}", item.Name, item.GetValue(t));
                }
            }

            return " where 1=1 " + whereBuilder.ToString();
        }

        private static string GetSqlInsert(string tableName, List<PropertyInfo> props)
        {
            return string.Format(" insert into dbo.[{0}] ({1})", tableName, GetSqlField(props));
        }

        private static string GetSqlUpdateSet<T>(List<PropertyInfo> props, T t)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" set ");
            foreach (var item in props)
            {
                if (GetSqlTypeIsNeedChar(item.PropertyType))
                {
                    sqlBuilder.AppendFormat("{0}='{1}',", item.Name, item.GetValue(t));
                }
                else
                {
                    sqlBuilder.AppendFormat("{0}={1},", item.Name, item.GetValue(t));
                }
            }
            string sqlStr = sqlBuilder.ToString();
            return sqlStr.Substring(0, sqlStr.Length - 1);
        }

        private static string GetSqlUpdateSet(List<SqlParam> props)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" set ");
            foreach (var item in props)
            {
                if (GetSqlTypeIsNeedChar(item.SqlType))
                {
                    sqlBuilder.AppendFormat("{0}='{1}',", item.Param, item.Value);
                }
                else
                {
                    sqlBuilder.AppendFormat("{0}={1},", item.Param, item.Value);
                }
            }
            string sqlStr = sqlBuilder.ToString();
            return sqlStr.Substring(0, sqlStr.Length - 1);
        }

        private static string GetSqlInsertValues<T>(List<PropertyInfo> props, T t)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            foreach (var item in props)
            {
                if (GetSqlTypeIsNeedChar(item.PropertyType))
                {
                    sqlBuilder.AppendFormat("'{0}',", item.GetValue(t));
                }
                else
                {
                    sqlBuilder.AppendFormat("{0},", item.GetValue(t));
                }
            }
            string sqlStr = sqlBuilder.ToString();
            sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);
            return string.Format(" values({0})", sqlBuilder.ToString());
        }

        private static string GetSqlField(List<PropertyInfo> props)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            foreach (var item in props)
            {
                if (sqlBuilder.ToString().Length < 1)
                {
                    sqlBuilder.AppendFormat("{0}", item.Name);
                }
                else
                {
                    sqlBuilder.AppendFormat(",{0}", item.Name);
                }
            }
            return sqlBuilder.ToString();
        }

        private static string GetSqlField(List<SqlParam> props)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            foreach (var item in props)
            {
                if (sqlBuilder.ToString().Length < 1)
                {
                    sqlBuilder.AppendFormat("{0}", item.Param);
                }
                else
                {
                    sqlBuilder.AppendFormat(",{0}", item.Param);
                }
            }
            return sqlBuilder.ToString();
        }

        private static bool GetSqlTypeIsNeedChar(DbType type)
        {
            return type == DbType.String || type == DbType.DateTime || type == DbType.Boolean||type==DbType.Guid||type==DbType.Date;
        }

        private static bool GetSqlTypeIsNeedChar(Type type)
        {
            return type == typeof(DateTime) || type == typeof(string) || type == typeof(bool) || type==typeof(Guid);
        }
    }
}
