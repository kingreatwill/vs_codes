using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    public class SqlParamsHelper
    {
        public static SqlDbType DbTypeToSqlDbType(DbType pSourceType)
        {
            SqlParameter paraConver = new SqlParameter();
            paraConver.DbType = pSourceType;
            return paraConver.SqlDbType;
        }

        public static DbType SqlDbTypeToDbType(SqlDbType pSourceType)
        {
            SqlParameter paraConver = new SqlParameter();
            paraConver.SqlDbType = pSourceType;
            return paraConver.DbType;
        }

        public static SqlParameter[] GetSqlParams(List<SqlParam> paramList)
        {
            SqlParameter[] sqlParams = new SqlParameter[paramList.Count];
            int i = 0;
            foreach (var item in paramList)
            {
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = item.Param,
                    SqlDbType = item.SqlType,
                    Value = item.Value,
                    Size = item.Size,
                    Direction = item.Direction
                };
                sqlParams[i] = param;
                i++;
            }
            return sqlParams;
        }

        public static SqlDbType GetSqlDbType(Type type)
        {
            SqlDbType sqlTy = SqlDbType.NVarChar;
            DbType dbt = DbType.String;
            if (Enum.TryParse<DbType>(type.Name, true, out dbt))
            {
                sqlTy = DbTypeToSqlDbType(dbt);
            }
            return sqlTy;
        }

        /// <summary>
        /// 泛型 NotSqlParamAttribute 过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static SqlParameter[] GetSqlParams<T>(T t) where T : class
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

            SqlParameter[] sqlParams = new SqlParameter[paramProps.Count];

            int i = 0;
            foreach (var item in paramProps)
            {
                object value = item.GetValue(t);
                string name = item.Name;

                SqlDbType sqlTy = GetSqlDbType(item.PropertyType);
                ParameterDirection direction = ParameterDirection.Input;

                SqlParamAttribute attr = item.GetCustomAttribute(typeof(SqlParamAttribute)) as SqlParamAttribute;

                if (attr != null)
                {
                    if (attr.Direction != default(ParameterDirection))
                    {
                        direction = attr.Direction;
                    }
                    if (!string.IsNullOrEmpty(attr.Name))
                    {
                        name = attr.Name;
                    }
                    if (attr.SqlType != default(SqlDbType))
                    {
                        sqlTy = attr.SqlType;
                    }
                }

                SqlParameter param = new SqlParameter()
                {
                    ParameterName = name,
                    SqlDbType = sqlTy,
                    Value = value,
                    Direction = direction
                };
                sqlParams[i] = param;
                i++;
            }
            return sqlParams;
        }
    }
}
