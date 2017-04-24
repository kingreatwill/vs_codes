using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lazywg.Thread
{
    /*
     一、使用线程的理由

    1、可以使用线程将代码同其他代码隔离，提高应用程序的可靠性。

    2、可以使用线程来简化编码。

    3、可以使用线程来实现并发执行。

    二、基本知识

    1、进程与线程：进程作为操作系统执行程序的基本单位，拥有应用程序的资源，进程包含线程，进程的资源被线程共享，线程不拥有资源。

    2、前台线程和后台线程：通过Thread类新建线程默认为前台线程。当所有前台线程关闭时，所有的后台线程也会被直接终止，不会抛出异常。

    3、挂起（Suspend）和唤醒（Resume）：由于线程的执行顺序和程序的执行情况不可预知，所以使用挂起和唤醒容易发生死锁的情况，在实际应用中应该尽量少用。

    4、阻塞线程：Join，阻塞调用线程，直到该线程终止。

    5、终止线程：Abort：抛出 ThreadAbortException 异常让线程终止，终止后的线程不可唤醒。Interrupt：抛出 ThreadInterruptException 异常让线程终止，通过捕获异常可以继续执行。

    6、线程优先级：AboveNormal BelowNormal Highest Lowest Normal，默认为Normal。
     **/
    public class SimpleThread
    {
        private static object _locker = new object();
        private int count = 0;

        public void TestSimpleThreate()
        {
            //创建线程
            System.Threading.Thread thread1 = new System.Threading.Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("Normal");
                }
            });
            System.Threading.Thread thread2 = new System.Threading.Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("Highest");
                }
            });
            thread1.Priority = ThreadPriority.Normal;//设置线程优先级
            thread2.Priority = ThreadPriority.Highest;//设置线程优先级
            thread1.Start();//启动线程
            thread2.Start();//启动线程
        }

        public void CaculateCount()
        {
            //创建线程
            System.Threading.Thread thread1 = new System.Threading.Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    //修改共享资源数据之前 锁定
                    lock (_locker)
                    {
                        count--;
                    }
                }
            });
            System.Threading.Thread thread2 = new System.Threading.Thread((times) =>
            {
                int num = (int)times;
                for (int i = 0; i < num; i++)
                {
                    //修改共享资源数据之前 锁定
                    lock (_locker)
                    {
                        count++;
                    }
                }
            });
            thread1.Priority = ThreadPriority.Normal;//设置线程优先级
            thread2.Priority = ThreadPriority.Normal;//设置线程优先级
            thread1.Start();//启动线程
            thread2.Start(10);//启动线程
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(count);
        }

        public void TestThreadPool() {
            ThreadPool.SetMaxThreads(6, 2);
            ThreadPool.SetMinThreads(2, 2);
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem((j) => { Console.WriteLine("hello " + j); }, i);
            }
            while (true) {
                int worker = 0;
                int io = 0;
                ThreadPool.GetAvailableThreads(out worker, out io);
                if (worker==6&&io==2)
                {
                    break;
                }
                System.Threading.Thread.Sleep(200);
            }
        }

        public void TestTask() {
            Task<int> t = new Task<int>((i)=> { return Sum((int)i,10);},20);
            Task<int> t1 = new Task<int>(i => Sum((int)i, 10), 20);
            t.Start();
            t1.Start();
            t.Wait();
            Console.WriteLine(t.Result);
            Task cwt = t1.ContinueWith(task => Console.WriteLine("The result is {0}", t1.Result));
        }

        private int Sum(int i, int j) {
            int sum = 0;
            checked { sum =i + j; }//结果溢出，抛出异常
            return sum;
        }
    }
}
