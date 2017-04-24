using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFES.Refresh
{
    public class HttpJsonLoad
    {
        public static List<HttpJson> LoadFile(string file)
        {
            if (!File.Exists(file))
                file = Environment.CurrentDirectory + @"/" + file;
            if (!File.Exists(file))
                throw new FileNotFoundException();
            StreamReader reader = new StreamReader(file, Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            return JsonHelper.DeserializeJsonToList<HttpJson>(content);
        }
    }
}
