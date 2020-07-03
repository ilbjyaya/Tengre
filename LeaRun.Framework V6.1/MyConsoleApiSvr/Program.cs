using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Web.Http; 
using System.Web.Http.SelfHost;

namespace MyConsoleApiSvr
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8644");
            var apiDll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebApi.dll");
            Assembly.LoadFrom(apiDll);

            config.Routes.MapHttpRoute(
                name: "api",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
               );

            using (var svr = new HttpSelfHostServer(config))
            {
                svr.OpenAsync().Wait();
                Console.WriteLine("API服务已开启！");
                Console.ReadLine();
            }
        }
    }
}
