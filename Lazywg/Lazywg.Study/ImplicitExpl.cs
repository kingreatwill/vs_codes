using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Study
{
    /// <summary>
    /// 显式 隐式转换
    /// </summary>
    public class ImplicitExpl
    {
        public string StrValue { get; set; }

        public ImplicitExpl(string val)
        {
            this.StrValue = val;
        }

        public static implicit operator ImplicitExpl(string str)
        {
            return new ImplicitExpl(str);
        }

        public static implicit operator string(ImplicitExpl str)
        {
            return str.StrValue;
        }

        public static explicit operator double(ImplicitExpl str)
        {
            double value = 0;
            if (double.TryParse(str.StrValue, out value))
            {
                return value;
            }
            return 0;
        }
    }

    public class TestImplicit
    {

        public void Test(string str)
        {

            //隐式转换
            ImplicitExpl strExt = str;

            //隐式转换
            string str1 = new ImplicitExpl(str);
            Console.WriteLine(str1);

            //显示转换
            double dou = (double)new ImplicitExpl("123");
            //不支持 未定义该显示转换
            //decimal dec = (decimal)new StringExtend("123");
        }
    }
}
