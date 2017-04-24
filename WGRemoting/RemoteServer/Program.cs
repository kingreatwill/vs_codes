using RemoteHelper;
using RemoteModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("服务端【启动】");

            //开启双信道
            RemotingHelper.RegisterServerTcpChannel(8989);

            RemotingHelper.RegisterServerHttpChannel(8090);

            Type t = typeof(RemotableType);
            
            //服务端注册激活远程对象
            RemotingHelper.RegisterRemoteObject(t,t.Name, WellKnownObjectMode.SingleCall);

            Console.ReadLine();
        }
    }
}
