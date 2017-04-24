using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common
{
    public class LazywgHandler : IMessageSink
    {
        //下一个接收器
        private IMessageSink nextSink;

        public IMessageSink NextSink
        {
            get { return nextSink; }
        }

        public LazywgHandler() { }

        public LazywgHandler(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }

        //同步处理方法
        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMessage retMsg = null;

            //方法调用消息接口
            IMethodCallMessage call = msg as IMethodCallMessage;

            //如果被调用的方法没打ResultFormateAttribute标签
            if (call == null || (Attribute.GetCustomAttribute(call.MethodBase, typeof(ResultFormateAttribute))) == null)
            {
                retMsg = nextSink.SyncProcessMessage(msg);
            }
            //如果打了ResultFormateAttribute标签
            else
            {
                Console.WriteLine("开始执行");

                retMsg = nextSink.SyncProcessMessage(msg);

                Console.WriteLine("执行完成");
            }
            return retMsg;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new NotImplementedException();
        }
    }
}
