using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFES.Refresh
{
    public class RefreshConfig
    {
        public static string HttpJsonFile
        {
            get
            {
                return ConfigurationManager.AppSettings["HttpJsonFile"];
            }
        }

        public static int SleepTime
        {
            get
            {
                string sleep = ConfigurationManager.AppSettings["SleepTime"];
                int sleepTime = 1000 * 60;
                int.TryParse(sleep, out sleepTime);
                return sleepTime;
            }
        }

        public static string LogFile
        {
            get
            {
                return ConfigurationManager.AppSettings["LogFile"];
            }
        }

        public static bool IsDebug
        {
            get
            {
                string debug = ConfigurationManager.AppSettings["IsDebug"];
                bool isdebug = true;
                bool.TryParse(debug, out isdebug);
                return isdebug;
            }
        }
    }
}
