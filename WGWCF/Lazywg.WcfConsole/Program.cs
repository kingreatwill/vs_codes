using Lazywg.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.WcfConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost service = new ServiceHost(typeof(IService));
            ServiceHost test = new ServiceHost(typeof(ITest));

            //ServiceClient proxy = new ServiceClient();
            //CompositeType composite= proxy.GetDataUsingDataContract(new CompositeType() { BoolValue=true,StringValue="hello"});
            //Console.WriteLine(composite.StringValue);

            Console.ReadKey();
        }
    }
}
