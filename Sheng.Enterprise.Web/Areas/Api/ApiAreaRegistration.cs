using System;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Api_default", "Api/{controller}/{action}", new
            {
                action = "Index"
            }, new string[]
            {
                "Sheng.Enterprise.Web.Areas.Api.Controllers"
            });
        }
    }
}
