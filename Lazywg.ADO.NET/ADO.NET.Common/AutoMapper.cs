using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Common
{
    /// <summary>
    /// 运行时自动映射实体
    /// </summary>
    public class AutoMapper
    {
        /// <summary>
        /// 数据库实体转其他实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T MapTo<T, E>(E entity)
        {
            Type tType = typeof(T);
            Type eType = typeof(E);

            PropertyInfo[] eprops = eType.GetProperties();

            PropertyInfo[] tprops = eType.GetProperties();

            T t = Activator.CreateInstance<T>();

            foreach (var item in tprops)
            {
                string propName = item.Name;
                MapAttribute attr = item.GetCustomAttribute(typeof(MapAttribute)) as MapAttribute;
                if (attr != null)
                {
                    propName = attr.Name;
                }

                var prop = eprops.Where(it => string.Compare(it.Name, propName, true) == 0);
                if (prop != null && prop.Count() > 0)
                {
                    object value = prop.First().GetValue(entity);
                    item.SetValue(t, value);
                }
            }

            return t;
        }

        public static E MapTo<E>(DataRow row)
        {
            E entiry = Activator.CreateInstance<E>();
            PropertyInfo[] props = typeof(E).GetProperties();
            List<PropertyInfo> toProps = new List<PropertyInfo>();
            foreach (var item in props)
            {
                NotMapAttribute attr = item.GetCustomAttribute(typeof(NotMapAttribute)) as NotMapAttribute;
                if (attr == null)
                {
                    toProps.Add(item);
                }
            }
            foreach (var item in toProps)
            {
                Type propType = item.PropertyType;
                string propName = item.Name;
                object value = row[item.Name];
                if (value != null)
                {
                    item.SetValue(entiry, value);
                }
            }
            return entiry;
        }

        public static E MapTo<E>(Dictionary<string, object> dict)
        {
            E entiry = Activator.CreateInstance<E>();
            PropertyInfo[] props = typeof(E).GetProperties();
            List<PropertyInfo> toProps = new List<PropertyInfo>();
            foreach (var item in props)
            {
                NotMapAttribute attr = item.GetCustomAttribute(typeof(NotMapAttribute)) as NotMapAttribute;
                if (attr == null)
                {
                    toProps.Add(item);
                }
            }

            foreach (var item in toProps)
            {
                Type propType = item.PropertyType;
                object value = null;
                dict.TryGetValue(item.Name,out value);
                if (value != null)
                {
                    item.SetValue(entiry, value);
                    continue;
                }
                if (propType == typeof(DateTime))
                {
                    item.SetValue(entiry, LazywgConfigs.DefaultTime);
                }
            }
            return entiry;
        }

        public static List<E> MapToList<E>(DataTable table)
        {
            List<E> list = new List<E>();

            foreach (var item in table.Rows)
            {
                var entiry = MapTo<E>(item as DataRow);
                list.Add(entiry);
            }
            return list;
        }
    }
}
