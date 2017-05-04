using Lazywg.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common.Request
{
    /// <summary>
    /// 方法帮助
    /// </summary>
    public class MethodHelper
    {
        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parmDict"></param>
        /// <returns></returns>
        public static object CallMethod(Method method, Dictionary<string, object> parmsValue)
        {
            //获取类型
            Type classType = Type.GetType(TypeHelper.GetClassType(method), true, false);

            //获取方法
            MethodInfo callMethod = classType.GetMethod(method.MethodName);

            if (callMethod == null)
            {
                throw new MissingMethodException(string.Format("方法【{0}】不存在", method.MethodName));
            }

            //获取方法参数
            ParameterInfo[] methodParms = callMethod.GetParameters();

            //创建实例
            object classObj = Activator.CreateInstance(classType);

            //参数赋值
            object[] parms = GetMethodParms(methodParms, parmsValue);

            return CallMethod(classObj, callMethod, parms);
        }

        public static object CallMethod(Method method, object[] classParms, Dictionary<string, object> parmsValue)
        {
            //获取类型
            Type classType = Type.GetType(TypeHelper.GetClassType(method), true, false);

            //获取方法
            MethodInfo callMethod = classType.GetMethod(method.MethodName);

            if (callMethod == null)
            {
                throw new MissingMethodException(string.Format("方法【{0}】不存在", method.MethodName));
            }

            //创建实例
            object classObj = null;
            if (classParms == null)
            {
                classObj = Activator.CreateInstance(classType);
            }
            else
            {
                classObj = Activator.CreateInstance(classType, classParms);
            }

            #region 方法参数赋值

            //获取方法参数
            ParameterInfo[] methodParms = callMethod.GetParameters();

            //参数赋值
            object[] parms = GetMethodParms(methodParms, parmsValue);

            #endregion

            return CallMethod(classObj, callMethod, parms);
        }

        public static object CallMethod(Method method, object[] classParms, object[] methodParms)
        {
            //获取类型
            Type classType = Type.GetType(TypeHelper.GetClassType(method), true, false);

            return CallMethod(classType, method.MethodName, classParms, methodParms);
        }

        public static object CallMethod(Type classType, string methodName, object[] classParms, object[] methodParms)
        {
            //获取方法
            MethodInfo callMethod = classType.GetMethod(methodName);

            if (callMethod == null)
            {
                throw new MissingMethodException(string.Format("方法【{0}】不存在", methodName));
            }

            //创建实例
            object classObj = null;
            if (classParms == null)
            {
                classObj = Activator.CreateInstance(classType);
            }
            else
            {
                classObj = Activator.CreateInstance(classType, classParms);
            }

            return CallMethod(classObj, callMethod, methodParms);
        }

        public static object CallMethod(object classObj, MethodInfo callMethod, object[] methodParms)
        {
            if (callMethod.IsStatic)
            {
                return callMethod.Invoke(null, methodParms);
            }
            return callMethod.Invoke(classObj, methodParms);
        }

        public static object[] GetMethodParms(ParameterInfo[] methodParms, Dictionary<string, object> parmsVal)
        {
            //参数赋值
            object[] parms = new object[methodParms.Length];

            for (int i = 0; i < parms.Length; i++)
            {
                ParameterInfo parmInfo = methodParms[i];
                Type paramType = parmInfo.ParameterType;
                object pobj = null;
                if (parmsVal.TryGetValue(parmInfo.Name, out pobj))
                {
                    if (TypeHelper.IsCommonType(paramType))
                    {
                        parms[i] = Convert.ChangeType(pobj, paramType);
                    }
                    else
                    {
                        parms[i] = JsonHelper.Deserialize(pobj, paramType);
                    }
                }
                else
                {
                    parms[i] = parmInfo.DefaultValue;
                }
            }

            return parms;
        }
    }
}
