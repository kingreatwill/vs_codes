using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lazywg.Helper
{
    public class DictionaryHelper
    {
        /// <summary>
        /// 对象序列化到文件
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="dicts"></param>
        /// <returns></returns>
        public static bool XmlSerialize(string xmlFile, SerializableDictionary<string, string> dicts)
        {
            using (FileStream fileStream = new FileStream(xmlFile, FileMode.Create))
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<string, string>));
                xmlFormatter.Serialize(fileStream, dicts);
            }
            return true;
        }

        /// <summary>
        /// 文件反序列化到对象
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static SerializableDictionary<string, string> XmlDeserialize(string xmlFile)
        {
            SerializableDictionary<string, string> dicts = null;
            using (FileStream fileStream = new FileStream(xmlFile, FileMode.Open))
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<string, string>));
                dicts = (SerializableDictionary<string, string>)xmlFormatter.Deserialize(fileStream);
            }
            return dicts;
        }
    }
}
