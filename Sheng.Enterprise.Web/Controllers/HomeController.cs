using Sheng.Enterprise.Core;
using Sheng.Enterprise.Web.Models;
using System;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web.Controllers
{
	public class HomeController : EnterpriseController
	{
		private static readonly UserManager _userManager = UserManager.Instance;

		private static readonly DomainManager _domainManager = DomainManager.Instance;

		[AllowedAnonymous]
		public ActionResult Login()
		{
			LoginViewModel model = new LoginViewModel();
			return base.View(model);
		}

		public ActionResult Logout()
		{
			SessionContainer.ClearUserContext(base.HttpContext);
			return base.RedirectToAction("Login");
		}

		[AllowedAnonymous]
		public ActionResult Register()
		{
			RegisterViewModel model = new RegisterViewModel();
			return base.View(model);
		}

		[AllowedAnonymous]
		public ActionResult ResetPassword()
		{
			return base.View();
		}

		public ActionResult Introduction()
		{
			return base.View();
		}

		[AllowedAnonymous]
		public ActionResult ErrorView()
		{
			return base.View();
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}
