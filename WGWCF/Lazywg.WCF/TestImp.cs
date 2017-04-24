using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lazywg.WCF
{
    /// <summary>
    /// 服务协定实现
    /// </summary>
    public class TestImp : ITest
    {
        /// <summary>
        /// 操作实现
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsTest(Test model)
        {
            throw new NotImplementedException();
        }
    }
}