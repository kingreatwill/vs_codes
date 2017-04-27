using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core
{
    /// <summary>
    /// 数据操作基类
    /// </summary>
    public abstract class RepositoryBase
    {
        #region 单件模式创建一个类对象

        /// <summary>
        /// 数据源对象
        /// </summary>
        private static EFContext dbContext = null;

        private static readonly object _locker = new object();

        public EFContext _db
        {
            get
            {
                if (dbContext == null)
                {
                    lock (_locker)
                    {
                        if (dbContext == null)
                        {
                            dbContext = new EFContext();
                        }
                    }
                }
                return dbContext;
            }
        }

        #endregion

        /// <summary>
        /// 存储变化 service层可能也会使用本方法,所以声明为public
        /// </summary>
        public virtual void SaveChanges()
        {
            this._db.Configuration.ValidateOnSaveEnabled = false;
            this._db.SaveChanges();
        }
    }
}
