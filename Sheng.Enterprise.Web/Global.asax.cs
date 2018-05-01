using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Linkup.Common;
using Sheng.Enterprise.Core;

namespace Sheng.Enterprise.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private LogService _logService = LogService.Instance;

        private ExceptionHandlingService _exceptionHandling = ServiceUnity.Instance.ExceptionHandling;

        protected void Application_Start()
        {
            this._logService.Write("Application_Start");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            Exception lastError = base.Server.GetLastError();
            if (lastError == null)
            {
                return;
            }
            base.Server.ClearError();
            this._exceptionHandling.HandleException(lastError);
        }
    }
}
