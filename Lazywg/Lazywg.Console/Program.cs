using Lazywg.Common;
using Lazywg.Helper;
using Lazywg.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Lazywg.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Console.WriteLine(new TestHandler().GetObj());

            ////日期扩展
            //DateTime date = DateTime.Now;

            //date.ToShortSimpleDateString();
            //date.ToSimpleDateString();

            // SimpleThread thread = new SimpleThread();
            //thread.TestSimpleThreate();
            //thread.CaculateCount();
            //thread.TestThreadPool();

            //Algorithm.Write(1);
            //Algorithm.StrOrder();
            //Algorithm.DisplayArrayValues(new byte[] { 255, 255, 255 });
            //Algorithm.DisplayArrayValues(new short[] { 255, 255, 255 });

            //int re = Algorithm.StrToInt("12.3");
            //double re2 = Algorithm.StrToDouble("12.3456");
            //System.Console.WriteLine(re);
            //System.Console.WriteLine(re2);
            //for (int i = 0; i < 20; i++)
            //{
            //    System.Console.WriteLine("{0},{1}",i,Algorithm.GetNumIsTwoPowerFlag(i));
            //}

            Algorithm.Shuffle();

            System.Console.ReadLine();

        }
    }
}
