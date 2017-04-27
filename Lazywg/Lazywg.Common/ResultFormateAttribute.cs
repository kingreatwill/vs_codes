using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common
{
    /// <summary>
    /// 返回数据特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ResultFormateAttribute : Attribute
    {
        /// <summary>
        /// 返回数据类型
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        /// 是否启用返回类型限制
        /// </summary>
        public bool IsEnable { get; set; }
    }

    /// <summary>
    /// 返回数据类型
    /// </summary>
    public enum ResultType
    {
        ResultPageList,
        ResultList,
        ResultObject,
        ResultInt
    }

    /// <summary>
    /// 返回基本信息
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回信息编码
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 返回分页list数据
    /// </summary>
    public class ResultPageList : BaseResult
    {
        public List<object> Content { get; set; }

        public PagerHelper Page { get; set; }
    }

    /// <summary>
    /// 返回list数据
    /// </summary>
    public class ResultList : BaseResult
    {
        public List<object> Content { get; set; }
    }

    /// <summary>
    /// 返回一个对象
    /// </summary>
    public class ResultObject : BaseResult
    {
        public object Content { get; set; }
    }

    /// <summary>
    /// 返回int类型值
    /// </summary>
    public class ResultInt : BaseResult
    {
        public int Content { get; set; }
    }
}
