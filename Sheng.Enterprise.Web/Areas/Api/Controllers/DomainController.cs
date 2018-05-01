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
			Domain domain = this._domainManager.GetDomain(base.UserContext.User.DomainId);
			return this.RespondDataResult(domain);
		}

		[HttpPost]
		public ActionResult UpdateDomain()
		{
			Domain domain = base.RequestArgs<Domain>();
			if (domain == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			domain.Id = base.UserContext.User.DomainId;
			this._domainManager.UpdateDomain(domain);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult GetOrganization()
		{
			string input = base.Request.QueryString["id"];
			Organization organization = this._domainManager.GetOrganization(Guid.Parse(input));
			return this.RespondDataResult(organization);
		}

		[HttpPost]
		public ActionResult UpdateOrganization()
		{
			Organization organization = base.RequestArgs<Organization>();
			if (organization == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._domainManager.UpdateOrganization(organization);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult CreateOrganization()
		{
			Organization organization = base.RequestArgs<Organization>();
			if (organization == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			organization.Id = Guid.NewGuid();
			this._domainManager.CreateOrganization(organization);
			return this.RespondDataResult(new
			{
				organization.Id
			});
		}

		[HttpPost]
		public ActionResult RemoveOrganization()
		{
			string input = base.Request.QueryString["id"];
			this._domainManager.RemoveOrganization(Guid.Parse(input));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult GetOrganizationList()
		{
			string input = base.Request.QueryString["domainId"];
			List<Organization> organizationList = this._domainManager.GetOrganizationList(Guid.Parse(input));
			return this.RespondDataResult(organizationList);
		}

		public ActionResult MoveOrganization(Guid id, Guid id2)
		{
			this._domainManager.SwapSort(id, id2, "Organization");
			return this.RespondResult();
		}
	}
}
