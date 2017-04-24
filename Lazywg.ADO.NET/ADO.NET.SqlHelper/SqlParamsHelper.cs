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
                    SqlDbType = item.DbType,
                    Value = item.Value,
                    Size = item.Size,
                    Direction = item.Direction
                };
                sqlParams[i] = param;
                i++;
            }
            return sqlParams;
        }

        public static SqlParameter[] GetSqlParams<T>(T t, List<NotSqlParam> notSqlMap) where T : class
        {

            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> needParamProps = new List<PropertyInfo>();

            foreach (var item in props)
            {
                if (notSqlMap == null || notSqlMap.Count < 1)
                {
                    needParamProps.Add(item);
                    continue;
                }
                var ishave = notSqlMap.Select(it => it.Param.Equals(item.Name));
                if (ishave == null || ishave.Count() < 1)
                {
                    needParamProps.Add(item);
                }
            }

            SqlParameter[] sqlParams = new SqlParameter[needParamProps.Count];

            int i = 0;
            foreach (var item in needParamProps)
            {
                object value = item.GetValue(t);
                Type ty = item.PropertyType;
                SqlDbType sqlTy = SqlDbType.NVarChar;
                DbType dbt = DbType.String;
                if (Enum.TryParse<DbType>(ty.Name, true, out dbt))
                {
                    sqlTy = DbTypeToSqlDbType(dbt);
                }
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = item.Name,
                    SqlDbType = sqlTy,
                    Value = value,
                    Direction = ParameterDirection.Input
                };
                sqlParams[i] = param;
                i++;
            }
            return sqlParams;
        }

        public static SqlParameter[] GetSqlParams<T>(T t) where T : class
        {

            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> needParamProps = new List<PropertyInfo>();

            foreach (var item in props)
            {
                var attr = item.GetCustomAttributes(typeof(NotSqlParamAttribute));
                if (attr == null || attr.Count() < 1)
                {
                    needParamProps.Add(item);
                }
            }

            SqlParameter[] sqlParams = new SqlParameter[needParamProps.Count];

            int i = 0;
            foreach (var item in needParamProps)
            {
                object value = item.GetValue(t);
                Type ty = item.PropertyType;
                SqlDbType sqlTy = SqlDbType.NVarChar;
                DbType dbt = DbType.String;
                if (Enum.TryParse<DbType>(ty.Name, true, out dbt))
                {
                    sqlTy = DbTypeToSqlDbType(dbt);
                }
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = item.Name,
                    SqlDbType = sqlTy,
                    Value = value,
                    Direction = ParameterDirection.Input
                };
                sqlParams[i] = param;
                i++;
            }
            return sqlParams;
        }
    }
}
