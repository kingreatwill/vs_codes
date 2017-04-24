using Lazywg.Common;
using Lazywg.Helper;
using Lazywg.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            SimpleThread thread = new SimpleThread();
            //thread.TestSimpleThreate();
            //thread.CaculateCount();
            thread.TestThreadPool();

            System.Console.ReadLine();

        }
    }
}
