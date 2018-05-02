using Sheng.Enterprise.Core;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web.Areas.Api.Controllers
{
	public class DomainController : EnterpriseController
	{
		private DomainManager _domainManager = DomainManager.Instance;

		[HttpPost]
		public ActionResult GetDomain()
		{
			Domain domain = _domainManager.GetDomain(UserContext.User.DomainId);
			return RespondDataResult(domain);
		}

		[HttpPost]
		public ActionResult UpdateDomain()
		{
			Domain domain = RequestArgs<Domain>();
			if (domain == null)
			{
				return RespondResult(false, "参数无效。");
			}
			domain.Id = UserContext.User.DomainId;
			_domainManager.UpdateDomain(domain);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult GetOrganization()
		{
			string input = Request.QueryString["id"];
			Organization organization = _domainManager.GetOrganization(Guid.Parse(input));
			return RespondDataResult(organization);
		}

		[HttpPost]
		public ActionResult UpdateOrganization()
		{
			Organization organization = RequestArgs<Organization>();
			if (organization == null)
			{
				return RespondResult(false, "参数无效。");
			}
			_domainManager.UpdateOrganization(organization);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult CreateOrganization()
		{
			Organization organization = RequestArgs<Organization>();
			if (organization == null)
			{
				return RespondResult(false, "参数无效。");
			}
			organization.Id = Guid.NewGuid();
			_domainManager.CreateOrganization(organization);
			return RespondDataResult(new
			{
				organization.Id
			});
		}

		[HttpPost]
		public ActionResult RemoveOrganization()
		{
			string input = Request.QueryString["id"];
			_domainManager.RemoveOrganization(Guid.Parse(input));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult GetOrganizationList()
		{
			string input = Request.QueryString["domainId"];
			List<Organization> organizationList = _domainManager.GetOrganizationList(Guid.Parse(input));
			return RespondDataResult(organizationList);
		}

		public ActionResult MoveOrganization(Guid id, Guid id2)
		{
			_domainManager.SwapSort(id, id2, "Organization");
			return RespondResult();
		}
	}
}
