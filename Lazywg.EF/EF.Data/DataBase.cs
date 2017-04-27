using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{
    public abstract class DataBase
    {
        private static DataContext _db = null;

        protected static DataContext CreateInstance()
        {
            if (_db == null)
                _db = new DataContext();
            return _db;
        }

        protected DataContext Db = CreateInstance();

        protected virtual void SubmitChanges()
        {
            try
            {
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
