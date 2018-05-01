using Sheng.Enterprise.Infrastructure;
using System;
using System.Web;

namespace Sheng.Enterprise.Web
{
	public static class SessionContainer
	{
		public static void SetUserContext(HttpContextBase httpContext, UserContext userContext)
		{
			httpContext.Session["UserContext"] = userContext;
		}

		public static UserContext GetUserContext(HttpContextBase httpContext)
		{
			return httpContext.Session["UserContext"] as UserContext;
		}

		public static UserContext GetUserContext(HttpContext httpContext)
		{
			return httpContext.Session["UserContext"] as UserContext;
		}

		public static void ClearUserContext(HttpContextBase httpContext)
		{
			httpContext.Session.Clear();
		}
	}
}
