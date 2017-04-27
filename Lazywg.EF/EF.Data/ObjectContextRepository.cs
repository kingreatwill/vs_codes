using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{
    /// <summary>
    /// EF数据结构实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObjectContextRepository : IRepository
    {
        #region PropertiesObjectContext
        /// <summary>
        /// 日志对象
        /// </summary>
        public VLog.IVLog _iVlog { get; set; }
        /// <summary>
        /// 数据上下文，对子类公开
        /// </summary>
        protected readonly ObjectContext _db;
        #endregion

        #region Constructors
        /// <summary>
        /// 通过具体子类建立DbContext对象，并提供日志对象的实现
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public ObjectContextRepository(ObjectContext db, VLog.IVLog logger)
        {
            try
            {
                _db = db;
                _iVlog = logger;
            }
            catch (Exception)
            {

                throw new Exception("EF底层数据出现问题，请检查...");
            }

        }

        public ObjectContextRepository(ObjectContext db): this(db, null)
        {

        }

        #endregion

        #region IRepository<T> 成员

        #region Create,Update,Delete  --Virtual Methods

        #region 简单调用
        public virtual void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            this.Insert(entity, true);
        }

        public void Insert<TEntity>(List<TEntity> list) where TEntity : class
        {
            //  BulkInsertAll<TEntity>(list);
            list.ForEach(entity =>
            {
                Insert(entity, false);
            });
            this.SaveChanges();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            // this.Update<TEntity>(entity, true);
            UpdateEntity<TEntity>(entity);
        }

        public void Update<TEntity>(List<TEntity> list) where TEntity : class
        {
            list.ForEach(entity =>
            {
                this.Update<TEntity>(entity, false);
            });
            this.SaveChanges();
        }

        public virtual void Update<TEntity>(Expression<Action<TEntity>> entity) where TEntity : class
        {
            this.Update<TEntity>(entity, true);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            this.Delete<TEntity>(entity, true);
        }
        #endregion

        #region GUID操作实现
        public void Insert<TEntity>(TEntity entity, bool isSubmit) where TEntity : class
        {
            //Logger.InfoLog("Create 表名:{0},内容：{1}", entity, ToJson(entity));
            _db.CreateObjectSet<TEntity>().AddObject(entity);
            if (isSubmit)
                this.SaveChanges();
        }

        public void Update<TEntity>(TEntity entity, bool isSubmit) where TEntity : class
        {
            _db.Attach(entity as EntityObject);
            _db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
            if (isSubmit)
                this.SaveChanges();
        }

        public void Update<TEntity>(Expression<Action<TEntity>> entity, bool isSubmit) where TEntity : class
        {
            TEntity newEntity = typeof(TEntity).GetConstructor(Type.EmptyTypes).Invoke(null) as TEntity;//建立指定类型的实例
            List<string> propertyNameList = new List<string>();
            MemberInitExpression param = entity.Body as MemberInitExpression;
            foreach (var item in param.Bindings)
            {
                string propertyName = item.Member.Name;
                object propertyValue;
                var memberAssignment = item as MemberAssignment;
                if (memberAssignment.Expression.NodeType == ExpressionType.Constant)
                {
                    propertyValue = (memberAssignment.Expression as ConstantExpression).Value;
                }
                else
                {
                    propertyValue = Expression.Lambda(memberAssignment.Expression, null).Compile().DynamicInvoke();
                }
                typeof(TEntity).GetProperty(propertyName).SetValue(newEntity, propertyValue, null);
                propertyNameList.Add(propertyName);
            }
            _db.Attach(newEntity as EntityObject);
            //  _db.Configuration.ValidateOnSaveEnabled = false;
            var ObjectStateEntry = ((IObjectContextAdapter)_db).ObjectContext.ObjectStateManager.GetObjectStateEntry(newEntity);
            propertyNameList.ForEach(x => ObjectStateEntry.SetModifiedProperty(x.Trim()));
            if (isSubmit)
                this.SaveChanges();
        }

        public void Delete<TEntity>(TEntity entity, bool isSubmit) where TEntity : class
        {
            _db.Attach(entity as EntityObject);
            _db.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
            if (isSubmit)
                this.SaveChanges();
        }
        #endregion

        #endregion

        #region Get --Virtual Methods
        public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetEntities(predicate).Count();
        }

        public virtual TEntity GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetEntities(predicate).SingleOrDefault();
        }

        public virtual IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            return _db.CreateObjectSet<TEntity>();
        }
        public virtual IEnumerable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return this.GetEntities<TEntity>().Where(predicate);
        }

        //public virtual IEnumerable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order) where TEntity : class
        //{
        //    var orderable = new Orderable<TEntity>(GetEntities(predicate).AsQueryable());
        //    order(orderable);
        //    return orderable.Queryable;
        //}

        //public virtual IEnumerable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order, int skip, int count) where TEntity : class
        //{
        //    return GetEntities(predicate, order).Skip(skip).Take(count);
        //}
        #endregion

        #endregion

        #region Methods
        /// <summary>
        /// 提交动作
        /// 当程序中出现SaveChanges开始与数据库进行通讯
        /// </summary>
        protected virtual void SaveChanges()
        {
            try
            {
                _db.SaveChanges(); //在有
            }
            catch (Exception ex)
            {
                string Message = "error:";
                if (ex.InnerException == null)
                    Message += ex.Message + ",";
                else if (ex.InnerException.InnerException == null)
                    Message += ex.InnerException.Message + ",";
                else if (ex.InnerException.InnerException.InnerException == null)
                    Message += ex.InnerException.InnerException.Message + ",";
                throw new Exception(Message);
            }
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void BulkInsertAll<T>(IEnumerable<T> entities) where T : class
        {
            entities = entities.ToArray();

            string cs = _db.Connection.ConnectionString;
            var conn = new SqlConnection(cs);
            conn.Open();

            Type t = typeof(T);

            var bulkCopy = new SqlBulkCopy(conn)
            {
                DestinationTableName = string.Format("[{0}]", t.Name)
            };

            var properties = t.GetProperties().Where(EventTypeFilter).ToArray();
            var table = new DataTable();

            foreach (var property in properties)
            {
                Type propertyType = property.PropertyType;
                if (propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                }

                table.Columns.Add(new DataColumn(property.Name, propertyType));
            }

            foreach (var entity in entities)
            {
                table.Rows.Add(properties.Select(
                  property => GetPropertyValue(
                  property.GetValue(entity, null))).ToArray());
            }

            bulkCopy.WriteToServer(table);
            conn.Close();
        }

        private bool EventTypeFilter(System.Reflection.PropertyInfo p)
        {
            var attribute = Attribute.GetCustomAttribute(p,
                typeof(AssociationAttribute)) as AssociationAttribute;

            if (attribute == null) return true;
            if (attribute.IsForeignKey == false) return true;

            return false;
        }

        private object GetPropertyValue(object o)
        {
            if (o == null)
                return DBNull.Value;
            return o;
        }

        private void UpdateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            Type entityType = entity.GetType();
            var table = entityType.GetProperties().Where(i => i.PropertyType != typeof(EntityKey) && i.PropertyType != typeof(EntityState)).ToArray();
            var primaryKeyColumns = (entity as EntityObject).GetPK();
            List<object> arguments = new List<object>();
            StringBuilder builder = new StringBuilder();

            foreach (var change in table)
            {
                if (primaryKeyColumns.Contains(change))
                    continue;

                if (arguments.Count != 0)
                    builder.Append(", ");

                if (change.GetValue(entity, null) != null)
                {
                    builder.Append(change.Name + " = {" + arguments.Count + "}");
                    arguments.Add(change.GetValue(entity, null));
                }
                else
                {
                    builder.Append(change.Name + " = NULL, ");
                }
            }

            if (builder.Length == 0)
                return;

            builder.Insert(0, "UPDATE " + string.Format("[{0}]", entityType.Name) + " SET ");

            builder.Append(" WHERE ");
            bool firstPrimaryKey = true;

            foreach (var primaryField in table)
            {
                if (firstPrimaryKey)
                    firstPrimaryKey = false;
                else
                    builder.Append(" AND ");

                object val = entityType.GetProperty(primaryField.Name).GetValue(entity, null);
                builder.Append(GetEqualStatment(primaryField.Name, arguments.Count));
                arguments.Add(val);
            }



            try
            {
                _db.ExecuteStoreCommand(builder.ToString(), arguments.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetEqualStatment(string fieldName, int paramId)
        {
            return string.Format("{0} = {1}", fieldName, GetParamTag(paramId));

        }

        private static string GetParamTag(int paramId)
        {
            return "{" + paramId + "}";

        }
        #endregion


        public void BatchInsert<TEntity>(List<TEntity> list) where TEntity : class
        {
            DoListSQL(list, SQLType.Insert);
        }

        public void BatchUpdate<TEntity>(List<TEntity> list) where TEntity : class
        {
            DoListSQL<TEntity>(list, SQLType.Update);
        }

        public void BatchDelete<TEntity>(List<TEntity> list) where TEntity : class
        {
            DoListSQL<TEntity>(list, SQLType.Delete);
        }

        /// <summary>
        /// 构建Update语句串
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Tuple<string, object[]> CreateUpdateSQL<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentException("The database entity can not be null.");

            Type entityType = entity.GetType();
            var table = entityType.GetProperties().Where(i => i.PropertyType != typeof(EntityKey) && i.PropertyType != typeof(EntityState)).ToArray();
            var primaryKeyColumns = (entity as EntityObject).GetPK();
            if (primaryKeyColumns == null || primaryKeyColumns.Count == 0)
                throw new ArgumentException("The Table entity have not a primary key.");
            List<object> arguments = new List<object>();
            StringBuilder builder = new StringBuilder();

            foreach (var change in table)
            {
                if (primaryKeyColumns.Contains(change))
                    continue;

                if (arguments.Count != 0)
                    builder.Append(", ");

                if (change.GetValue(entity, null) != null)
                {
                    builder.Append(change.Name + " = {" + arguments.Count + "}");

                    if (change.PropertyType == typeof(string) || change.PropertyType == typeof(DateTime))
                        arguments.Add("'" + change.GetValue(entity, null).ToString().Replace("'", "char(39)") + "'");
                    else
                        arguments.Add(change.GetValue(entity, null));
                }
                else
                {
                    builder.Append(change.Name + " = NULL, ");
                }
            }

            if (builder.Length == 0)
                throw new Exception("没有任何属性进行更新");

            builder.Insert(0, " UPDATE " + string.Format("[{0}]", entityType.Name) + " SET ");

            builder.Append(" WHERE ");
            bool firstPrimaryKey = true;

            foreach (var primaryField in primaryKeyColumns)
            {
                if (firstPrimaryKey)
                    firstPrimaryKey = false;
                else
                    builder.Append(" AND ");

                object val = entityType.GetProperty(primaryField.Name).GetValue(entity, null);
                builder.Append(GetEqualStatment(primaryField.Name, arguments.Count));
                arguments.Add(val);
            }
            return new Tuple<string, object[]>(builder.ToString(), arguments.ToArray());

        }

        /// <summary>
        /// 构建Insert语句串
        /// 主键为自增时，如果主键值为0，我们将主键插入到SQL串中
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Tuple<string, object[]> CreateInsertSQL<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentException("The database entity can not be null.");

            Type entityType = entity.GetType();
            var table = entityType.GetProperties().Where(i => i.PropertyType != typeof(EntityKey) && i.PropertyType != typeof(EntityState)).ToArray();
            var primaryKeyColumns = (entity as EntityObject).GetPK();

            List<object> arguments = new List<object>();
            StringBuilder fieldbuilder = new StringBuilder();
            StringBuilder valuebuilder = new StringBuilder();

            fieldbuilder.Append(" INSERT INTO " + string.Format("[{0}]", entityType.Name) + " (");

            foreach (var member in table)
            {
                if (primaryKeyColumns.Contains(member) && Convert.ToString(member.GetValue(entity, null)) == "0")
                    continue;
                object value = member.GetValue(entity, null);
                if (value != null)
                {
                    if (arguments.Count != 0)
                    {
                        fieldbuilder.Append(", ");
                        valuebuilder.Append(", ");
                    }

                    fieldbuilder.Append(member.Name);
                    if (member.PropertyType == typeof(string) || member.PropertyType == typeof(DateTime))
                        valuebuilder.Append("'{" + arguments.Count + "}'");
                    else
                        valuebuilder.Append("{" + arguments.Count + "}");
                    if (value.GetType() == typeof(string))
                        value = value.ToString().Replace("'", "char(39)");
                    arguments.Add(value);

                }
            }


            fieldbuilder.Append(") Values (");

            fieldbuilder.Append(valuebuilder.ToString());
            fieldbuilder.Append(");");
            return new Tuple<string, object[]>(fieldbuilder.ToString(), arguments.ToArray());
        }
        /// <summary>
        /// 构建Delete语句串
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Tuple<string, object[]> CreateDeleteSQL<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentException("The database entity can not be null.");

            Type entityType = entity.GetType();
            var table = entityType.GetProperties().Where(i => i.PropertyType != typeof(EntityKey) && i.PropertyType != typeof(EntityState)).ToArray();
            var primaryKeyColumns = (entity as EntityObject).GetPK();
            if (primaryKeyColumns == null || primaryKeyColumns.Count == 0)
                throw new ArgumentException("The Table entity have not a primary key.");

            List<object> arguments = new List<object>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" Delete from " + string.Format("[{0}]", entityType.Name));

            builder.Append(" WHERE ");
            bool firstPrimaryKey = true;

            foreach (var primaryField in primaryKeyColumns)
            {
                if (firstPrimaryKey)
                    firstPrimaryKey = false;
                else
                    builder.Append(" AND ");

                object val = entityType.GetProperty(primaryField.Name).GetValue(entity, null);
                builder.Append(GetEqualStatment(primaryField.Name, arguments.Count));
                arguments.Add(val);
            }
            return new Tuple<string, object[]>(builder.ToString(), arguments.ToArray());
        }


        /// <summary>
        /// 执行实体列表以SQL串的方式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        private void DoListSQL<TEntity>(IEnumerable<TEntity> list, SQLType sqlType) where TEntity : class
        {
            StringBuilder sqlstr = new StringBuilder();

            switch (sqlType)
            {
                case SQLType.Insert:
                    list.ToList().ForEach(i =>
                    {
                        Tuple<string, object[]> sql = CreateInsertSQL(i);
                        sqlstr.AppendFormat(sql.Item1, sql.Item2);
                    });
                    break;
                case SQLType.Update:
                    list.ToList().ForEach(i =>
                    {
                        Tuple<string, object[]> sql = CreateUpdateSQL(i);
                        sqlstr.AppendFormat(sql.Item1, sql.Item2);
                    });
                    break;
                case SQLType.Delete:
                    list.ToList().ForEach(i =>
                    {
                        Tuple<string, object[]> sql = CreateDeleteSQL(i);
                        sqlstr.AppendFormat(sql.Item1, sql.Item2);
                    });
                    break;
                default:
                    throw new ArgumentException("请输入正确的参数");
            }

            _db.ExecuteStoreCommand(sqlstr.ToString());
        }

        /// <summary>
        /// 根据主键得到一个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetEntity<TEntity>(params object[] id) where TEntity : class
        {
            var count = 0;
            List<PropertyInfo> res_Primary = new List<PropertyInfo>();
            List<EntityKeyMember> keyMemberList = new List<EntityKeyMember>();
            PropertyInfo[] properties = typeof(TEntity).GetProperties();
            foreach (PropertyInfo pI in properties)
            {
                System.Object[] attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is EdmScalarPropertyAttribute)
                    {
                        if ((attribute as EdmScalarPropertyAttribute).EntityKeyProperty && !(attribute as EdmScalarPropertyAttribute).IsNullable)
                            keyMemberList.Add(new EntityKeyMember(pI.Name, id[count]));
                        count++;
                    }

                }
            }
            return _db.GetObjectByKey(new EntityKey(_db.DefaultContainerName + "." + typeof(TEntity).Name, keyMemberList)) as TEntity;
        }
    }
}
