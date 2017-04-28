using Lazywg.Common.Helper;
using Lazywg.Common.Request;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lazywg.Console
{
    public class MethodCall
    {
        public static object CallMethod(RequestMethod method, Dictionary<string, object> parmDict)
        {
            string version = "Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null";
            if (!method.ClassType.Contains(version))
                method.ClassType = string.Format("{0},{1},{2}", method.ClassType, method.Assembly, version);

            //获取类型
            Type classType = Type.GetType(method.ClassType, true, false);

            //获取方法
            MethodInfo callMethod = classType.GetMethod(method.MethodName);

            //获取方法参数
            ParameterInfo[] methodParms = callMethod.GetParameters();

            //创建实例
            object classObj = Activator.CreateInstance(classType);

            //参数赋值
            object[] parms = new object[methodParms.Length];
            for (int i = 0; i < parms.Length; i++)
            {
                ParameterInfo parmInfo = methodParms[i];
                Type paramType = parmInfo.ParameterType;
                object pobj = null;
                if (parmDict.TryGetValue(parmInfo.Name, out pobj))
                {
                    parms[i] = JsonHelper.Deserialize(pobj, paramType);
                }
                else
                {
                    parms[i] = parmInfo.DefaultValue;
                }
            }
            if (callMethod.IsStatic)
            {
                return callMethod.Invoke(null, parms);
            }
            return callMethod.Invoke(classObj, parms);
        }
    }
}
