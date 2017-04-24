using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteModel
{
    public class RemotableType : MarshalByRefObject
    {
        public string SayHello()
        {
            Console.WriteLine("RemotableType.SayHello() was called!");//打印在服务端
            return "Hello, world";//返回给客户端
        }
    }
}
