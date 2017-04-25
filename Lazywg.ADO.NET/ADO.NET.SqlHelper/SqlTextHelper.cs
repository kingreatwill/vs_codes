using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    public class SqlTextHelper
    {
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

            return GetSqlInsert(type.Name, paramProps) + GetSqlInsertValues<T>(paramProps,t);
        }

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

            return string.Format(" update table {0} ", type.Name) + GetSqlUpdateSet<T>(paramProps,t) + GetSqlWhere<T>(pkProps,t);
        }

        public static string GetUpdateSqlText<T>(List<SqlParam> update, List<SqlParam> where)
        {
            Type type = typeof(T);
            return string.Format(" update table {0}  ", type.Name) + GetSqlUpdateSet(update) + GetSqlWhere(where);
        }

        public static string GetDeleteSqlText<T>(List<SqlParam> where)
        {
            Type type = typeof(T);
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat(" delete from {0} ", type.Name);

            return sqlBuilder.ToString() + GetSqlWhere(where);
        }

        public static string GetDeleteSqlText<T>(string sqlWhere)
        {
            Type type = typeof(T);
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat(" delete from {0} where ", type.Name);
            sqlBuilder.Append(sqlWhere);
            return sqlBuilder.ToString();
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
            return string.Format("select {0} from {1} {2} ", GetSqlField(paramProps), type.Name, GetSqlWhere(where));
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
            return string.Format("select {0} from {1} where 1=1 and {2} ", GetSqlField(paramProps), type.Name,sqlWhere);
        }

        public static string GetSelectPagerSqlText<T>(List<SqlParam> where, PagerHelper pager)
        {
            throw new NotImplementedException();
        }

        public static string GetSelectPagerSqlText<T>(string sqlWhere, PagerHelper pager)
        {
            throw new NotImplementedException();
        }

        private static string GetSqlWhere(List<SqlParam> where)
        {
            StringBuilder whereBuilder = new StringBuilder();
            foreach (var item in where)
            {
                if (whereBuilder.ToString().Length < 1)
                {
                    if (GetSqlTypeIsNeedChar(item.SqlType))
                    {
                        whereBuilder.AppendFormat(" {0}='{1}'", item.Param, item.Value);
                    }
                    else
                    {
                        whereBuilder.AppendFormat(" {0}={1}", item.Param, item.Value);
                    }
                }
                else
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
            }

            return " where " + whereBuilder.ToString();
        }

        private static string GetSqlWhere<T>(List<PropertyInfo> where, T t)
        {
            StringBuilder whereBuilder = new StringBuilder();
            foreach (var item in where)
            {
                if (whereBuilder.ToString().Length < 1)
                {
                    if (GetSqlTypeIsNeedChar(item.PropertyType))
                    {
                        whereBuilder.AppendFormat(" {0}='{1}'", item.Name, item.GetValue(t));
                    }
                    else
                    {
                        whereBuilder.AppendFormat(" {0}={1}", item.Name, item.GetValue(t));
                    }
                }
                else
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
            }

            return " where " + whereBuilder.ToString();
        }

        private static string GetSqlInsert(string tableName, List<PropertyInfo> props)
        {
            return string.Format(" insert into {0} (", tableName) + GetSqlField(props) + ") ";
        }

        private static string GetSqlUpdateSet<T>(List<PropertyInfo> props,T t)
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

        private static string GetSqlInsertValues<T>(List<PropertyInfo> props,T t)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" values(");
            foreach (var item in props)
            {
                if (item.PropertyType == typeof(DateTime) || item.PropertyType == typeof(string))
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
            return sqlStr + ")";
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

        private static bool GetSqlTypeIsNeedChar(DbType type) {
            return type == DbType.String || type == DbType.DateTime;
        }

        private static bool GetSqlTypeIsNeedChar(Type type)
        {
          return type == typeof(DateTime) || type == typeof(string);
        }
    }
}
