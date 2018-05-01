using Sheng.Enterprise.Core;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web.Areas.Api.Controllers
{
	public class UserController : EnterpriseController
	{
		private UserManager _userManager = UserManager.Instance;

		private static readonly DomainManager _domainManager = DomainManager.Instance;

		[AllowedAnonymous]
		public ActionResult Register()
		{
			UserRegisterArgs userRegisterArgs = base.RequestArgs<UserRegisterArgs>();
			if (userRegisterArgs == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			if (base.Session["ValidateCode"] == null || base.Session["ValidateCode"].ToString() != userRegisterArgs.ValidateCode)
			{
				return this.RespondResult(false, "验证码无效。");
			}
			UserRegisterResult userRegisterResult = this._userManager.Register(userRegisterArgs);
			if (userRegisterResult.Result == UserRegisterResultEnum.Success)
			{
				UserContext userContext = new UserContext(userRegisterResult.User, userRegisterResult.Domain);
				userContext.RootOrganization = UserController._domainManager.GetOrganization(userRegisterResult.Domain.Id);
				userContext.Authorization = new AuthorizationWrapper();
				userContext.Organization = UserController._domainManager.GetOrganization(userRegisterResult.User.OrganizationId);
				SessionContainer.SetUserContext(base.HttpContext, userContext);
				return this.RespondResult();
			}
			ApiResult apiResult = new ApiResult
			{
				Success = false
			};
			switch (userRegisterResult.Result)
			{
			case UserRegisterResultEnum.Unknow:
				apiResult.Message = "未知错误。";
				break;
			case UserRegisterResultEnum.AccountInUse:
				apiResult.Message = "帐户被占用，请尝试其它帐户名称。";
				break;
			case UserRegisterResultEnum.UserInfoInvalid:
				apiResult.Message = "帐户被占用，用户信息无效。";
				break;
			}
			return this.RespondResult(apiResult);
		}

		public ActionResult GetUserList()
		{
			GetUserListArgs getUserListArgs = base.RequestArgs<GetUserListArgs>();
			if (getUserListArgs == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			getUserListArgs.DomainId = base.UserContext.Domain.Id;
			GetItemListResult userList = this._userManager.GetUserList(getUserListArgs);
			return this.RespondDataResult(userList);
		}

		public ActionResult Update()
		{
			UserDto userDto = base.RequestArgs<UserDto>();
			if (userDto == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			User user = userDto.User;
			UserOperatorResult userOperatorResult = this._userManager.Update(user, userDto.WorkTypeList);
			if (userOperatorResult == UserOperatorResult.Success)
			{
				return this.RespondResult();
			}
			ApiResult apiResult = new ApiResult
			{
				Success = false
			};
			if (userOperatorResult == UserOperatorResult.AccountExistent)
			{
				apiResult.Message = "指定的帐号已被占用。";
			}
			return this.RespondResult(apiResult);
		}

		public ActionResult Create()
		{
			UserDto userDto = base.RequestArgs<UserDto>();
			if (userDto == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			User user = userDto.User;
			user.Id = Guid.NewGuid();
			UserOperatorResult userOperatorResult = this._userManager.Create(user, userDto.WorkTypeList);
			if (userOperatorResult == UserOperatorResult.Success)
			{
				return this.RespondResult();
			}
			ApiResult apiResult = new ApiResult
			{
				Success = false
			};
			if (userOperatorResult == UserOperatorResult.AccountExistent)
			{
				apiResult.Message = "指定的帐号已被占用。";
			}
			return this.RespondResult(apiResult);
		}

		public ActionResult Remove()
		{
			Guid guid = Guid.Parse(base.Request.QueryString["id"]);
			if (guid == base.UserContext.User.Id)
			{
				return this.RespondResult(false, "您不能删除自己。");
			}
			this._userManager.Remove(guid);
			return this.RespondResult();
		}

		public ActionResult ResetPasswordToDefault()
		{
			string input = base.Request.QueryString["id"];
			this._userManager.ResetPasswordToDefault(Guid.Parse(input));
			return this.RespondResult();
		}

		[AllowedAnonymous]
		public ActionResult ResetPassword()
		{
			ResetPasswordArgs resetPasswordArgs = base.RequestArgs<ResetPasswordArgs>();
			if (resetPasswordArgs == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			if (this._userManager.ResetPassword(resetPasswordArgs))
			{
				return this.RespondResult();
			}
			ApiResult apiResult = new ApiResult
			{
				Success = false
			};
			apiResult.Message = "请检查您输入的帐户及电子邮件地址是否正确。";
			return this.RespondResult(apiResult);
		}

		public ActionResult UpdatePassword()
		{
			UpdatePasswordArgs updatePasswordArgs = base.RequestArgs<UpdatePasswordArgs>();
			if (updatePasswordArgs == null)
			{
				return this.RespondResult(false, "参数无效。");
			}
			bool success = this._userManager.UpdatePassword(base.UserContext.User.Id, updatePasswordArgs);
			return this.RespondResult(success, string.Empty);
		}
	}
}
