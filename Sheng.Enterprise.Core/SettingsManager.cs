using Linkup.Data;
using Linkup.DataRelationalMapping;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sheng.Enterprise.Core
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();

		private DatabaseWrapper _dataBase = ServiceUnity.Instance.Database;

		public static SettingsManager Instance
		{
			get
			{
				return SettingsManager._instance;
			}
		}

		private SettingsManager()
		{
		}

		public List<JobTitle> GetJobTitleList(Guid domainId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Domain", domainId);
			return this._dataBase.Select<JobTitle>(dictionary);
		}

		public void CreateJobTitle(JobTitle jobTitle)
		{
			if (jobTitle == null)
			{
				return;
			}
			this._dataBase.Insert(jobTitle);
		}

		public void UpdateJobTitle(JobTitle jobTitle)
		{
			if (jobTitle == null)
			{
				return;
			}
			this._dataBase.Update(jobTitle);
		}

		public void RemoveJobTitle(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery("DELETE FROM [JobTitle] WHERE [Id] = @id", list);
		}

		public List<JobLevel> GetJobLevelList(Guid domainId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Domain", domainId);
			return this._dataBase.Select<JobLevel>(dictionary);
		}

		public void CreateJobLevel(JobLevel jobLevel)
		{
			if (jobLevel == null)
			{
				return;
			}
			this._dataBase.Insert(jobLevel);
		}

		public void UpdateJobLevel(JobLevel jobLevel)
		{
			if (jobLevel == null)
			{
				return;
			}
			this._dataBase.Update(jobLevel);
		}

		public void RemoveJobLevel(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery("DELETE FROM [JobLevel] WHERE [Id] = @id", list);
		}

		public List<OfficeLocation> GetOfficeLocationList(Guid domainId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Domain", domainId);
			return this._dataBase.Select<OfficeLocation>(dictionary);
		}

		public void CreateOfficeLocation(OfficeLocation officeLocation)
		{
			if (officeLocation == null)
			{
				return;
			}
			this._dataBase.Insert(officeLocation);
		}

		public void UpdateOfficeLocation(OfficeLocation officeLocation)
		{
			if (officeLocation == null)
			{
				return;
			}
			this._dataBase.Update(officeLocation);
		}

		public void RemoveOfficeLocation(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery("DELETE FROM [OfficeLocation] WHERE [Id] = @id", list);
		}

		public WorkType GetWorkType(Guid id)
		{
			WorkType workType = new WorkType();
			workType.Id = id;
			if (this._dataBase.Fill<WorkType>(workType))
			{
				return workType;
			}
			return null;
		}

		public List<WorkType> GetWorkTypeList(Guid domainId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Domain", domainId);
			return this._dataBase.Select<WorkType>(dictionary);
		}

		public List<WorkType> GetUserWorkTypeList(Guid userId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@user", userId));
			return this._dataBase.Select<WorkType>("SELECT [WorkType].* FROM [UserWorkType] LEFT JOIN [WorkType] ON [UserWorkType].[WorkType] = [WorkType].[Id] WHERE [UserWorkType].[User] = @user", list);
		}

		public void CreateWorkType(WorkType workType)
		{
			if (workType == null)
			{
				return;
			}
			this._dataBase.Insert(workType);
		}

		public void UpdateWorkType(WorkType workType)
		{
			if (workType == null)
			{
				return;
			}
			this._dataBase.Update(workType);
		}

		public void RemoveWorkType(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery("DELETE FROM [WorkType] WHERE [Id] = @id", list);
			this._dataBase.ExecuteNonQuery("DELETE FROM [WorkTask] WHERE [WorkType] = @id", list);
		}

		public WorkTask GetWorkTask(Guid id)
		{
			WorkTask workTask = new WorkTask();
			workTask.Id = id;
			if (this._dataBase.Fill<WorkTask>(workTask))
			{
				return workTask;
			}
			return null;
		}

		public List<WorkTask> GetWorkTaskList(Guid domainId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Domain", domainId);
			return this._dataBase.Select<WorkTask>(dictionary);
		}

		public void CreateWorkTask(WorkTask workTask)
		{
			if (workTask == null)
			{
				return;
			}
			this._dataBase.Insert(workTask);
		}

		public void UpdateWorkTask(WorkTask workTask)
		{
			if (workTask == null)
			{
				return;
			}
			this._dataBase.Update(workTask);
		}

		public void RemoveWorkTask(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery("DELETE FROM [WorkTask] WHERE [Id] = @id", list);
		}

		public List<WorkStatus> GetWorkStatusList(Guid domainId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Domain", domainId);
			return this._dataBase.Select<WorkStatus>(dictionary);
		}

		public void CreateWorkStatus(WorkStatus workStatus)
		{
			if (workStatus == null)
			{
				return;
			}
			this._dataBase.Insert(workStatus);
		}

		public void UpdateWorkStatus(WorkStatus workStatus)
		{
			if (workStatus == null)
			{
				return;
			}
			this._dataBase.Update(workStatus);
		}

		public void RemoveWorkStatus(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery("DELETE FROM [WorkStatus] WHERE [Id] = @id", list);
		}

		public void CreateCheckRelation(CheckRelationJsonContract checkRelationJsonContract)
		{
			if (checkRelationJsonContract == null)
			{
				return;
			}
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@checker", checkRelationJsonContract.Checker));
			this._dataBase.ExecuteNonQuery("DELETE FROM [CheckRelation] WHERE [Checker] = @checker", list);
			List<SqlExpression> list2 = new List<SqlExpression>();
			foreach (Guid current in checkRelationJsonContract.StaffList)
			{
				list2.Add(RelationalMappingUnity.GetSqlExpression(new CheckRelation
				{
					Domain = checkRelationJsonContract.Domain,
					Checker = checkRelationJsonContract.Checker,
					Staff = current
				}, SqlExpressionType.Insert));
			}
			this._dataBase.ExcuteSqlExpression(list2);
		}

		public void RemoveCheckRelation(Guid checkerId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@checker", checkerId));
			this._dataBase.ExecuteNonQuery("DELETE FROM [CheckRelation] WHERE [Checker] = @checker", list);
		}

		public List<CheckerWrapper> GetCheckerWrapperList(Guid domainId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@domainId", domainId));
			DataSet arg_48_0 = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetCheckRelationList", list, new string[]
			{
				"result"
			});
			Dictionary<string, CheckerWrapper> dictionary = new Dictionary<string, CheckerWrapper>();
			List<CheckerWrapper> list2 = new List<CheckerWrapper>();
			foreach (DataRow dataRow in arg_48_0.Tables[0].Rows)
			{
				string text = dataRow["Checker"].ToString();
				CheckerWrapper checkerWrapper;
				if (!dictionary.ContainsKey(text))
				{
					checkerWrapper = new CheckerWrapper();
					checkerWrapper.Id = Guid.Parse(text);
					checkerWrapper.Name = dataRow["CheckerName"].ToString();
					list2.Add(checkerWrapper);
					dictionary.Add(text, checkerWrapper);
				}
				else
				{
					checkerWrapper = dictionary[text];
				}
				CheckStaffWrapper checkStaffWrapper = new CheckStaffWrapper();
				checkStaffWrapper.Id = Guid.Parse(dataRow["Staff"].ToString());
				checkStaffWrapper.Name = dataRow["StaffName"].ToString();
				checkerWrapper.StaffList.Add(checkStaffWrapper);
			}
			return list2;
		}

		public List<CheckStaffWrapper> GetCheckStaffList(Guid checkerId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@checkerId", checkerId));
			return RelationalMappingUnity.Select<CheckStaffWrapper>(this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetCheckStaffList", list, new string[]
			{
				"result"
			}).Tables[0]);
		}

		public List<Role> GetRoleList(Guid domainId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Domain", domainId);
			return this._dataBase.Select<Role>(dictionary);
		}

		public void CreateRole(Role role)
		{
			if (role == null)
			{
				return;
			}
			this._dataBase.Insert(role);
		}

		public void UpdateRole(Role role)
		{
			if (role == null)
			{
				return;
			}
			this._dataBase.Update(role);
		}

		public void RemoveRole(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery("DELETE FROM [Role] WHERE [Id] = @id", list);
			this._dataBase.ExecuteNonQuery("DELETE FROM [RoleAuthorization] WHERE [Role] = @id", list);
			this._dataBase.ExecuteNonQuery("DELETE FROM [RoleUser] WHERE [Role] = @id", list);
		}

		public Role GetRole(Guid id)
		{
			Role role = new Role();
			role.Id = id;
			if (this._dataBase.Fill<Role>(role))
			{
				return role;
			}
			return null;
		}

		public List<Authorization> GetAuthorizationListByRoleId(Guid roleId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Role", roleId);
			return this._dataBase.Select<Authorization>(dictionary);
		}

		public void UpdateAuthorizationListByRoleId(RoleAuthorizationRelation roleAuthorizationRelation)
		{
			if (roleAuthorizationRelation == null)
			{
				return;
			}
			List<SqlExpression> list = new List<SqlExpression>();
			List<CommandParameter> list2 = new List<CommandParameter>();
			list2.Add(new CommandParameter("@role", roleAuthorizationRelation.Role));
			SqlExpression sqlExpression = new SqlExpression
			{
				Sql = "DELETE FROM [RoleAuthorization] WHERE [Role] = @role"
			};
			sqlExpression.ParameterList = this._dataBase.CommandParameterToSqlParameter(list2);
			list.Add(sqlExpression);
			foreach (Authorization current in roleAuthorizationRelation.AuthorizationList)
			{
				list.Add(RelationalMappingUnity.GetSqlExpression(new RoleAuthorization
				{
					Domain = roleAuthorizationRelation.Domain,
					Role = roleAuthorizationRelation.Role,
					AuthorizationKey = current.Key
				}, SqlExpressionType.Insert));
			}
			this._dataBase.ExcuteSqlExpression(list);
		}

		public void AddUserToRole(RoleUser roleUser)
		{
			if (roleUser == null)
			{
				return;
			}
			this._dataBase.Insert(roleUser);
		}

		public void AddUserToRole(List<RoleUser> roleUserList)
		{
			if (roleUserList == null || roleUserList.Count == 0)
			{
				return;
			}
			using (List<RoleUser>.Enumerator enumerator = this.GetUserRoleListByRoleId(roleUserList[0].Role).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RoleUser currentRoleUser = enumerator.Current;
					List<RoleUser> list = (from c in roleUserList
					where c.User == currentRoleUser.User
					select c).ToList<RoleUser>();
					if (list.Count != 0)
					{
						foreach (RoleUser current in list)
						{
							roleUserList.Remove(current);
						}
					}
				}
			}
			this._dataBase.InsertList(roleUserList.Cast<object>().ToList<object>());
		}

		public void RemoveUserFromRole(RoleUser roleUser)
		{
			if (roleUser == null)
			{
				return;
			}
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@role", roleUser.Role));
			list.Add(new CommandParameter("@user", roleUser.User));
			this._dataBase.ExecuteNonQuery("DELETE FROM [RoleUser] WHERE [Role] = @role AND [User] = @user", list);
		}

		public List<User> GetUserListByRoleId(Guid roleId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@role", roleId));
			return RelationalMappingUnity.Select<User>(this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetUserListByRoleId", list, new string[]
			{
				"result"
			}).Tables[0]);
		}

		public List<RoleUser> GetUserRoleListByRoleId(Guid roleId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@role", roleId));
			return this._dataBase.Select<RoleUser>("SELECT * FROM [RoleUser] WHERE [Role] = @role", list);
		}

		public void Feedback(Feedback feedback)
		{
			if (feedback == null)
			{
				return;
			}
			this._dataBase.Insert(feedback);
		}
	}
}
