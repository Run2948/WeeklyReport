using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sheng.Enterprise.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute("Default", "{controller}/{action}", new
			{
				controller = "WeeklyReport",
				action = "Post"
			}, new string[]
			{
				"Sheng.Enterprise.Web.Controllers"
			});
		}
	}
}
