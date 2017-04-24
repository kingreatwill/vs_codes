using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Helper
{
    public class LogHelper
    {
    }

    public class TextLoger
    {
        private string logName = string.Empty;
        private string logPath = string.Empty;


        public TextLoger(string name)
        {
            this.logName = name;
            this.logPath = GetLogFilePath();
        }

        //public bool WriteLoger(string content)
        //{
        //    string file = logPath + logName + DateTime.Now.ToString("yyyyMMdd") + ".log";
        //    using (FileStream stream = new FileStream(file, FileMode.OpenOrCreate))
        //    {
        //        byte[] bytes = Encoding.Default.GetBytes(content);
        //        stream.Write(bytes, bytes.Length, bytes.Length);
        //    }
        //    return true;
        //}
        public bool WriteLoger(string content)
        {
            string file = logPath + logName + DateTime.Now.ToString("yyyyMMdd") + ".log";
            File.AppendAllText(file, content);
            return true;
        }

        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <returns></returns>
        private string GetLogFilePath()
        {
            string path = ConfigurationManager.AppSettings["log"];
            if (string.IsNullOrEmpty(path))
            {
                path = Environment.CurrentDirectory + @"\logs\";
            }
            return path;
        }
    }
}
