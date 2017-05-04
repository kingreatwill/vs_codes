using Lazywg.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lazywg.Common.Request
{
    public class MethodConfig
    {
        private static string configFile = string.Empty;

        private static Dictionary<string, Method> methodDict = new Dictionary<string, Method>();

        /// <summary>
        /// 私有
        /// </summary>
        private MethodConfig()
        {
            if (methodDict == null)
            {
                methodDict = new Dictionary<string, Method>();
            }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void LoadConfig(string fileName)
        {
            if (File.Exists(fileName))
            {
                configFile = fileName;
                ParseConfig();
                return;
            }
            configFile = FileHelper.GetFileFullPath(fileName);
            ParseConfig();
        }

        private static void ParseConfig()
        {
            Methods methods = XmlHelper.DeserializeFromXml<Methods>(configFile);
            methods.MethodList.ForEach(item =>
            {
                if (methodDict.ContainsKey(item.MethodCode))
                {
                    throw new ArgumentException(string.Format("方法[{0}]已存在", item.MethodCode));
                }
                methodDict.Add(item.MethodCode, item);
            });
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Method GetMethod(string code)
        {
            Method method = null;
            methodDict.TryGetValue(code, out method);
            return method;
        }
    }
}
