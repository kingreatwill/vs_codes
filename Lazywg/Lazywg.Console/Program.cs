using Lazywg.Common.Helper;
using Lazywg.Common.Request;
using System;
using System.Collections.Generic;

namespace Lazywg.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Console.WriteLine(new TestHandler().GetObj());

            ////日期扩展
            //DateTime date = DateTime.Now;

            //date.ToShortSimpleDateString();
            //date.ToSimpleDateString();

            // SimpleThread thread = new SimpleThread();
            //thread.TestSimpleThreate();
            //thread.CaculateCount();
            //thread.TestThreadPool();

            //Algorithm.Write(1);
            //Algorithm.StrOrder();
            //Algorithm.DisplayArrayValues(new byte[] { 255, 255, 255 });
            //Algorithm.DisplayArrayValues(new short[] { 255, 255, 255 });

            //int re = Algorithm.StrToInt("12.3");
            //double re2 = Algorithm.StrToDouble("12.3456");
            //System.Console.WriteLine(re);
            //System.Console.WriteLine(re2);
            //for (int i = 0; i < 20; i++)
            //{
            //    System.Console.WriteLine("{0},{1}",i,Algorithm.GetNumIsTwoPowerFlag(i));
            //}

            //Algorithm.Shuffle();
            User user = new User() { ID = Guid.NewGuid().ToString(), Name = "wanggao1", Sex = "male", Age = 28, CreateTime = DateTime.Now, IsDelete = false };
            //string json1 = JsonHelper.JsonSerialize(user);
            //System.Console.WriteLine(json1);
            //string json2 = JsonHelper.JSONSerialize(user);
            //System.Console.WriteLine(json2);
            //string json3 = JsonHelper.Serialize(user);
            //System.Console.WriteLine(json3);

            //User user1 = JsonHelper.Deserialize<User>(json1);
            //User user2 = JsonHelper.JSONDeserialize<User>(json1);
            //User user3 = JsonHelper.JsonDeserialize<User>(json1);

            //Dictionary<string, object> dict1 = JsonHelper.Deserialize<Dictionary<string,object>>(json1);//转化失败 无数据
            //Dictionary<string, object> dict2 = JsonHelper.JSONDeserialize<Dictionary<string, object>>(json1);
            //Dictionary<string, object> dict3 = JsonHelper.JsonDeserialize<Dictionary<string, object>>(json1);

            Methods methods = new Methods();
            methods.MethodList.Add(new Method() { Assembly = "Lazywg.Console", NameSpace = "Lazywg.Console", ClassName = "Algorithm", MethodCode = "1001", MethodName = "Write", MethodDesc = "循环打印" });
            methods.MethodList.Add(new Method() { Assembly = "Lazywg.Console", NameSpace = "Lazywg.Console", ClassName = "Algorithm", MethodCode = "1002", MethodName = "StrOrder", MethodDesc = "字符串排序" });
            methods.MethodList.Add(new Method() { Assembly = "Lazywg.Console", NameSpace = "Lazywg.Console", ClassName = "Algorithm", MethodCode = "1003", MethodName = "RepeatCopyStr", MethodDesc = "重复复制字符串" });

            XmlHelper.SerializeToXml(methods, @"D:\method.config");

            //Methods methods2 = XmlHelper.DeserializeFromXml<Methods>(@"D:\method.config");

            //MethodConfig.LoadConfig("method.config");

            //Dictionary<string, object> request = new Dictionary<string, object>();
           
            //request.Add("MethodCode", "1001");

            ////Dictionary<string, object> request = new Dictionary<string, object>();
            ////request.Add("Params", "{'number':100}");
            ////request.Add("MethodCode", "1001");

            //Dictionary<string, object> parms = new Dictionary<string, object>();
            //parms.Add("number",100);

            //Method method = MethodConfig.GetMethod(request["MethodCode"].ToString());

            //object result = MethodCall.CallMethod(method, parms);
            //System.Console.WriteLine(result);


            System.Console.ReadLine();

        }
    }
}
