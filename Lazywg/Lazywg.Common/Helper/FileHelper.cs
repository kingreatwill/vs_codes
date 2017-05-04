using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// 获取文件完整路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileFullPath(string fileName) {
            if (File.Exists(fileName))
            {
                return fileName;
            }
            fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(fileName))
            {
                return fileName;
            }
            fileName = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, fileName);
            if (File.Exists(fileName))
            {
                return fileName;
            }
            fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            if (File.Exists(fileName))
            {
                return fileName;
            }
            throw new FileNotFoundException();
        }
    }
}
