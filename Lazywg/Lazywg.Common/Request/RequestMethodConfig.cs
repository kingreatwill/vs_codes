using Lazywg.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lazywg.Common.Request
{
    public class RequestMethodConfig
    {
        private static string configFile = string.Empty;

        private static Dictionary<string, RequestMethod> methodDict = new Dictionary<string, RequestMethod>();

        /// <summary>
        /// 私有
        /// </summary>
        private RequestMethodConfig()
        {
            if (methodDict == null)
            {
                methodDict = new Dictionary<string, RequestMethod>();
            }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void LoadConfig(string fileName)
        {
            if (File.Exists(fileName)) {
                configFile = fileName;
                ParseConfig();
                return;
            }
            fileName = AppDomain.CurrentDomain.BaseDirectory + @"/" + fileName;
            if (File.Exists(fileName))
            {
                configFile = fileName;
                ParseConfig();
                return;
            }
            throw new FileNotFoundException();
        }

        private static void ParseConfig() {
            RequestMethods methods = XmlHelper.DeserializeFromXml<RequestMethods>(configFile);
            methods.Methods.ForEach(item =>
            {
                if (methodDict.ContainsKey(item.MethodCode))
                {
                    throw new ArgumentException(string.Format("方法[{0}]已存在",item.MethodCode));
                }
                methodDict.Add(item.MethodCode, item);
            });
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static RequestMethod GetMethod(string code) {
            RequestMethod method = null;
            methodDict.TryGetValue(code, out method);
            return method;
        }
    }
}
