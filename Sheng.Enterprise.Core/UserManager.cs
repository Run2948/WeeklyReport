using Linkup.Data;
using Linkup.DataRelationalMapping;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;

namespace Sheng.Enterprise.Core
{
	public class UserManager
	{
		private static readonly UserManager _instance = new UserManager();

		private DatabaseWrapper _dataBase = ServiceUnity.Instance.Database;

		private object _lockObj = new object();

		private static readonly DomainManager _domainManager = DomainManager.Instance;

		public static UserManager Instance
		{
			get
			{
				return UserManager._instance;
			}
		}

		private UserManager()
		{
		}

		public User Verify(string account, string password)
		{
			if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
			{
				return null;
			}

		    List<CommandParameter> list = new List<CommandParameter>
		    {
		        new CommandParameter("@account", account),
		        new CommandParameter("@password", password)
		    };
		    List<User> list2 = this._dataBase.Select<User>("SELECT * FROM [User] WHERE [Account] = @account AND [Password] = @password AND [Removed] = 0", list);
			return list2.Count != 1 ? null : list2[0];
		}

		public User GetUser(Guid id)
		{
		    User user = new User {Id = id};
		    return this._dataBase.Fill<User>(user) ? user : null;
		}

		public UserDataWrapper GetUserDataWrapper(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetUser", list, new string[]
			{
				"result"
			});
			if (dataSet.Tables[0].Rows.Count == 0)
			{
				return null;
			}
			return RelationalMappingUnity.Select<UserDataWrapper>(dataSet.Tables[0].Rows[0]);
		}

		public List<UserWorkType> GetUserWorkTypeList(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@user", id));
			return this._dataBase.Select<UserWorkType>("SELECT * FROM [UserWorkType] WHERE [User] = @user", list);
		}

		public UserRegisterResult Register(UserRegisterArgs args)
		{
			UserRegisterResult userRegisterResult = new UserRegisterResult();
			if (args == null)
			{
				userRegisterResult.Result = UserRegisterResultEnum.UserInfoInvalid;
				return userRegisterResult;
			}
			object lockObj = this._lockObj;
			lock (lockObj)
			{
				List<CommandParameter> list = new List<CommandParameter>();
				list.Add(new CommandParameter("@account", args.Account));
				if (int.Parse(this._dataBase.ExecuteScalar("SELECT Count(Id) FROM [User] WHERE [Account] = @account", list).ToString()) > 0)
				{
					userRegisterResult.Result = UserRegisterResultEnum.AccountInUse;
					return userRegisterResult;
				}
				Domain domain = new Domain();
				UserManager._domainManager.Create(domain);
			    Organization organization = new Organization
			    {
			        Id = domain.Id,
			        Domain = domain.Id,
			        Name = args.DomainName
			    };
			    UserManager._domainManager.CreateOrganization(organization);
				User user = new User
				{
					Account = args.Account,
					Password = args.Password,
					Name = args.Name,
					Telphone = args.Telphone,
					Email = args.Email,
					DomainId = domain.Id,
					OrganizationId = organization.Id,
					Notify = true
				};
				this._dataBase.Insert(user);
			    Dictionary<string, object> dictionary = new Dictionary<string, object> {{"Domain", domain.Id}};
			    WorkType workType = this._dataBase.Select<WorkType>(dictionary)[0];
			    UserWorkType userWorkType = new UserWorkType
			    {
			        Domain = domain.Id,
			        User = user.Id,
			        WorkType = workType.Id
			    };
			    this._dataBase.Insert(userWorkType);
				userRegisterResult.User = user;
				userRegisterResult.Domain = domain;
			}
			userRegisterResult.Result = UserRegisterResultEnum.Success;
			return userRegisterResult;
		}

		public UserOperatorResult Update(User user, List<Guid> workTypeList)
		{
			if (user == null)
			{
				return UserOperatorResult.Failed;
			}
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", user.Id));
			list.Add(new CommandParameter("@account", user.Account));
			if (int.Parse(this._dataBase.ExecuteScalar("SELECT COUNT(Id) FROM [User] WHERE [Id] <> @id AND [Account] = @account AND [Removed] = 0", list).ToString()) > 0)
			{
				return UserOperatorResult.AccountExistent;
			}
			SqlExpression sqlExpression = RelationalMappingUnity.GetSqlExpression(user, new SqlExpressionArgs
			{
				Type = SqlExpressionType.Update,
				ExcludeFields = "Password"
			});
			this._dataBase.ExcuteSqlExpression(sqlExpression);
			list = new List<CommandParameter>();
			list.Add(new CommandParameter("@user", user.Id));
			this._dataBase.ExecuteNonQuery("DELETE [UserWorkType] WHERE [User] = @user", list);
			if (workTypeList != null && workTypeList.Count > 0)
			{
				foreach (Guid current in workTypeList)
				{
					UserWorkType userWorkType = new UserWorkType();
					userWorkType.Domain = user.DomainId;
					userWorkType.User = user.Id;
					userWorkType.WorkType = current;
					this._dataBase.Insert(userWorkType);
				}
			}
			return UserOperatorResult.Success;
		}

		public UserOperatorResult Create(User user, List<Guid> workTypeList)
		{
			if (user == null)
			{
				return UserOperatorResult.Failed;
			}
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@account", user.Account));
			if (int.Parse(this._dataBase.ExecuteScalar("SELECT COUNT(Id) FROM [User] WHERE [Account] = @account AND [Removed] = 0", list).ToString()) > 0)
			{
				return UserOperatorResult.AccountExistent;
			}
			user.Password = IOHelper.GetMD5HashFromString("123");
			this._dataBase.Insert(user);
			list = new List<CommandParameter>();
			list.Add(new CommandParameter("@user", user.Id));
			this._dataBase.ExecuteNonQuery("DELETE [UserWorkType] WHERE [User] = @user", list);
			if (workTypeList != null && workTypeList.Count > 0)
			{
				foreach (Guid current in workTypeList)
				{
					UserWorkType userWorkType = new UserWorkType();
					userWorkType.Domain = user.DomainId;
					userWorkType.User = user.Id;
					userWorkType.WorkType = current;
					this._dataBase.Insert(userWorkType);
				}
			}
			return UserOperatorResult.Success;
		}

		public List<UserDataWrapper> GetUserDataWrapperList(Guid organizationId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@organizationId", organizationId));
			return RelationalMappingUnity.Select<UserDataWrapper>(this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetUserWrapperList", list, new string[]
			{
				"result"
			}).Tables[0]);
		}

		public GetItemListResult GetUserList(GetUserListArgs args)
		{
			if (args.Name == null)
			{
				args.Name = string.Empty;
			}
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@domain", args.DomainId));
			list.Add(new CommandParameter("@page", args.Page));
			list.Add(new CommandParameter("@pageSize", args.PageSize));
			list.Add(new CommandParameter("@name", "%" + args.Name + "%"));
			list.Add(new CommandParameter("@organizationId", args.OrganizationId));
			list.Add(new CommandParameter("@searchOrganization", args.SearchOrganization));
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetUserList", list, new string[]
			{
				"result"
			});
			if (dataSet.Tables[0].Rows.Count == 0 && args.Page > 1)
			{
				int num = args.Page;
				args.Page = num - 1;
				return this.GetUserList(args);
			}
			GetItemListResult getItemListResult = new GetItemListResult();
			getItemListResult.ItemList = dataSet.Tables[0];
			int num2 = int.Parse(dataSet.Tables[1].Rows[0][0].ToString());
			getItemListResult.TotalPage = num2 / args.PageSize;
			if (num2 % args.PageSize > 0)
			{
				GetItemListResult expr_17B = getItemListResult;
				int num = expr_17B.TotalPage;
				expr_17B.TotalPage = num + 1;
			}
			getItemListResult.Page = args.Page;
			return getItemListResult;
		}

		public void Remove(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery("UPDATE [User] Set [Removed] = 1 WHERE [Id] = @id", list);
		}

		public void ResetPasswordToDefault(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			list.Add(new CommandParameter("@password", IOHelper.GetMD5HashFromString("123")));
			this._dataBase.ExecuteNonQuery("UPDATE [User] Set [Password] = @password WHERE [Id] = @id", list);
		}

		public bool ResetPassword(ResetPasswordArgs args)
		{
			int num = new Random(DateTime.Now.Second).Next(10000, 100000);
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@account", args.Account));
			list.Add(new CommandParameter("@email", args.Email));
			list.Add(new CommandParameter("@password", IOHelper.GetMD5HashFromString(num.ToString())));
			int num2 = this._dataBase.ExecuteNonQuery("UPDATE [User] Set [Password] = @password WHERE [Account] = @account AND [Email] = @email", list);
			if (num2 == 1)
			{
				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress("linkup_noreply@163.com");
				mailMessage.To.Add(new MailAddress(args.Email));
				mailMessage.Subject = string.Format("您的密码已重置", new object[0]);
				string body = string.Format("<b>您的密码已重置</b><br/>\n                            帐户：{0}<br/>\n                            密码：{1}<br/>\n                            登录地址：http://e.zkebao.com<br/>\n                            ", args.Account, num);
				mailMessage.Body = body;
				mailMessage.IsBodyHtml = true;
				SmtpService.Instance.Send(mailMessage);
			}
			return num2 == 1;
		}

		public bool UpdatePassword(Guid id, UpdatePasswordArgs args)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			list.Add(new CommandParameter("@password", args.Password));
			list.Add(new CommandParameter("@newPassword", args.NewPassword));
			return this._dataBase.ExecuteNonQuery("UPDATE [User] SET [Password] = @newPassword WHERE [Id] = @id AND [Password] = @password", list) == 1;
		}

		public List<Authorization> GetAuthorizationListByUser(Guid userId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@userId", userId));
			List<Authorization> list2 = RelationalMappingUnity.Select<Authorization>(this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetAuthorizationListByUser", list, new string[]
			{
				"result"
			}).Tables[0]);
			if (list2 == null)
			{
				return null;
			}
			return list2;
		}

		public List<Role> GetRoleListByUser(Guid userId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@userId", userId));
			List<Role> list2 = RelationalMappingUnity.Select<Role>(this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetRoleListByUser", list, new string[]
			{
				"result"
			}).Tables[0]);
			if (list2 == null)
			{
				return null;
			}
			return list2;
		}
	}
}
