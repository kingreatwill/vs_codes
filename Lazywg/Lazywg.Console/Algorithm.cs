using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lazywg.Console
{
    public class Algorithm
    {
        /// <summary>
        /// 循环打印 递归
        /// 不允许使用循环语句、条件语句，在控制台中打印出1-200这200个数。
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool Write(int number)
        {
            System.Console.WriteLine(number);
            return number >= 200 || Write(number + 1);
        }

        /// <summary>
        /// 按字符串内数字排序
        /// 有5个Aspx页面，分别为"Page_1.aspx","Page_10.aspx","Page_100.aspx","Page_11.aspx","Page_111.aspx",请编写代码，让5个Aspx页面按下面的顺序输出:
        /// Page_1.aspx Page_10.aspx Page_11.aspx  Page_100.aspx Page_111.aspx
        /// </summary>
        public static void StrOrder()
        {
            var pageList = new[] { "Page_1.aspx", "Page_10.aspx", "Page_100.aspx", "Page_11.aspx", "Page_111.aspx" };
            pageList = pageList.OrderBy(s => int.Parse(Regex.Match(s, @"\d+").Value)).ToArray();
            Array.ForEach(pageList, System.Console.WriteLine);
        }

        /// <summary>
        /// 字符串重复复制
        /// 给定一个字符串，试编写代码，实现重复N倍输出字符串的功能。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="repeatCount"></param>
        /// <returns></returns>
        public static string RepeatCopyStr(string str, int repeatCount)
        {
            char[] array = str.ToCharArray();
            char[] dest = new char[array.Length * repeatCount];
            for (int i = 0; i < repeatCount; i++)
            {
                Buffer.BlockCopy(array, 0, dest, array.Length * i * 2, array.Length * 2);
            }
            return new String(dest);
        }

        /// <summary>
        /// 给定一个整形数组，请用16进制的方式显示数组的值。
        /// 比方：一个short类型数组:[255,255,255],输出的结果为 00FF 00FF 00FF,
        /// 如果是byte类型，则输出为 FF FF FF
        /// </summary>
        /// <param name="arr"></param>
        public static void DisplayArrayValues(Array arr)
        {
            int elementLength = Buffer.ByteLength(arr) / arr.Length;
            string formatString = String.Format("{{0:X{0}}} ", 2 * elementLength);
            for (int ctr = 0; ctr < arr.Length; ctr++)
                System.Console.Write(formatString, arr.GetValue(ctr));
            System.Console.WriteLine();
        }
        public static int StrToInt(string str)
        {
            if (!Regex.IsMatch(str, @"^-?[0-9]\d*"))
                throw new ArgumentException(string.Format("{0}非数字字符串", str));
            string intPart = string.Empty;
            bool isNegative = false;
            if (str.IndexOf('-') != -1)
            {
                str = str.Substring(1);
                isNegative = true;
            }
            if (str.IndexOf(".") != -1)
            {
                intPart = str.Substring(str.IndexOf('-') == -1 ? 0 : str.IndexOf('-'), str.IndexOf("."));
            }

            int part1 = StrToIntPart(intPart);

            return isNegative ? part1 : part1;
        }

        public static double StrToDouble(string str)
        {
            if (!Regex.IsMatch(str, @"^-?[0-9]\d*"))
                throw new ArgumentException(string.Format("{0}非数字字符串", str));
            string intPart = string.Empty;
            string floatPart = string.Empty;
            bool isNegative = false;
            if (str.IndexOf('-') != -1)
            {
                str = str.Substring(1);
                isNegative = true;
            }
            if (str.IndexOf(".") != -1)
            {
                intPart = str.Substring(str.IndexOf('-') == -1 ? 0 : str.IndexOf('-'), str.IndexOf("."));
                floatPart = str.Substring(str.IndexOf(".") + 1);
            }

            int part1 = StrToIntPart(intPart);
            double part2 = StrToFractionalPart(floatPart);

            return isNegative ? (part1 + part2) * -1 : part1 + part2;
        }

        private static double StrToFractionalPart(string str)
        {
            double result = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int it = str[i] - '0';
                result += it * 1.0 / Math.Pow(10, i + 1);
            }
            return result;
        }

        /// <summary>
        /// 请自行实现一个函数，该函数的功能是将用户输入的numeric string 转换为integer。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static int StrToIntPart(string str)
        {
            int result = 0;
            for (int i = 0; i < str.Length; i++)
            {
                result = result * 10 + (str[i] - '0');
            }
            return result;
        }

        /// <summary>
        /// 获取数字是否是2的n次方
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool GetNumIsTwoPowerFlag(int num)
        {
            if (num < 1) return false;
            //10&1,100&11,1000&111,10000&1111
            return (num & (num - 1)) == 0;
        }

        /// <summary>
        /// 随机洗牌
        /// </summary>
        public static void Shuffle()
        {
            var random = new Random();
            var result = new List<string>();
            string[] cardType = { "红桃", "黑桃", "方块", "梅花" };
            string[] cardValue = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            foreach (string type in cardType)
            {
                var list = cardValue.Select(value => string.Format("{0}{1}", type, value));
                result.AddRange(list);
            }
            result.ForEach(str => System.Console.Write("{0}\t", str));
            System.Console.WriteLine();
            System.Console.WriteLine();
            //result = (from c in result orderby random.Next(0, 51) descending select c).ToList();
            result = (from c in result orderby Guid.NewGuid() descending select c).ToList();
            result.ForEach(str => System.Console.Write("{0}\t", str));
        }
    }
}
