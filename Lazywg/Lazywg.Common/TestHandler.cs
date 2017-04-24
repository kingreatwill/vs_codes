using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common
{
    [LazywgHandler]
    public class TestHandler : LazywgHandler
    {
        [ResultFormate(ResultType = ResultType.ResultInt, IsEnable = true)]
        public object GetObj()
        {
            return "测试";
        }
    }
}
