using System;
using System.IO;
using System.Xml.Serialization;

namespace Lazywg.Common.Helper
{
    /// <summary>
    /// xml帮助类
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// 从文件反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new ArgumentNullException(filePath + " not Exists");

                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    T ret = (T)xs.Deserialize(reader);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 序列化到xml
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool SerializeToXml(object context, string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create,FileAccess.Write))
                {
                    XmlSerializer xs = new XmlSerializer(context.GetType());
                    xs.Serialize(stream, context);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 序列化成string
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string Serialize(object context)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream()) {
                    XmlSerializer xs = new XmlSerializer(context.GetType());
                    xs.Serialize(stream, context);
                    using (StreamReader reader = new StreamReader(stream)) {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string context)
        {
            try
            {
                using (StreamReader reader = new StreamReader(context))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    T ret = (T)xs.Deserialize(reader);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
