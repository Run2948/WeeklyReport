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
			return View();
		}

		public ActionResult Password()
		{
			return View();
		}

		[AuthorizationFilter("Settings_Organization")]
		public ActionResult Organization()
		{
			return View(new OrganizationViewModel
			{
				Domain = _domainManager.GetDomain(UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_Organization")]
		public ActionResult OrganizationalStructure()
		{
			return View(new OrganizationalStructureViewModel
			{
				Domain = _domainManager.GetDomain(UserContext.User.DomainId),
				OrganizationList = _domainManager.GetOrganizationList(UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_User")]
		public new ActionResult User()
		{
			return View(new UserViewModel
			{
				Domain = _domainManager.GetDomain(UserContext.User.DomainId),
				OrganizationList = _domainManager.GetOrganizationList(UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_User")]
		public ActionResult UserDetail()
		{
		    UserDetailViewModel userDetailViewModel =
		        new UserDetailViewModel
		        {
		            JobTitleList = _settingsManager.GetJobTitleList(UserContext.User.DomainId),
		            JobLevelList = _settingsManager.GetJobLevelList(UserContext.User.DomainId),
		            OfficeLocationList = _settingsManager.GetOfficeLocationList(UserContext.User.DomainId),
		            WorkTypeList = _settingsManager.GetWorkTypeList(UserContext.Domain.Id)
		        };
		    string text = Request.QueryString["id"];
			Guid id;
			if (!string.IsNullOrEmpty(text) && Guid.TryParse(text, out id))
			{
				userDetailViewModel.User = _userManager.GetUserDataWrapper(id);
				userDetailViewModel.UserWorkTypeList = _userManager.GetUserWorkTypeList(id);
			}
			return View(userDetailViewModel);
		}

		[AuthorizationFilter("Settings_Role")]
		public ActionResult Role()
		{
			return View(new RoleViewModel
			{
				RoleList = _settingsManager.GetRoleList(UserContext.Domain.Id)
			});
		}

		[AuthorizationFilter("Settings_BasalData")]
		public ActionResult JobTitle()
		{
			return View(new JobTitleViewModel
			{
				Domain = _domainManager.GetDomain(UserContext.User.DomainId),
				JobTitleList = _settingsManager.GetJobTitleList(UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_BasalData")]
		public ActionResult JobLevel()
		{
			return View(new JobLevelViewModel
			{
				Domain = _domainManager.GetDomain(UserContext.User.DomainId),
				JobLevelList = _settingsManager.GetJobLevelList(UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("Settings_BasalData")]
		public ActionResult OfficeLocation()
		{
			return View(new OfficeLocationViewModel
			{
				Domain = _domainManager.GetDomain(UserContext.User.DomainId),
				OfficeLocationList = _settingsManager.GetOfficeLocationList(UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("WeeklyReport_Settings")]
		public ActionResult WorkType()
		{
			return View(new WorkTypeViewModel
			{
				WorkTypeList = _settingsManager.GetWorkTypeList(UserContext.User.DomainId),
				WorkTaskList = _settingsManager.GetWorkTaskList(UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("WeeklyReport_Settings")]
		public ActionResult WorkStatus()
		{
			return View(new WorkStatusViewModel
			{
				WorkStatusList = _settingsManager.GetWorkStatusList(UserContext.User.DomainId)
			});
		}

		[AuthorizationFilter("WeeklyReport_Settings")]
		public ActionResult CheckRelation()
		{
			return View(new CheckRelationViewModel
			{
				CheckerWrapperList = _settingsManager.GetCheckerWrapperList(UserContext.Domain.Id)
			});
		}

		public ActionResult Feedback()
		{
			return View();
		}
	}
}
