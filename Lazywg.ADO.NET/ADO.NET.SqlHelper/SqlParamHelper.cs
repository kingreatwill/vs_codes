﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.SqlHelper
{
    public class SqlParamHelper
    {
        public static SqlDbType DbTypeToSqlDbType(DbType sqlType)
        {
            SqlParameter parm = new SqlParameter();
            parm.DbType = sqlType;
            return parm.SqlDbType;
        }

        public static DbType SqlDbTypeToDbType(SqlDbType sqlType)
        {
            SqlParameter parm = new SqlParameter();
            parm.SqlDbType = sqlType;
            return parm.DbType;
        }

        public static SqlParameter[] GetSqlParams(List<SqlParam> paramList)
        {
            if (paramList==null||paramList.Count<1)
            {
                return null;
            }
            paramList = paramList.OrderBy(item => item.SortNum).ToList();

            SqlParameter[] sqlParams = new SqlParameter[paramList.Count];
            int i = 0;
            foreach (var item in paramList)
            {
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@" + item.Param,
                    SqlDbType = DbTypeToSqlDbType(item.SqlType),
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
        public static SqlParameter[] GetSqlParams<T>(T t)
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
                    ParameterName = @"@" + name,
                    SqlDbType = sqlTy,
                    Value = value,
                    Direction = direction
                };
                sqlParams[i] = param;
                i++;
            }
            return sqlParams;
        }

        public static List<SqlParam> GetSqlParams(Dictionary<string, object> parmDict)
        {
            if (parmDict==null||parmDict.Count<1)
            {
                return null;
            }
            List<SqlParam> parms = new List<SqlParam>();
            foreach (var item in parmDict)
            {
                parms.Add(GetSqlParam(item.Key, item.Value));
            }
            return parms;
        }

        public static SqlParam GetSqlParam(string paramName, object paramValue)
        {
            SqlParameter parm = new SqlParameter(paramName, paramValue);
            return GetSqlParam(paramName, paramValue, parm.DbType, parm.Direction, parm.Size);
        }

        public static SqlParam GetSqlParam(string paramName, object paramValue, DbType dbType)
        {
            SqlParameter parm = new SqlParameter(paramName, paramValue);
            parm.DbType = dbType;
            return GetSqlParam(paramName, paramValue, parm.DbType, parm.Direction, parm.Size);
        }
        public static SqlParam GetSqlParam(string paramName, object paramValue, DbType dbType, ParameterDirection direction)
        {
            SqlParameter parm = new SqlParameter(paramName, paramValue);
            parm.DbType = dbType;
            parm.Direction = direction;
            return GetSqlParam(paramName, paramValue, parm.DbType, parm.Direction, parm.Size);
        }
        public static SqlParam GetSqlParam(string paramName, object paramValue, DbType dbType, ParameterDirection direction, int size)
        {
            return new SqlParam() { Param = @"@" + paramName, Value = paramValue, SqlType = dbType, Direction = direction, Size = size };
        }
    }
}
