using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace WGRemoting
{
    public class RemotingHelper
    {
        private IChannel _channel;
        private int _port = 8080;

        /// <summary>
        /// 注册tcp信道
        /// </summary>
        public ObjRef RegisterTcpChannel(RemoteClientObject obj, string objName)
        {
            _channel = new TcpChannel(_port);
            ChannelServices.RegisterChannel(_channel, true);
            ObjRef oref = RemotingServices.Marshal(obj, objName);
            return oref;
        }

        /// <summary>
        /// 注册http信道
        /// </summary>
        public ObjRef RegisterHttpChannel(RemoteClientObject obj, string objName)
        {
            _channel = new HttpChannel(_port);
            ChannelServices.RegisterChannel(_channel, true);
            ObjRef oref = RemotingServices.Marshal(obj, objName);
            return oref;
        }

        /// <summary>
        /// 注册远程对象
        /// </summary>
        /// <param name="t"></param>
        /// <param name="typeName"></param>
        /// <param name="mode"></param>
        public void RegisterRemoteObject(Type t, string typeName, WellKnownObjectMode mode)
        {
            RemotingConfiguration.RegisterWellKnownServiceType(t, typeName, mode);
        }

        /// <summary>
        /// 客户端激活
        /// </summary>
        /// <param name="t"></param>
        /// <param name="uri"></param>
        public void ClientActivatedServiceType(Type t, string uri)
        {
            RemotingConfiguration.ApplicationName = uri;
            RemotingConfiguration.RegisterActivatedServiceType(t);
        }

        /// <summary>
        /// 获取服务的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        public T GetServerRemoteObject<T>(string uri)
        {
            //uri =  "tcp://localhost:8080/ServiceMessage"
            T t = (T)Activator.GetObject(typeof(T), uri);
            return t;
        }

        /// <summary>
        /// 获取客户端对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public T GetClientRemoteObject<T>(object[] objs, string uri) where T : new()
        {
            //uri =  "tcp://localhost:8080/ServiceMessage"
            RemotingConfiguration.RegisterActivatedClientType(typeof(T), uri);
            object[] attrs = { new UrlAttribute(uri) };
            T t = (T)Activator.CreateInstance(typeof(T), objs, attrs);
            return t;
        }

        public void UnRegisterAllRomoteChannel()
        {

            //获得当前已注册的通道；
            IChannel[] channels = ChannelServices.RegisteredChannels;

            foreach (IChannel item in channels)
            {
                if (item is TcpChannel)
                {
                    TcpChannel channel = (TcpChannel)item;

                    //关闭监听；
                    channel.StopListening(null);

                    //注销通道；
                    ChannelServices.UnregisterChannel(channel);
                }
                if (item is HttpChannel)
                {
                    HttpChannel channel = (HttpChannel)item;

                    //关闭监听；
                    channel.StopListening(null);
                    //注销通道；
                    ChannelServices.UnregisterChannel(channel);
                }
            }
        }

        public void UnRegisterRomoteChannel(string channelName)
        {
            //获得当前已注册的通道；
            IChannel[] channels = ChannelServices.RegisteredChannels;

            foreach (IChannel item in channels)
            {
                if (item.ChannelName.Equals(channelName))
                {
                    if (item is TcpChannel)
                    {
                        TcpChannel channel = (TcpChannel)item;

                        //关闭监听；
                        channel.StopListening(null);

                        //注销通道；
                        ChannelServices.UnregisterChannel(channel);
                    }
                    if (item is HttpChannel)
                    {
                        HttpChannel channel = (HttpChannel)item;

                        //关闭监听；
                        channel.StopListening(null);
                        //注销通道；
                        ChannelServices.UnregisterChannel(channel);
                    }
                    break;
                }
            }
        }
    }
}
