using System;
using Lazywg.Common.Request;

namespace Lazywg.Common.Helper
{
    public class TypeHelper
    {
        private static readonly string version = "Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null";

        /// <summary>
        /// string  int bool double float decimal guid char byte uint
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCommonType(Type type)
        {
            if (type == typeof(string)
                || type == typeof(int)
                || type == typeof(bool)
                || type == typeof(double)
                || type == typeof(float)
                || type == typeof(decimal)
                || type == typeof(Guid)
                || type == typeof(char)
                || type == typeof(byte)
                || type == typeof(uint)
                )
                return true;
            return false;
        }

        public static string GetClassType(Method method)
        {
            return GetClassType(method.Assembly, method.NameSpace, method.ClassName);
        }

        public static string GetClassType(string assemblyName, string nameSpace, string className)
        {
            return string.Format("{0}.{1},{2},{3}", nameSpace, className, assemblyName, version);
        }
    }
}
