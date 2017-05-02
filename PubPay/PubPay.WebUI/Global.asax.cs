using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace PubPay.WebUI
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.BeginRequest += Application_BeginRequest;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.FilePath=="/")
            {
                Context.Response.Redirect("Alipay/Default.aspx");
            }
        }
    }
}