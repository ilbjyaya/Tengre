using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc; 
using System.Web.Routing;

namespace LeaRun.Application.Web
{
    /// <summary>
    /// 应用程序全局设置
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {            
        }

        /// <summary>
        /// 应用程序错误处理
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string hPath = Request.Url.LocalPath.ToString();
            if (!hPath.Contains("/tengreser.asmx"))
            {
                if (hPath.Contains("/API"))
                {
                    Context.RewritePath(hPath.Replace("/API", "/tengreser.asmx"));
                }
            }
        }
    }
}