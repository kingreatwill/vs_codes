using EmitMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core
{
    /// <summary>
    /// 数据操作实现类（统一实现类）
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityRepository<TEntity> : RepositoryBase, IEntityRepository<TEntity> where TEntity : class,IEntity
    {
        #region IEntityRepository<TEntity> Members

        public void Insert(TEntity entity)
        {
            this._db.Set<TEntity>().Add(entity);
            this._db.Entry(entity).State = EntityState.Added;
            this.SaveChanges();
        }

        public void Insert(IList<TEntity> list)
        {
            list.ToList().ForEach(entity =>
            {
                this._db.Set<TEntity>().Add(entity);
                this._db.Entry(entity).State = EntityState.Added;
            });
            this.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            this._db.Set<TEntity>().Remove(entity);
            this._db.Entry(entity).State = EntityState.Deleted;
            this.SaveChanges();
        }

        public void Delete(IList<TEntity> list)
        {
            list.ToList().ForEach(entity =>
            {
                this._db.Set<TEntity>().Remove(entity);
                this._db.Entry(entity).State = EntityState.Deleted;
            });
            this.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            var entry = this._db.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var entityToUpdate = DbSet.Find(entity.ID);
                ObjectMapperManager.DefaultInstance.GetMapper<TEntity, TEntity>().Map(entity, entityToUpdate);
                this.SaveChanges();
            }
        }

        public void Update(IList<TEntity> list)
        {
            throw new NotImplementedException();
        }

        public DbSet<TEntity> GetList()
        {
            return this.DbSet;
        }

        /// <summary>
        /// 泛型数据表属性
        /// </summary>
        protected DbSet<TEntity> DbSet
        {
            get { return this._db.Set<TEntity>(); }
        }

        #endregion

        /// <summary>
        /// 操作提交
        /// </summary>
        public override void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
