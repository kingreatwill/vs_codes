using RemoteHelper;
using RemoteModel;
using System;

namespace RemoteClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("客户端【启动】");

            //注册通道
            RemotingHelper.RegisterClientTcpChannel();
            RemotingHelper.RegisterClentHttpChannel();

            //获取远程对象
            RemotableType remoteObject1 = RemotingHelper.GetServerRemoteObject<RemotableType>("TCP://localhost:8989/RemotableType");
            RemotableType remoteObject2 = RemotingHelper.GetServerRemoteObject<RemotableType>("http://localhost:8090/RemotableType");

            Console.WriteLine(remoteObject1.SayHello());
            Console.WriteLine(remoteObject2.SayHello());

            Type t = typeof(ClientModel);
            RemotingHelper.SetApplicationName(t.Name);
            ////客户端激活远程对象
            RemotingHelper.ClientActivatedServiceType(t);
            ////获取远程对象
            ClientModel model = RemotingHelper.GetServerRemoteObject<ClientModel>("http://localhost:8090/ClientModel");
            Console.WriteLine(model.ClientActivited("我是客户端"));

            Console.ReadLine();
        }
    }
}
