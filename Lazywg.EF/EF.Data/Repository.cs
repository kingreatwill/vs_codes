using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{
    /// <summary>
    /// EF数据结构实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository : IRepository
    {
        #region Properties

        /// <summary>
        /// 日志对象
        /// </summary>
        public VLog.IVLog _iVlog { get; set; }

        /// <summary>
        /// 数据上下文，对子类公开
        /// </summary>
        protected readonly DbContext _db;

        #endregion

        #region Constructors

        /// <summary>
        /// 通过具体子类建立DbContext对象，并提供日志对象的实现
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public Repository(DbContext db, VLog.IVLog logger)
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

        public Repository(DbContext db): this(db, null){}

        #endregion

        #region 简单调用

        public virtual void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            this.Insert(entity, true);
        }

        public void Insert<TEntity>(List<TEntity> list) where TEntity : class
        {
            BulkInsertAll<TEntity>(list);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            this.Update<TEntity>(entity, true);
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
            _db.Entry<TEntity>(entity);
            _db.Set<TEntity>().Add(entity);
            if (isSubmit)
                this.SaveChanges();
        }

        public void Update<TEntity>(TEntity entity, bool isSubmit) where TEntity : class
        {
            _db.Set<TEntity>().Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
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
            _db.Set<TEntity>().Attach(newEntity);
            _db.Configuration.ValidateOnSaveEnabled = false;
            var ObjectStateEntry = ((IObjectContextAdapter)_db).ObjectContext.ObjectStateManager.GetObjectStateEntry(newEntity);
            propertyNameList.ForEach(x => ObjectStateEntry.SetModifiedProperty(x.Trim()));
            if (isSubmit)
                this.SaveChanges();
        }

        public void Delete<TEntity>(TEntity entity, bool isSubmit) where TEntity : class
        {
            _db.Set<TEntity>().Attach(entity);
            _db.Set<TEntity>().Remove(entity);
            if (isSubmit)
                this.SaveChanges();
        }

        #endregion

        #region Get --Virtual Methods

        public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetEntities(predicate).Count();
        }

        public virtual TEntity GetEntity<TEntity>(params object[] id) where TEntity : class
        {
            return _db.Set<TEntity>().Find(id);
        }
        public virtual TEntity GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetEntities(predicate).SingleOrDefault();
        }

        public virtual IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            return _db.Set<TEntity>();
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

            string cs = _db.Database.Connection.ConnectionString;
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

        #endregion
    }
}
