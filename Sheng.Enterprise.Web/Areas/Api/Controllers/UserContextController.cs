using Sheng.Enterprise.Core;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web.Areas.Api.Controllers
{
	public class UserContextController : EnterpriseController
	{
		private static readonly UserManager _userManager = UserManager.Instance;

		private static readonly DomainManager _domainManager = DomainManager.Instance;

		[AllowedAnonymous]
		public ActionResult Login()
		{
			LoginArgs loginArgs = base.RequestArgs<LoginArgs>();
			if (loginArgs == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			User user = UserContextController._userManager.Verify(loginArgs.Account, loginArgs.Password);
			if (user == null)
			{
				return this.RespondResult(false, "帐号或密码错误。");
			}
			Domain domain = UserContextController._domainManager.GetDomain(user.DomainId);
			AuthorizationWrapper authorizationWrapper = new AuthorizationWrapper();
			authorizationWrapper.AuthorizationList = UserContextController._userManager.GetAuthorizationListByUser(user.Id);
			UserContext userContext = new UserContext(user, domain);
			userContext.RootOrganization = UserContextController._domainManager.GetOrganization(domain.Id);
			userContext.Authorization = authorizationWrapper;
			userContext.RoleList = UserContextController._userManager.GetRoleListByUser(user.Id);
			userContext.Organization = UserContextController._domainManager.GetOrganization(user.OrganizationId);
			SessionContainer.SetUserContext(base.HttpContext, userContext);
			return this.RespondResult();
		}

		[AllowedAnonymous]
		public ActionResult GetValidateCode()
		{
			ValidateCode expr_05 = new ValidateCode();
			string text = expr_05.CreateValidateCode(5);
			base.Session["ValidateCode"] = text;
			byte[] fileContents = expr_05.CreateValidateGraphic(text);
			return base.File(fileContents, "image/jpeg");
		}

		public ActionResult Heartbeat()
		{
			return this.RespondResult();
		}
	}
}
