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
			return View(model);
		}

		public ActionResult Logout()
		{
			SessionContainer.ClearUserContext(HttpContext);
			return RedirectToAction("Login");
		}

		[AllowedAnonymous]
		public ActionResult Register()
		{
			RegisterViewModel model = new RegisterViewModel();
			return View(model);
		}

		[AllowedAnonymous]
		public ActionResult ResetPassword()
		{
			return View();
		}

		public ActionResult Introduction()
		{
			return View();
		}

		[AllowedAnonymous]
		public ActionResult ErrorView()
		{
			return View();
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}
