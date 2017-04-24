using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteModel
{
    public class ClientModel: MarshalByRefObject
    {
        public string ClientActivited(string str) {
            Console.WriteLine(str);
            return "服务端已打印";
        }
    }
}
