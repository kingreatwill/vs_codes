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

        public static SqlDbType GetSqlDbType(Type type) {
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
                var attr = item.GetCustomAttributes(typeof(NotSqlParamAttribute));
                if (attr == null || attr.Count() < 1)
                {
                    paramProps.Add(item);
                }
            }

            SqlParameter[] sqlParams = new SqlParameter[paramProps.Count];

            int i = 0;
            foreach (var item in paramProps)
            {
                object value = item.GetValue(t);
                SqlDbType sqlTy = GetSqlDbType(item.PropertyType);
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
