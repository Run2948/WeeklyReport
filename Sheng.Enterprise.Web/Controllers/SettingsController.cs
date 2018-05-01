using Sheng.Enterprise.Core;
using Sheng.Enterprise.Web.Models;
using System;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web.Controllers
{
	public class SettingsController : EnterpriseController
	{
		private static readonly DomainManager _domainManager = DomainManager.Instance;

		private static readonly SettingsManager _settingsManager = SettingsManager.Instance;

		private static readonly UserManager _userManager = UserManager.Instance;

		public ActionResult Index()
		{
			return base.View();
		}

		public ActionResult Password()
		{
			return base.View();
		}

		[AuthorizationFilter("Settings_Organization")]
		public ActionResult Organization()
		{
			return base.View(new OrganizationViewModel
			{
				Domain = SettingsController._domainManager.GetDomain(base.UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_Organization")]
		public ActionResult OrganizationalStructure()
		{
			return base.View(new OrganizationalStructureViewModel
			{
				Domain = SettingsController._domainManager.GetDomain(base.UserContext.User.DomainId),
				OrganizationList = SettingsController._domainManager.GetOrganizationList(base.UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_User")]
		public new ActionResult User()
		{
			return base.View(new UserViewModel
			{
				Domain = SettingsController._domainManager.GetDomain(base.UserContext.User.DomainId),
				OrganizationList = SettingsController._domainManager.GetOrganizationList(base.UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_User")]
		public ActionResult UserDetail()
		{
			UserDetailViewModel userDetailViewModel = new UserDetailViewModel();
			userDetailViewModel.JobTitleList = SettingsController._settingsManager.GetJobTitleList(base.UserContext.User.DomainId);
			userDetailViewModel.JobLevelList = SettingsController._settingsManager.GetJobLevelList(base.UserContext.User.DomainId);
			userDetailViewModel.OfficeLocationList = SettingsController._settingsManager.GetOfficeLocationList(base.UserContext.User.DomainId);
			userDetailViewModel.WorkTypeList = SettingsController._settingsManager.GetWorkTypeList(base.UserContext.Domain.Id);
			string text = base.Request.QueryString["id"];
			Guid id;
			if (!string.IsNullOrEmpty(text) && Guid.TryParse(text, out id))
			{
				userDetailViewModel.User = SettingsController._userManager.GetUserDataWrapper(id);
				userDetailViewModel.UserWorkTypeList = SettingsController._userManager.GetUserWorkTypeList(id);
			}
			return base.View(userDetailViewModel);
		}

		[AuthorizationFilter("Settings_Role")]
		public ActionResult Role()
		{
			return base.View(new RoleViewModel
			{
				RoleList = SettingsController._settingsManager.GetRoleList(base.UserContext.Domain.Id)
			});
		}

		[AuthorizationFilter("Settings_BasalData")]
		public ActionResult JobTitle()
		{
			return base.View(new JobTitleViewModel
			{
				Domain = SettingsController._domainManager.GetDomain(base.UserContext.User.DomainId),
				JobTitleList = SettingsController._settingsManager.GetJobTitleList(base.UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_BasalData")]
		public ActionResult JobLevel()
		{
			return base.View(new JobLevelViewModel
			{
				Domain = SettingsController._domainManager.GetDomain(base.UserContext.User.DomainId),
				JobLevelList = SettingsController._settingsManager.GetJobLevelList(base.UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_BasalData")]
		public ActionResult OfficeLocation()
		{
			return base.View(new OfficeLocationViewModel
			{
				Domain = SettingsController._domainManager.GetDomain(base.UserContext.User.DomainId),
				OfficeLocationList = SettingsController._settingsManager.GetOfficeLocationList(base.UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("WeeklyReport_Settings")]
		public ActionResult WorkType()
		{
			return base.View(new WorkTypeViewModel
			{
				WorkTypeList = SettingsController._settingsManager.GetWorkTypeList(base.UserContext.User.DomainId),
				WorkTaskList = SettingsController._settingsManager.GetWorkTaskList(base.UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("WeeklyReport_Settings")]
		public ActionResult WorkStatus()
		{
			return base.View(new WorkStatusViewModel
			{
				WorkStatusList = SettingsController._settingsManager.GetWorkStatusList(base.UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("WeeklyReport_Settings")]
		public ActionResult CheckRelation()
		{
			return base.View(new CheckRelationViewModel
			{
				CheckerWrapperList = SettingsController._settingsManager.GetCheckerWrapperList(base.UserContext.Domain.Id)
			});
		}

		public ActionResult Feedback()
		{
			return base.View();
		}
	}
}
