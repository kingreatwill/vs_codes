using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{

    /// <summary>
    /// ObjectContext扩展
    /// </summary>
    public static class ObjectContextExtension
    {
        /// <summary>
        /// 得到主键
        /// </summary>
        public static List<PropertyInfo> GetPK(this EntityObject value)
        {
            List<PropertyInfo> pks = new List<PropertyInfo>();

            PropertyInfo[] properties = value.GetType().GetProperties();
            foreach (PropertyInfo pI in properties)
            {
                System.Object[] attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is EdmScalarPropertyAttribute)
                    {
                        if ((attribute as EdmScalarPropertyAttribute).EntityKeyProperty && !(attribute as EdmScalarPropertyAttribute).IsNullable)
                            pks.Add(pI);
                    }

                }
            }
            return pks;
        }

        /// <summary>
        /// 把所有属性都标为已修改
        /// </summary>
        /// <param name="objectContext"></param>
        /// <param name="item"></param>
        public static void SetAllModified<TEntity>(this ObjectContext objectContext, TEntity item)
        {
            ObjectStateEntry stateEntry = objectContext.ObjectStateManager.GetObjectStateEntry(item) as ObjectStateEntry;
            IEnumerable propertyNameList = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata.Select(pn => pn.FieldType.Name);
            foreach (string propName in propertyNameList)
            {
                stateEntry.SetModifiedProperty(propName);
            }
            stateEntry.SetModified();
        }
    }
}
