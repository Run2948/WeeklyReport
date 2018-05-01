using Microsoft.CSharp.RuntimeBinder;
using Sheng.Enterprise.Core;
using Sheng.Enterprise.Infrastructure;
using Sheng.Enterprise.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace Sheng.Enterprise.Web.Controllers
{
	public class WeeklyReportController : EnterpriseController
	{
		[CompilerGenerated]
		private static class <>o__4
		{
			public static CallSite<Func<CallSite, object, int, object>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__5
		{
			public static CallSite<Func<CallSite, object, int, object>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__6
		{
			public static CallSite<Func<CallSite, object, int, object>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__7
		{
			public static CallSite<Func<CallSite, object, int, object>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__8
		{
			public static CallSite<Func<CallSite, object, int, object>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__9
		{
			public static CallSite<Func<CallSite, object, int, object>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__10
		{
			public static CallSite<Func<CallSite, object, int, object>> <>p__0;
		}

		private static readonly SettingsManager _settingsManager = SettingsManager.Instance;

		private static readonly WeeklyReportManager _weeklyReportManager = WeeklyReportManager.Instance;

		private static readonly DomainManager _domainManager = DomainManager.Instance;

		public ActionResult Index()
		{
			return base.View();
		}

		public ActionResult Post()
		{
			PostViewModel postViewModel = new PostViewModel();
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			postViewModel.CurrentWeekOfYear = weekOfYear;
			int year = DateTime.Now.Year;
			string text = base.Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text))
			{
				int.TryParse(text, out year);
			}
			int month = DateTime.Now.Month;
			string text2 = base.Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			postViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(base.Request.QueryString["week"], postViewModel.WeekList);
			postViewModel.Year = year;
			postViewModel.Month = month;
			postViewModel.WeekOfYear = weekOfYear2;
			postViewModel.WorkTypeList = WeeklyReportController._settingsManager.GetUserWorkTypeList(base.UserContext.User.Id);
			postViewModel.WorkTaskList = WeeklyReportController._settingsManager.GetWorkTaskList(base.UserContext.Domain.Id);
			postViewModel.WorkStatusList = WeeklyReportController._settingsManager.GetWorkStatusList(base.UserContext.Domain.Id);
			if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
			{
				postViewModel.ItemStartDate = DateTime.Now.AddDays(-7.0);
				postViewModel.ItemEndDate = DateTime.Now;
			}
			else
			{
				postViewModel.ItemStartDate = DateTimeHelper.CalculateFirstDateOfWeek(DateTime.Now);
				postViewModel.ItemEndDate = DateTime.Now;
			}
			if (year == DateTime.Now.Year && weekOfYear == weekOfYear2)
			{
				postViewModel.AllowPost = true;
			}
			else
			{
				if (year == DateTime.Now.Year && weekOfYear - weekOfYear2 == 1 && DateTime.Now.DayOfWeek == DayOfWeek.Monday)
				{
					postViewModel.AllowPost = true;
				}
				if (DateTime.Now.Year - year == 1 && month == 12 && DateTime.Now.Month == 1 && weekOfYear == 1 && weekOfYear2 == DateTimeHelper.GetYearWeekCount(year))
				{
					postViewModel.AllowPost = true;
				}
			}
			try
			{
				postViewModel.WeeklyReport = WeeklyReportController._weeklyReportManager.GetWeeklyReport(base.UserContext.User.Id, year, weekOfYear2);
			}
			catch
			{
				return base.RedirectToAction("ErrorView", "Home");
			}
			if (weekOfYear == weekOfYear2 && base.UserContext.User.Notify)
			{
				if (postViewModel.WeeklyReport != null && postViewModel.WeeklyReport.ItemList != null)
				{
					DateTime monday = DateTimeHelper.CalculateFirstDateOfWeek(DateTime.Now);
					Func<WeeklyReportItem, bool> <>9__0;
					while (monday.Day < DateTime.Now.Day)
					{
						IEnumerable<WeeklyReportItem> arg_308_0 = postViewModel.WeeklyReport.ItemList;
						Func<WeeklyReportItem, bool> arg_308_1;
						if ((arg_308_1 = <>9__0) == null)
						{
							arg_308_1 = (<>9__0 = ((WeeklyReportItem c) => c.Date.HasValue && c.Date.Value.Day == monday.Day));
						}
						if (arg_308_0.Where(arg_308_1).Count<WeeklyReportItem>() == 0)
						{
							postViewModel.Notify = true;
							if (base.Session["Notify"] == null)
							{
								postViewModel.AlertNotify = true;
								base.Session["Notify"] = true;
								break;
							}
							break;
						}
						else
						{
							monday = monday.AddDays(1.0);
						}
					}
				}
				else if (DateTime.Now.DayOfWeek != DayOfWeek.Monday)
				{
					postViewModel.Notify = true;
					if (base.Session["Notify"] == null)
					{
						postViewModel.AlertNotify = true;
						base.Session["Notify"] = true;
					}
				}
			}
			if (WeeklyReportController.<>o__4.<>p__0 == null)
			{
				WeeklyReportController.<>o__4.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentWeekOfYear", typeof(WeeklyReportController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			WeeklyReportController.<>o__4.<>p__0.Target(WeeklyReportController.<>o__4.<>p__0, base.ViewBag, DateTimeHelper.GetWeekOfYear(DateTime.Now));
			return base.View(postViewModel);
		}

		[AuthorizationFilter("WeeklyReport_SearchByOrganization")]
		public ActionResult SearchByOrganization()
		{
			SearchByOrganizationViewModel searchByOrganizationViewModel = new SearchByOrganizationViewModel();
			string text = base.Request.QueryString["organizationId"];
			if (string.IsNullOrEmpty(text))
			{
				searchByOrganizationViewModel.OrganizationId = base.UserContext.Organization.Id;
				searchByOrganizationViewModel.OrganizationName = base.UserContext.Organization.Name;
			}
			else
			{
				Organization organization = WeeklyReportController._domainManager.GetOrganization(Guid.Parse(text));
				if (organization == null)
				{
					searchByOrganizationViewModel.OrganizationId = base.UserContext.Organization.Id;
					searchByOrganizationViewModel.OrganizationName = base.UserContext.Organization.Name;
				}
				else
				{
					searchByOrganizationViewModel.OrganizationId = organization.Id;
					searchByOrganizationViewModel.OrganizationName = organization.Name;
				}
			}
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			searchByOrganizationViewModel.CurrentWeekOfYear = weekOfYear;
			int year = DateTime.Now.Year;
			string text2 = base.Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out year);
			}
			int month = DateTime.Now.Month;
			string text3 = base.Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text3))
			{
				int.TryParse(text3, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			searchByOrganizationViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(base.Request.QueryString["week"], searchByOrganizationViewModel.WeekList);
			searchByOrganizationViewModel.Year = year;
			searchByOrganizationViewModel.Month = month;
			searchByOrganizationViewModel.WeekOfYear = weekOfYear2;
			searchByOrganizationViewModel.WeeklyReportList = WeeklyReportController._weeklyReportManager.GetWeeklyReportListByOrganization(base.UserContext.Domain.Id, searchByOrganizationViewModel.OrganizationId, year, weekOfYear2);
			if (WeeklyReportController.<>o__5.<>p__0 == null)
			{
				WeeklyReportController.<>o__5.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentWeekOfYear", typeof(WeeklyReportController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			WeeklyReportController.<>o__5.<>p__0.Target(WeeklyReportController.<>o__5.<>p__0, base.ViewBag, DateTimeHelper.GetWeekOfYear(DateTime.Now));
			return base.View(searchByOrganizationViewModel);
		}

		public ActionResult SearchByPersonal()
		{
			SearchByPersonalViewModel searchByPersonalViewModel = new SearchByPersonalViewModel();
			string text = base.Request.QueryString["userId"];
			if (string.IsNullOrEmpty(text))
			{
				searchByPersonalViewModel.UserId = base.UserContext.User.Id;
				searchByPersonalViewModel.UserName = base.UserContext.User.Name;
			}
			else
			{
				searchByPersonalViewModel.UserId = Guid.Parse(text);
				searchByPersonalViewModel.UserName = base.Request.QueryString["userName"];
			}
			int year = DateTime.Now.Year;
			string text2 = base.Request.QueryString["startYear"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out year);
			}
			int month = DateTime.Now.Month;
			string text3 = base.Request.QueryString["startMonth"];
			if (!string.IsNullOrEmpty(text3))
			{
				int.TryParse(text3, out month);
			}
			int year2 = DateTime.Now.Year;
			string text4 = base.Request.QueryString["endYear"];
			if (!string.IsNullOrEmpty(text4))
			{
				int.TryParse(text4, out year2);
			}
			int month2 = DateTime.Now.Month;
			string text5 = base.Request.QueryString["endMonth"];
			if (!string.IsNullOrEmpty(text5))
			{
				int.TryParse(text5, out month2);
			}
			searchByPersonalViewModel.StartYear = year;
			searchByPersonalViewModel.StartMonth = month;
			searchByPersonalViewModel.EndYear = year2;
			searchByPersonalViewModel.EndMonth = month2;
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			searchByPersonalViewModel.CurrentWeekOfYear = weekOfYear;
			searchByPersonalViewModel.WeeklyReportList = WeeklyReportController._weeklyReportManager.GetWeeklyReportListByPerson(searchByPersonalViewModel.UserId, year, month, year2, month2);
			if (WeeklyReportController.<>o__6.<>p__0 == null)
			{
				WeeklyReportController.<>o__6.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentWeekOfYear", typeof(WeeklyReportController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			WeeklyReportController.<>o__6.<>p__0.Target(WeeklyReportController.<>o__6.<>p__0, base.ViewBag, DateTimeHelper.GetWeekOfYear(DateTime.Now));
			return base.View(searchByPersonalViewModel);
		}

		[AuthorizationFilter("WeeklyReport_SearchByWorkType")]
		public ActionResult SearchByWorkType()
		{
			SearchByWorkTypeViewModel searchByWorkTypeViewModel = new SearchByWorkTypeViewModel();
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			searchByWorkTypeViewModel.CurrentWeekOfYear = weekOfYear;
			int year = DateTime.Now.Year;
			string text = base.Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text))
			{
				int.TryParse(text, out year);
			}
			int month = DateTime.Now.Month;
			string text2 = base.Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			searchByWorkTypeViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(base.Request.QueryString["week"], searchByWorkTypeViewModel.WeekList);
			searchByWorkTypeViewModel.Year = year;
			searchByWorkTypeViewModel.Month = month;
			searchByWorkTypeViewModel.WeekOfYear = weekOfYear2;
			searchByWorkTypeViewModel.WorkTypeList = WeeklyReportController._settingsManager.GetUserWorkTypeList(base.UserContext.User.Id);
			searchByWorkTypeViewModel.WorkTaskList = WeeklyReportController._settingsManager.GetWorkTaskList(base.UserContext.Domain.Id);
			string text3 = base.Request.QueryString["workType"];
			if (text3 == "null")
			{
				text3 = null;
			}
			Guid? workType = null;
			if (!string.IsNullOrEmpty(text3))
			{
				workType = new Guid?(Guid.Parse(text3));
			}
			string text4 = base.Request.QueryString["workTask"];
			Guid? workTask = null;
			if (!string.IsNullOrEmpty(text4))
			{
				workTask = new Guid?(Guid.Parse(text4));
			}
			if (workType.HasValue)
			{
				searchByWorkTypeViewModel.WeeklyReportList = WeeklyReportController._weeklyReportManager.GetWeeklyReportListByWorkType(base.UserContext.Domain.Id, workType, workTask, year, weekOfYear2);
			}
			else
			{
				searchByWorkTypeViewModel.WeeklyReportList = new List<WeeklyReport>();
			}
			if (WeeklyReportController.<>o__7.<>p__0 == null)
			{
				WeeklyReportController.<>o__7.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentWeekOfYear", typeof(WeeklyReportController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			WeeklyReportController.<>o__7.<>p__0.Target(WeeklyReportController.<>o__7.<>p__0, base.ViewBag, DateTimeHelper.GetWeekOfYear(DateTime.Now));
			return base.View(searchByWorkTypeViewModel);
		}

		[AuthorizationFilter("WeeklyReport_Check")]
		public ActionResult Check()
		{
			CheckViewModel checkViewModel = new CheckViewModel();
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			checkViewModel.CurrentWeekOfYear = weekOfYear;
			int year = DateTime.Now.Year;
			string text = base.Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text))
			{
				int.TryParse(text, out year);
			}
			int month = DateTime.Now.Month;
			string text2 = base.Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			checkViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(base.Request.QueryString["week"], checkViewModel.WeekList);
			checkViewModel.Year = year;
			checkViewModel.Month = month;
			checkViewModel.WeekOfYear = weekOfYear2;
			string value = base.Request.QueryString["checkViewType"];
			CheckViewType checkViewType;
			if (!string.IsNullOrEmpty(value) && Enum.TryParse<CheckViewType>(value, out checkViewType))
			{
				checkViewModel.CheckViewType = checkViewType;
			}
			checkViewModel.WeeklyReportList = WeeklyReportController._weeklyReportManager.GetWeeklyReportListForCheck(base.UserContext.Domain.Id, base.UserContext.User.Id, year, weekOfYear2, checkViewModel.CheckViewType);
			if (WeeklyReportController.<>o__8.<>p__0 == null)
			{
				WeeklyReportController.<>o__8.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentWeekOfYear", typeof(WeeklyReportController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			WeeklyReportController.<>o__8.<>p__0.Target(WeeklyReportController.<>o__8.<>p__0, base.ViewBag, DateTimeHelper.GetWeekOfYear(DateTime.Now));
			return base.View(checkViewModel);
		}

		[AuthorizationFilter("WeeklyReport_ReportByOrganization")]
		public ActionResult ReportByOrganization()
		{
			ReportByOrganizationViewModel reportByOrganizationViewModel = new ReportByOrganizationViewModel();
			string text = base.Request.QueryString["organizationId"];
			if (string.IsNullOrEmpty(text))
			{
				reportByOrganizationViewModel.OrganizationId = base.UserContext.RootOrganization.Id;
				reportByOrganizationViewModel.OrganizationName = base.UserContext.RootOrganization.Name;
			}
			else
			{
				Organization organization = WeeklyReportController._domainManager.GetOrganization(Guid.Parse(text));
				if (organization == null)
				{
					reportByOrganizationViewModel.OrganizationId = base.UserContext.RootOrganization.Id;
					reportByOrganizationViewModel.OrganizationName = base.UserContext.RootOrganization.Name;
				}
				else
				{
					reportByOrganizationViewModel.OrganizationId = organization.Id;
					reportByOrganizationViewModel.OrganizationName = organization.Name;
				}
			}
			int year = DateTime.Now.Year;
			string text2 = base.Request.QueryString["startYear"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out year);
			}
			int month = DateTime.Now.Month;
			string text3 = base.Request.QueryString["startMonth"];
			if (!string.IsNullOrEmpty(text3))
			{
				int.TryParse(text3, out month);
			}
			int year2 = DateTime.Now.Year;
			string text4 = base.Request.QueryString["endYear"];
			if (!string.IsNullOrEmpty(text4))
			{
				int.TryParse(text4, out year2);
			}
			int month2 = DateTime.Now.Month;
			string text5 = base.Request.QueryString["endMonth"];
			if (!string.IsNullOrEmpty(text5))
			{
				int.TryParse(text5, out month2);
			}
			reportByOrganizationViewModel.StartYear = year;
			reportByOrganizationViewModel.StartMonth = month;
			reportByOrganizationViewModel.EndYear = year2;
			reportByOrganizationViewModel.EndMonth = month2;
			reportByOrganizationViewModel.Data = WeeklyReportController._weeklyReportManager.ReportByOrganization(base.UserContext.Domain.Id, reportByOrganizationViewModel.OrganizationId, year, month, year2, month2);
			if (WeeklyReportController.<>o__9.<>p__0 == null)
			{
				WeeklyReportController.<>o__9.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentWeekOfYear", typeof(WeeklyReportController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			WeeklyReportController.<>o__9.<>p__0.Target(WeeklyReportController.<>o__9.<>p__0, base.ViewBag, DateTimeHelper.GetWeekOfYear(DateTime.Now));
			return base.View(reportByOrganizationViewModel);
		}

		[AuthorizationFilter("WeeklyReport_ReportBySubmit")]
		public ActionResult ReportBySubmit()
		{
			ReportBySubmitViewModel reportBySubmitViewModel = new ReportBySubmitViewModel();
			string text = base.Request.QueryString["organizationId"];
			if (string.IsNullOrEmpty(text))
			{
				reportBySubmitViewModel.OrganizationId = base.UserContext.RootOrganization.Id;
				reportBySubmitViewModel.OrganizationName = base.UserContext.RootOrganization.Name;
			}
			else
			{
				Organization organization = WeeklyReportController._domainManager.GetOrganization(Guid.Parse(text));
				if (organization == null)
				{
					reportBySubmitViewModel.OrganizationId = base.UserContext.RootOrganization.Id;
					reportBySubmitViewModel.OrganizationName = base.UserContext.RootOrganization.Name;
				}
				else
				{
					reportBySubmitViewModel.OrganizationId = organization.Id;
					reportBySubmitViewModel.OrganizationName = organization.Name;
				}
			}
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			reportBySubmitViewModel.CurrentWeekOfYear = weekOfYear;
			int year = DateTime.Now.Year;
			string text2 = base.Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out year);
			}
			int month = DateTime.Now.Month;
			string text3 = base.Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text3))
			{
				int.TryParse(text3, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			reportBySubmitViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(base.Request.QueryString["week"], reportBySubmitViewModel.WeekList);
			reportBySubmitViewModel.Year = year;
			reportBySubmitViewModel.Month = month;
			reportBySubmitViewModel.WeekOfYear = weekOfYear2;
			reportBySubmitViewModel.Data = WeeklyReportController._weeklyReportManager.ReportBySumbit(base.UserContext.Domain.Id, reportBySubmitViewModel.OrganizationId, year, weekOfYear2);
			if (WeeklyReportController.<>o__10.<>p__0 == null)
			{
				WeeklyReportController.<>o__10.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentWeekOfYear", typeof(WeeklyReportController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			WeeklyReportController.<>o__10.<>p__0.Target(WeeklyReportController.<>o__10.<>p__0, base.ViewBag, DateTimeHelper.GetWeekOfYear(DateTime.Now));
			return base.View(reportBySubmitViewModel);
		}
	}
}
