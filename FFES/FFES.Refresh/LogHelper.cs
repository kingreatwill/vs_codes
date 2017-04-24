using FFES.Refresh;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Logger
{
    public class LogHelper
    {
        private static string path = RefreshConfig.LogFile;

        public static void WriteLog(string mode, string content)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Environment.CurrentDirectory+ @"/logs";
            }
            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }
            DateTime now = DateTime.Now;

            string filename = path + "/" +now.ToString("yyyy-MM-dd") + ".log";//用日期对日志文件命名

            //创建或打开日志文件，向日志文件末尾追加记录
            StreamWriter mySw = File.AppendText(filename);

            //向日志文件写入内容
            string write_content = now.ToString("yyyy-MM-dd HH:mm:ss") + " " + mode + ": " + content;
            mySw.WriteLine(write_content);

            //关闭日志文件
            mySw.Close();
        }
    }
}
