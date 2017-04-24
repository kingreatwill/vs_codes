using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Common
{
    public class LazywgHandlerAttribute: ContextAttribute, IContributeObjectSink
    {
        public LazywgHandlerAttribute() : base("LazywgHandler")
        {
        }

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new LazywgHandler(nextSink);
        }
    }
}
