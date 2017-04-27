using EF.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{
    public class DataContext : DbContext
    {

        public IDbSet<UserBas> UserBases { get; private set; }

        public DataContext() : base(DataContext.GetConnectStr())
        {
            InitDbSets();

        }

        public DataContext(string cs): base(cs)
        {
            InitDbSets();
        }

        private void InitDbSets()
        {
            UserBases = this.Set<UserBas>();
            Init();
        }

        public static string GetConnectStr()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            if (string.IsNullOrEmpty(constr))
            {
                constr = ConfigurationManager.AppSettings["ConStr"];
            }
            return constr;
        }

        private static void SetInitializer(InitializerTypes InitType)
        {
            switch (InitType)
            {
                case InitializerTypes.Standard:
                    Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
                    break;
                case InitializerTypes.ReCreateAlways:
                    Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
                    break;
                case InitializerTypes.ReCreateByChange:
                    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
                    break;
                default:
                    break;
            }
        }

        private static void Init()
        {
            Database.DefaultConnectionFactory = new SqlConnectionFactory();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DataContext.SetInitializer(InitializerTypes.ReCreateByChange);
            }
            else
            {
                DataContext.SetInitializer(InitializerTypes.Standard);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
