using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Common
{
    /// <summary>
    /// 自定义异常处理
    /// </summary>
    public class LazywgException : Exception
    {

        public LazywgException() : base("自定义异常")
        {
        }

        public LazywgException(string message) : base(message)
        {
        }

        public LazywgException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected LazywgException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
