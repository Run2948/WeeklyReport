using Sheng.Enterprise.Core;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web.Areas.Api.Controllers
{
	public class SettingsController : EnterpriseController
	{
		private SettingsManager _settingsManager = SettingsManager.Instance;

		[HttpPost]
		public ActionResult CreateJobTitle()
		{
			JobTitle jobTitle = RequestArgs<JobTitle>();
			if (jobTitle == null)
			{
				return RespondResult(false, "参数无效。");
			}
			jobTitle.Id = Guid.NewGuid();
			_settingsManager.CreateJobTitle(jobTitle);
			return RespondDataResult(new
			{
				jobTitle.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateJobTitle()
		{
			JobTitle jobTitle = RequestArgs<JobTitle>();
			if (jobTitle == null)
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.UpdateJobTitle(jobTitle);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveJobTitle()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveJobTitle(Guid.Parse(text));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult CreateJobLevel()
		{
			JobLevel jobLevel = RequestArgs<JobLevel>();
			if (jobLevel == null)
			{
				return RespondResult(false, "参数无效。");
			}
			jobLevel.Id = Guid.NewGuid();
			_settingsManager.CreateJobLevel(jobLevel);
			return RespondDataResult(new
			{
				jobLevel.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateJobLevel()
		{
			JobLevel jobLevel = RequestArgs<JobLevel>();
			if (jobLevel == null)
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.UpdateJobLevel(jobLevel);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveJobLevel()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveJobLevel(Guid.Parse(text));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult CreateOfficeLocation()
		{
			OfficeLocation officeLocation = RequestArgs<OfficeLocation>();
			if (officeLocation == null)
			{
				return RespondResult(false, "参数无效。");
			}
			officeLocation.Id = Guid.NewGuid();
			_settingsManager.CreateOfficeLocation(officeLocation);
			return RespondDataResult(new
			{
				officeLocation.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateOfficeLocation()
		{
			OfficeLocation officeLocation = RequestArgs<OfficeLocation>();
			if (officeLocation == null)
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.UpdateOfficeLocation(officeLocation);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveOfficeLocation()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveOfficeLocation(Guid.Parse(text));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult CreateWorkType()
		{
			WorkType workType = RequestArgs<WorkType>();
			if (workType == null)
			{
				return RespondResult(false, "参数无效。");
			}
			workType.Id = Guid.NewGuid();
			_settingsManager.CreateWorkType(workType);
			return RespondDataResult(new
			{
				workType.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateWorkType()
		{
			WorkType workType = RequestArgs<WorkType>();
			if (workType == null)
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.UpdateWorkType(workType);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveWorkType()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveWorkType(Guid.Parse(text));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult CreateWorkTask()
		{
			WorkTask workTask = RequestArgs<WorkTask>();
			if (workTask == null)
			{
				return RespondResult(false, "参数无效。");
			}
			workTask.Id = Guid.NewGuid();
			_settingsManager.CreateWorkTask(workTask);
			return RespondDataResult(new
			{
				workTask.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateWorkTask()
		{
			WorkTask workTask = RequestArgs<WorkTask>();
			if (workTask == null)
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.UpdateWorkTask(workTask);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveWorkTask()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveWorkTask(Guid.Parse(text));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult CreateWorkStatus()
		{
			WorkStatus workStatus = RequestArgs<WorkStatus>();
			if (workStatus == null)
			{
				return RespondResult(false, "参数无效。");
			}
			workStatus.Id = Guid.NewGuid();
			_settingsManager.CreateWorkStatus(workStatus);
			return RespondDataResult(new
			{
				workStatus.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateWorkStatus()
		{
			WorkStatus workStatus = RequestArgs<WorkStatus>();
			if (workStatus == null)
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.UpdateWorkStatus(workStatus);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveWorkStatus()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveWorkStatus(Guid.Parse(text));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult CreateCheckRelation()
		{
			CheckRelationJsonContract checkRelationJsonContract = RequestArgs<CheckRelationJsonContract>();
			if (checkRelationJsonContract == null)
			{
				return RespondResult(false, "参数无效。");
			}
			checkRelationJsonContract.Domain = UserContext.Domain.Id;
			_settingsManager.CreateCheckRelation(checkRelationJsonContract);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveCheckRelation()
		{
			string text = Request.QueryString["checker"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveCheckRelation(Guid.Parse(text));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult GetCheckStaffList()
		{
			string text = Request.QueryString["checker"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			List<CheckStaffWrapper> checkStaffList = _settingsManager.GetCheckStaffList(Guid.Parse(text));
			return RespondDataResult(checkStaffList);
		}

		[HttpPost]
		public ActionResult CreateRole()
		{
			Role role = RequestArgs<Role>();
			if (role == null)
			{
				return RespondResult(false, "参数无效。");
			}
			role.Id = Guid.NewGuid();
			role.Domain = UserContext.Domain.Id;
			_settingsManager.CreateRole(role);
			return RespondDataResult(new
			{
				role.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateRole()
		{
			Role role = RequestArgs<Role>();
			if (role == null)
			{
				return RespondResult(false, "参数无效。");
			}
			role.Domain = UserContext.Domain.Id;
			_settingsManager.UpdateRole(role);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveRole()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveRole(Guid.Parse(text));
			return RespondResult();
		}

		[HttpPost]
		public ActionResult GetRole()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			Role role = _settingsManager.GetRole(Guid.Parse(text));
			return RespondDataResult(role);
		}

		[HttpPost]
		public ActionResult GetAuthorizationListByRoleId()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			List<Authorization> authorizationListByRoleId = _settingsManager.GetAuthorizationListByRoleId(Guid.Parse(text));
			return RespondDataResult(authorizationListByRoleId);
		}

		[HttpPost]
		public ActionResult UpdateAuthorizationListByRoleId()
		{
			RoleAuthorizationRelation roleAuthorizationRelation = RequestArgs<RoleAuthorizationRelation>();
			if (roleAuthorizationRelation == null)
			{
				return RespondResult(false, "参数无效。");
			}
			roleAuthorizationRelation.Domain = UserContext.Domain.Id;
			_settingsManager.UpdateAuthorizationListByRoleId(roleAuthorizationRelation);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult AddUserToRole()
		{
			List<RoleUser> list = RequestArgs<List<RoleUser>>();
			if (list == null)
			{
				return RespondResult(false, "参数无效。");
			}
			using (List<RoleUser>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.Domain = UserContext.Domain.Id;
				}
			}
			_settingsManager.AddUserToRole(list);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveUserFromRole()
		{
			RoleUser roleUser = RequestArgs<RoleUser>();
			if (roleUser == null)
			{
				return RespondResult(false, "参数无效。");
			}
			_settingsManager.RemoveUserFromRole(roleUser);
			return RespondResult();
		}

		[HttpPost]
		public ActionResult GetUserListByRoleId()
		{
			string text = Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return RespondResult(false, "参数无效。");
			}
			List<User> userListByRoleId = _settingsManager.GetUserListByRoleId(Guid.Parse(text));
			return RespondDataResult(userListByRoleId);
		}

		[HttpPost]
		public ActionResult Feedback()
		{
			Feedback feedback = RequestArgs<Feedback>();
			if (feedback == null)
			{
				return RespondResult(false, "参数无效。");
			}
			feedback.Domain = UserContext.Domain.Id;
			feedback.User = UserContext.User.Id;
			feedback.IP = HttpContext.Request.UserHostAddress;
			feedback.Time = DateTime.Now;
			_settingsManager.Feedback(feedback);
			return RespondResult();
		}
	}
}
