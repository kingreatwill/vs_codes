using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PubPay.WebUI.Startup))]
namespace PubPay.WebUI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
