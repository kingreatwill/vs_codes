using ADO.NET.Common;
using ADO.NET.Model.DB;
using ADO.NET.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User() { ID = Guid.NewGuid().ToString(), Name = "wanggao1", Sex = "male", Age = 28, CreateTime = DateTime.Now, IsDelete = false };

            //SqlParameter[] sqlParams = SqlParamsHelper.GetSqlParams<User>(user);

            //foreach (var item in sqlParams)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Dictionary<string, object> dict = new Dictionary<string, object>();
            //dict.Add("Name", user.Name);
            //dict.Add("ID", user.ID);
            //dict.Add("Age", user.Age);

            //User user1 = AutoMapper.MapTo<User>(dict);
            //Console.WriteLine(user1.Name);

            //IBaseSqlText txtOperate = ObjectFactory.Instance.CreateObject<BaseSqlTextImp>(null);
            //bool result = txtOperate.Insert<User>(user);
            //Console.WriteLine("插入结果{0}", result);

            ////user.Name = "wg";
            ////bool result1 = dbOperate.Update<User>(user);
            //List<SqlParam> parms = new List<SqlParam>();
            //parms.Add(SqlParamsHelper.GetSqlParam("ID", user.ID));
            //User re = txtOperate.Select<User>(parms);

            IBaseProc procOperate = ObjectFactory.Instance.CreateObject<BaseProcImp>(null);
            Console.WriteLine("插入用户：{0}", procOperate.Insert<User>(user));

            user.Name = "proc_name";
            Console.WriteLine("更新用户：{0}", procOperate.Update<User>(user));

            Dictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("SqlWhere", string.Empty);

            User user1 = procOperate.Select<User>(SqlParamHelper.GetSqlParams(parms));
            int count = procOperate.SelectCount<User>(SqlParamHelper.GetSqlParams(parms));

            Console.ReadLine();
        }
    }
}
