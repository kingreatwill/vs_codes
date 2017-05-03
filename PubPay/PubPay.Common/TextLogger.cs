using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubPay.Common
{
    public class TextLogger
    {
        private static readonly object _locker = new object();

        private string _logType = "lazywg";

        public TextLogger() { }

        public TextLogger(string logType)
        {
            this._logType = logType;
        }

        public void WriteLog(object content)
        {
            if (content != null)
            {
                if (content is Exception)
                {
                    AppendException(content as Exception, false);
                }
                else
                {
                    AppendToFile(content.ToString());
                }
            }
        }

        public void WriteLog(string message, object content) {
            AppendToFile(message);
            WriteLog(content);
        }

        private void AppendToFile(object content)
        {
            if (content!=null)
            {
                AppendToFile(content.ToString());
            }
        }

        private void AppendException(Exception e, bool isInner)
        {
            if (e!=null)
            {
                if (isInner)
                {
                    AppendToFile("\t 内部异常:");
                }
                else {
                    AppendToFile("开始打印异常信息：+++++++++++++++++++++++++++++++++++++++");
                }
                AppendToFile("\t 提示信息:");
                AppendToFile(e.Message);

                AppendToFile("\t 代码:");
                AppendToFile(e.Source);

                AppendToFile("\t 堆栈:");
                AppendToFile(e.StackTrace);

                AppendToFile("\t 目标方法:");
                AppendToFile(e.TargetSite);

                AppendException(e.InnerException, true);

                if (!isInner)
                {
                    AppendToFile("异常信息打印结束：+++++++++++++++++++++++++++++++++++++++");
                }
            }
        }

        private void AppendToFile(string str)
        {
            lock (_locker)
            {
                try
                {
                    str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+ " " + str;
                    using (StreamWriter writer = new StreamWriter(GetFileName(), true))
                    {
                        writer.WriteLine(str);
                    }
                }
                catch
                {
                    return;
                }

            }
        }

        /// <summary>
        /// 获取日志文件的物理路径
        /// </summary>
        /// <returns></returns>
        private string GetFileName()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("{0}_{1}.log", _logType, DateTime.Now.ToString("yyyyMMdd")));
        }
    }
}
