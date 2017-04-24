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

namespace RemoteHelper
{
    public class RemotingHelper
    {
        private static IChannel _channel;


        private static void RegisterChannel(bool ensureSecurity)
        {
            ChannelServices.RegisterChannel(_channel, ensureSecurity);
        }

        /// <summary>
        /// 注册tcp信道
        /// </summary>
        public static void RegisterServerTcpChannel(int port, bool ensureSecurity = false)
        {
            _channel = new TcpChannel(port);
            RegisterChannel(ensureSecurity);
        }

        /// <summary>
        /// 注册tcp信道
        /// </summary>
        public static void RegisterClientTcpChannel(bool ensureSecurity = false)
        {
            _channel = new TcpClientChannel();
            RegisterChannel(ensureSecurity);
        }

        /// <summary>
        /// 注册http信道
        /// </summary>
        public static void RegisterServerHttpChannel(int port, bool ensureSecurity = false)
        {
            _channel = new HttpChannel(port);
            RegisterChannel(ensureSecurity);
        }
        /// <summary>
        /// 注册http信道
        /// </summary>
        public static void RegisterClentHttpChannel(bool ensureSecurity = false)
        {
            _channel = new HttpClientChannel();
            RegisterChannel(ensureSecurity);
        }

        /// <summary>
        /// 注册远程对象(服务端激活)
        /// 
        /// 同一类型对象只可以用一种方式注册(客户激活 或者 服务激活)。
        /// 即是说如果使用上面的方法注册对象，那么要么调用 ClientActivated，
        /// 要么调用SingleCall或者Singleton，而不能都调用。
        /// 上面的RegisterWellKnownServiceType()方法接受三个参数：
        /// 1.允许进行远程访问的对象类型信息；
        /// 2.远程对象的名称，用于定位远程对象；
        /// 3.服务激活对象的方式，Singleton或者Single Call。
        /// 
        /// 服务激活 Singleton(Server activated Singleton)
        /// 这个模式的最大特色就是所有的客户共享同一个对象。服务端只在对象第一次被调用时创建服务对象，对于后继的访问使用同一个对象提供服务
        /// ，由于Singleton对象的状态由其它对象所共享，所以使用Singleton对象应该考虑线程同步 的问题.
        /// 
        /// 服务激活 Single Call(Server activated Single Call)
        /// Single Call方式是对每一次请求(比如方法调用)创建一个对象，而在每次方法返回之后销毁对象。由此可见Single Call 方式的最大特点就是 不保存状态。
        /// 使用Single Call的好处就是不会过久地占用资源，因为方法返回后对资源的占用就随对象被销毁而释放了
        /// </summary>
        /// <param name="t"></param>
        /// <param name="objUri"></param>
        /// <param name="mode"></param>
        public static void RegisterRemoteObject(Type t, string objUri, WellKnownObjectMode mode)
        {
            RemotingConfiguration.RegisterWellKnownServiceType(t, objUri, mode);
        }

        /// <summary>
        /// 客户端激活
        /// 客户激活(Client activated )
        /// 客户激活模式的缺点就是 如果客户端过多时，或者服务对象为“大对象”时，服务器端的压力过大。
        /// 另外，客户程序可能只需要调用服务对象的一个方法，但是却持有服务对象过长时间，这样浪费了服务器的资源。
        /// </summary>
        /// <param name="t"></param>
        public static void ClientActivatedServiceType(Type t)
        {
            //RemotingConfiguration.ApplicationName = uri;
            RemotingConfiguration.RegisterActivatedServiceType(t);
        }

        /// <summary>
        /// 添加应用域
        ///RemotingConfiguration类型还有一个ApplicationName静态属性，当设置了这个属性之后，对于客户激活对象，可以提供此ApplicationName作为Url参数，也可以不提供。如果提供ApplicationName，则必须与服务端设置的ApplicationName相匹配；对于服务激活对象，访问时必须提供ApplicationName，此时两种方式的Uri为下面的形式：
        ///protocal://hostadrress:port/ApplicationName/ObjectUrl       // Server Activated
        ///protocal://hostadrress:port                     // Client Activated Object
        ///protocal:// hostadrress:port/ApplicationName    // Client Activated Object
        ///比如，如果通道采用协议为tcp，服务器地址为127.0.0.1，端口号为8051，ApplicationName设为DemoApp，ObjectUrl设为RemoteObject(ObjUrl为使用RegisterWellKnownServiceType()方法注册服务激活对象时第2个参数所提供的字符串；注意客户激活对象不使用它)，则客户端在访问时需要提供的地址为：
        ///tcp://127.0.0.1:8051/DemoApp/RemoteObject   // Server Activated Object
        ///tcp://127.0.0.1:8051/DemoApp                // Client Activated Object
        ///tcp://127.0.0.1:8051                        // Client Activated Object
        ///如果RemotingConfiguration类型没有设置ApplicationName静态属性，则客户端在获取远程对象时不需要提供ApplicationName，此时Url变为下面形式：
        ///protocal://hostadrress:port/ObjectUrl       // Server Activated Object
        ///protocal://hostadrress:port                 // Client Activated Object
        /// </summary>
        /// <param name="appName"></param>
        public static void SetApplicationName(string appName)
        {
            RemotingConfiguration.ApplicationName = appName;
        }

        /// <summary>
        /// 获取服务的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static T GetServerRemoteObject<T>(string uri) where T : class
        {
            T t =Activator.GetObject(typeof(T), uri) as T;
            return t;
        }

        /// <summary>
        /// 获取客户端对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs">构造参数</param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static T GetClientRemoteObject<T>(object[] objs, string uri) where T : class
        {
            RemotingConfiguration.RegisterActivatedClientType(typeof(T), uri);
            object[] attrs = { new UrlAttribute(uri) };
            T t = Activator.CreateInstance(typeof(T), objs, attrs) as T;
            return t;
        }

        public static void UnRegisterAllRomoteChannel()
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

        public static void UnRegisterRomoteChannel(string channelName)
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
