using Linkup.Common;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web
{
	public class EnterpriseController : Controller
	{
		[CompilerGenerated]
		private static class <>o__4
		{
			public static CallSite<Func<CallSite, object, User, object>> <>p__0;

			public static CallSite<Func<CallSite, object, Domain, object>> <>p__1;

			public static CallSite<Func<CallSite, object, UserContext, object>> <>p__2;

			public static CallSite<Func<CallSite, object, Organization, object>> <>p__3;

			public static CallSite<Func<CallSite, object, List<string>, object>> <>p__4;
		}

		public UserContext UserContext
		{
			get;
			set;
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowedAnonymous), false).Length != 0)
			{
				return;
			}
			this.UserContext = SessionContainer.GetUserContext(filterContext.HttpContext);
			if (this.UserContext == null)
			{
				filterContext.Result = new RedirectResult("~/Home/Login");
				return;
			}
			if (EnterpriseController.<>o__4.<>p__0 == null)
			{
				EnterpriseController.<>o__4.<>p__0 = CallSite<Func<CallSite, object, Sheng.Enterprise.Infrastructure.User, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "User", typeof(EnterpriseController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			EnterpriseController.<>o__4.<>p__0.Target(EnterpriseController.<>o__4.<>p__0, base.ViewBag, this.UserContext.User);
			if (EnterpriseController.<>o__4.<>p__1 == null)
			{
				EnterpriseController.<>o__4.<>p__1 = CallSite<Func<CallSite, object, Domain, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Domain", typeof(EnterpriseController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			EnterpriseController.<>o__4.<>p__1.Target(EnterpriseController.<>o__4.<>p__1, base.ViewBag, this.UserContext.Domain);
			if (EnterpriseController.<>o__4.<>p__2 == null)
			{
				EnterpriseController.<>o__4.<>p__2 = CallSite<Func<CallSite, object, UserContext, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserContext", typeof(EnterpriseController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			EnterpriseController.<>o__4.<>p__2.Target(EnterpriseController.<>o__4.<>p__2, base.ViewBag, this.UserContext);
			if (EnterpriseController.<>o__4.<>p__3 == null)
			{
				EnterpriseController.<>o__4.<>p__3 = CallSite<Func<CallSite, object, Organization, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "RootOrganization", typeof(EnterpriseController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			EnterpriseController.<>o__4.<>p__3.Target(EnterpriseController.<>o__4.<>p__3, base.ViewBag, this.UserContext.RootOrganization);
			List<string> list = new List<string>();
			int num = 2015;
			int year = DateTime.Now.Year;
			do
			{
				list.Add(num.ToString());
				num++;
			}
			while (num <= year);
			if (EnterpriseController.<>o__4.<>p__4 == null)
			{
				EnterpriseController.<>o__4.<>p__4 = CallSite<Func<CallSite, object, List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "YearList", typeof(EnterpriseController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			EnterpriseController.<>o__4.<>p__4.Target(EnterpriseController.<>o__4.<>p__4, base.ViewBag, list);
		}

		protected TObj RequestArgs<TObj>() where TObj : class
		{
			return JsonConvert.DeserializeObject<TObj>(new StreamReader(base.HttpContext.Request.InputStream).ReadToEnd());
		}

		protected virtual ContentResult RespondResult()
		{
			return this.RespondResult(true, null);
		}

		protected virtual ContentResult RespondResult(bool success, string message)
		{
			return this.RespondResult(success, message, null);
		}

		protected virtual ContentResult RespondResult(bool success, string message, object data)
		{
			ApiResult apiResult = new ApiResult
			{
				Success = success,
				Message = message,
				Data = data
			};
			return this.RespondResult(apiResult);
		}

		protected virtual ContentResult RespondResult(ApiResult apiResult)
		{
			return new ContentResult
			{
				ContentEncoding = Encoding.UTF8,
				Content = JsonHelper.NewtonsoftSerializer(apiResult)
			};
		}

		protected virtual ContentResult RespondDataResult(object data)
		{
			return this.RespondResult(true, null, data);
		}
	}
}
