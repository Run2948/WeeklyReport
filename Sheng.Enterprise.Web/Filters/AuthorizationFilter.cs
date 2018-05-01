using System;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web
{
	public class AuthorizationFilter : ActionFilterAttribute
	{
		public string Key
		{
			get;
			set;
		}

		public AuthorizationFilter(string key)
		{
			this.Key = key;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			EnterpriseController enterpriseController = filterContext.Controller as EnterpriseController;
			if (enterpriseController.UserContext == null)
			{
				filterContext.Result = new RedirectResult("~/Home/Login");
			}
			if (enterpriseController.UserContext.RoleList != null && enterpriseController.UserContext.RoleList.Count != 0 && !enterpriseController.UserContext.Authorization.Verify(this.Key))
			{
				filterContext.Result = new RedirectResult("~/Home/Login");
			}
		}
	}
}
