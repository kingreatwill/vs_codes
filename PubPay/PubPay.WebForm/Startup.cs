using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PubPay.WebForm.Startup))]
namespace PubPay.WebForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
