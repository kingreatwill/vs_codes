using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lazywg.Common.Request
{
    /// <summary>
    /// 请求方法
    /// </summary>
    [XmlRoot("Method")]
    public class Method
    {
        /// <summary>
        /// 方法描述
        /// </summary>
        [XmlAttribute("MethodDesc")]
        public string MethodDesc { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        [XmlAttribute("MethodName")]
        public string MethodName { get; set; }

        /// <summary>
        /// 方法编号
        /// </summary>
        [XmlAttribute("MethodCode")]
        public string MethodCode { get; set; }

        /// <summary>
        /// 所属程序集
        /// </summary>
        [XmlAttribute("Assembly")]
        public string Assembly { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        [XmlAttribute("NameSpace")]
        public string NameSpace { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        [XmlAttribute("ClassName")]
        public string ClassName { get; set; }
    }

    [XmlRoot("MethodsConfig")]
    public class Methods
    {

        [XmlArray("Methods")]
        [XmlArrayItem("Method")]
        public List<Method> MethodList { get; set; }

        public Methods()
        {
            MethodList = new List<Method>();
        }
    }
}
