using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core
{
    public class EFContext : DbContext
    {
        public EFContext() : base("ConStr") { }

        public EFContext(string nameOrConStr) : base(nameOrConStr) {
        }
    }
}
