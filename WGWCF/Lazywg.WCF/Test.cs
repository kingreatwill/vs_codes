using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lazywg.WCF
{
   /// <summary>
   /// 测试数据 协定
   /// </summary>
    [DataContract]
    public class Test
    {
        /// <summary>
        /// 是否测试 数据成员 
        /// </summary>
        [DataMember]
        public bool IsTest { get; set; }

        /// <summary>
        /// 测试信息 忽略的协定数据成员
        /// </summary>
        [IgnoreDataMember]
        public string TestContext { get; set; }
    }
}