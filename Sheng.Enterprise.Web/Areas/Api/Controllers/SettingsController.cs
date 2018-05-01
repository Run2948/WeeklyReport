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
			JobTitle jobTitle = base.RequestArgs<JobTitle>();
			if (jobTitle == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			jobTitle.Id = Guid.NewGuid();
			this._settingsManager.CreateJobTitle(jobTitle);
			return this.RespondDataResult(new
			{
				jobTitle.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateJobTitle()
		{
			JobTitle jobTitle = base.RequestArgs<JobTitle>();
			if (jobTitle == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.UpdateJobTitle(jobTitle);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveJobTitle()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveJobTitle(Guid.Parse(text));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult CreateJobLevel()
		{
			JobLevel jobLevel = base.RequestArgs<JobLevel>();
			if (jobLevel == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			jobLevel.Id = Guid.NewGuid();
			this._settingsManager.CreateJobLevel(jobLevel);
			return this.RespondDataResult(new
			{
				jobLevel.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateJobLevel()
		{
			JobLevel jobLevel = base.RequestArgs<JobLevel>();
			if (jobLevel == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.UpdateJobLevel(jobLevel);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveJobLevel()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveJobLevel(Guid.Parse(text));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult CreateOfficeLocation()
		{
			OfficeLocation officeLocation = base.RequestArgs<OfficeLocation>();
			if (officeLocation == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			officeLocation.Id = Guid.NewGuid();
			this._settingsManager.CreateOfficeLocation(officeLocation);
			return this.RespondDataResult(new
			{
				officeLocation.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateOfficeLocation()
		{
			OfficeLocation officeLocation = base.RequestArgs<OfficeLocation>();
			if (officeLocation == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.UpdateOfficeLocation(officeLocation);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveOfficeLocation()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveOfficeLocation(Guid.Parse(text));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult CreateWorkType()
		{
			WorkType workType = base.RequestArgs<WorkType>();
			if (workType == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			workType.Id = Guid.NewGuid();
			this._settingsManager.CreateWorkType(workType);
			return this.RespondDataResult(new
			{
				workType.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateWorkType()
		{
			WorkType workType = base.RequestArgs<WorkType>();
			if (workType == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.UpdateWorkType(workType);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveWorkType()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveWorkType(Guid.Parse(text));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult CreateWorkTask()
		{
			WorkTask workTask = base.RequestArgs<WorkTask>();
			if (workTask == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			workTask.Id = Guid.NewGuid();
			this._settingsManager.CreateWorkTask(workTask);
			return this.RespondDataResult(new
			{
				workTask.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateWorkTask()
		{
			WorkTask workTask = base.RequestArgs<WorkTask>();
			if (workTask == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.UpdateWorkTask(workTask);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveWorkTask()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveWorkTask(Guid.Parse(text));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult CreateWorkStatus()
		{
			WorkStatus workStatus = base.RequestArgs<WorkStatus>();
			if (workStatus == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			workStatus.Id = Guid.NewGuid();
			this._settingsManager.CreateWorkStatus(workStatus);
			return this.RespondDataResult(new
			{
				workStatus.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateWorkStatus()
		{
			WorkStatus workStatus = base.RequestArgs<WorkStatus>();
			if (workStatus == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.UpdateWorkStatus(workStatus);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveWorkStatus()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveWorkStatus(Guid.Parse(text));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult CreateCheckRelation()
		{
			CheckRelationJsonContract checkRelationJsonContract = base.RequestArgs<CheckRelationJsonContract>();
			if (checkRelationJsonContract == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			checkRelationJsonContract.Domain = base.UserContext.Domain.Id;
			this._settingsManager.CreateCheckRelation(checkRelationJsonContract);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveCheckRelation()
		{
			string text = base.Request.QueryString["checker"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveCheckRelation(Guid.Parse(text));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult GetCheckStaffList()
		{
			string text = base.Request.QueryString["checker"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			List<CheckStaffWrapper> checkStaffList = this._settingsManager.GetCheckStaffList(Guid.Parse(text));
			return this.RespondDataResult(checkStaffList);
		}

		[HttpPost]
		public ActionResult CreateRole()
		{
			Role role = base.RequestArgs<Role>();
			if (role == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			role.Id = Guid.NewGuid();
			role.Domain = base.UserContext.Domain.Id;
			this._settingsManager.CreateRole(role);
			return this.RespondDataResult(new
			{
				role.Id
			});
		}

		[HttpPost]
		public ActionResult UpdateRole()
		{
			Role role = base.RequestArgs<Role>();
			if (role == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			role.Domain = base.UserContext.Domain.Id;
			this._settingsManager.UpdateRole(role);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveRole()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveRole(Guid.Parse(text));
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult GetRole()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			Role role = this._settingsManager.GetRole(Guid.Parse(text));
			return this.RespondDataResult(role);
		}

		[HttpPost]
		public ActionResult GetAuthorizationListByRoleId()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			List<Authorization> authorizationListByRoleId = this._settingsManager.GetAuthorizationListByRoleId(Guid.Parse(text));
			return this.RespondDataResult(authorizationListByRoleId);
		}

		[HttpPost]
		public ActionResult UpdateAuthorizationListByRoleId()
		{
			RoleAuthorizationRelation roleAuthorizationRelation = base.RequestArgs<RoleAuthorizationRelation>();
			if (roleAuthorizationRelation == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			roleAuthorizationRelation.Domain = base.UserContext.Domain.Id;
			this._settingsManager.UpdateAuthorizationListByRoleId(roleAuthorizationRelation);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult AddUserToRole()
		{
			List<RoleUser> list = base.RequestArgs<List<RoleUser>>();
			if (list == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			using (List<RoleUser>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.Domain = base.UserContext.Domain.Id;
				}
			}
			this._settingsManager.AddUserToRole(list);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult RemoveUserFromRole()
		{
			RoleUser roleUser = base.RequestArgs<RoleUser>();
			if (roleUser == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			this._settingsManager.RemoveUserFromRole(roleUser);
			return this.RespondResult();
		}

		[HttpPost]
		public ActionResult GetUserListByRoleId()
		{
			string text = base.Request.QueryString["id"];
			if (string.IsNullOrEmpty(text))
			{
				return this.RespondResult(false, "参数无效。");
			}
			List<User> userListByRoleId = this._settingsManager.GetUserListByRoleId(Guid.Parse(text));
			return this.RespondDataResult(userListByRoleId);
		}

		[HttpPost]
		public ActionResult Feedback()
		{
			Feedback feedback = base.RequestArgs<Feedback>();
			if (feedback == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			feedback.Domain = base.UserContext.Domain.Id;
			feedback.User = base.UserContext.User.Id;
			feedback.IP = base.HttpContext.Request.UserHostAddress;
			feedback.Time = DateTime.Now;
			this._settingsManager.Feedback(feedback);
			return this.RespondResult();
		}
	}
}
