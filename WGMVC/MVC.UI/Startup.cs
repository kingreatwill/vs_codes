using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC.UI.Startup))]
namespace MVC.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
