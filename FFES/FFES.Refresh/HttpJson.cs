using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFES.Refresh
{
    public class HttpJson
    {
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string Params { get; set; }
        public int SleepTime { get; set; }
    }
}
