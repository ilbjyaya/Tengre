using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http.SelfHost;
using System.Threading;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        Thread tr = new Thread( new ThreadStart(callServ));
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            tr.Start();
        }
         
        private static void callServ()
        {
            using (Controllers.BaseController ba = new Controllers.BaseController())
            {
                ba.GetUserListJson();
            }
            while (true)
            {
                Thread.Sleep(600000);
                using (Controllers.BaseController ba = new Controllers.BaseController())
                {
                    ba.GetUserListJson();
                }
            }

        }
    }
}
