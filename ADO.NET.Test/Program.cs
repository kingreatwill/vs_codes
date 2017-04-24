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
            User user = new User() { ID = Guid.NewGuid().ToString(), Name = "wanggao", Sex = "male", Age = 28, CreateTime = DateTime.Now, IsDelete = false };

            SqlParameter[] sqlParams = SqlParamsHelper.GetSqlParams<User>(user);

            foreach (var item in sqlParams)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadLine();
        }
    }
}
