using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Lazywg.WCF
{
    /// <summary>
    /// 服务协定
    /// </summary>
    [ServiceContract]
    public interface ITest
    {
        /// <summary>
        /// 操作协定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsTest(Test model);
    }
}