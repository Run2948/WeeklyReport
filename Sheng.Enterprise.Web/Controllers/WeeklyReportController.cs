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
		private static readonly SettingsManager _settingsManager = SettingsManager.Instance;

		private static readonly WeeklyReportManager _weeklyReportManager = WeeklyReportManager.Instance;

		private static readonly DomainManager _domainManager = DomainManager.Instance;


		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Post()
		{
			PostViewModel postViewModel = new PostViewModel();
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			postViewModel.CurrentWeekOfYear = weekOfYear;
			int year = DateTime.Now.Year;
			string text = Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text))
			{
				int.TryParse(text, out year);
			}
			int month = DateTime.Now.Month;
			string text2 = Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			postViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(Request.QueryString["week"], postViewModel.WeekList);
			postViewModel.Year = year;
			postViewModel.Month = month;
			postViewModel.WeekOfYear = weekOfYear2;
			postViewModel.WorkTypeList = _settingsManager.GetUserWorkTypeList(UserContext.User.Id);
			postViewModel.WorkTaskList = _settingsManager.GetWorkTaskList(UserContext.Domain.Id);
			postViewModel.WorkStatusList = _settingsManager.GetWorkStatusList(UserContext.Domain.Id);
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
				postViewModel.WeeklyReport = _weeklyReportManager.GetWeeklyReport(UserContext.User.Id, year, weekOfYear2);
			}
			catch
			{
				return RedirectToAction("ErrorView", "Home");
			}
			if (weekOfYear == weekOfYear2 && UserContext.User.Notify)
			{
				if (postViewModel.WeeklyReport?.ItemList != null)
				{
					DateTime monday = DateTimeHelper.CalculateFirstDateOfWeek(DateTime.Now);
					while (monday.Day < DateTime.Now.Day)
					{
						IEnumerable<WeeklyReportItem> reportList = postViewModel.WeeklyReport.ItemList;
						Func<WeeklyReportItem, bool> where = c=> c.Date.HasValue && c.Date.Value.Day == monday.Day;
						if (!reportList.Where(where).Any())
						{
							postViewModel.Notify = true;
							if (Session["Notify"] == null)
							{
								postViewModel.AlertNotify = true;
								Session["Notify"] = true;
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
					if (Session["Notify"] == null)
					{
						postViewModel.AlertNotify = true;
						Session["Notify"] = true;
					}
				}
			}

		    ViewBag.CurrentWeekOfYear = postViewModel.CurrentWeekOfYear;
            return View(postViewModel);
		}

		[AuthorizationFilter("WeeklyReport_SearchByOrganization")]
		public ActionResult SearchByOrganization()
		{
			SearchByOrganizationViewModel searchByOrganizationViewModel = new SearchByOrganizationViewModel();
			string text = Request.QueryString["organizationId"];
			if (string.IsNullOrEmpty(text))
			{
				searchByOrganizationViewModel.OrganizationId = UserContext.Organization.Id;
				searchByOrganizationViewModel.OrganizationName = UserContext.Organization.Name;
			}
			else
			{
				Organization organization = _domainManager.GetOrganization(Guid.Parse(text));
				if (organization == null)
				{
					searchByOrganizationViewModel.OrganizationId = UserContext.Organization.Id;
					searchByOrganizationViewModel.OrganizationName = UserContext.Organization.Name;
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
			string text2 = Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out year);
			}
			int month = DateTime.Now.Month;
			string text3 = Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text3))
			{
				int.TryParse(text3, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			searchByOrganizationViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(Request.QueryString["week"], searchByOrganizationViewModel.WeekList);
			searchByOrganizationViewModel.Year = year;
			searchByOrganizationViewModel.Month = month;
			searchByOrganizationViewModel.WeekOfYear = weekOfYear2;
			searchByOrganizationViewModel.WeeklyReportList = _weeklyReportManager.GetWeeklyReportListByOrganization(UserContext.Domain.Id, searchByOrganizationViewModel.OrganizationId, year, weekOfYear2);

		    ViewBag.CurrentWeekOfYear = searchByOrganizationViewModel.CurrentWeekOfYear;

            return View(searchByOrganizationViewModel);
		}

		public ActionResult SearchByPersonal()
		{
			SearchByPersonalViewModel searchByPersonalViewModel = new SearchByPersonalViewModel();
			string text = Request.QueryString["userId"];
			if (string.IsNullOrEmpty(text))
			{
				searchByPersonalViewModel.UserId = UserContext.User.Id;
				searchByPersonalViewModel.UserName = UserContext.User.Name;
			}
			else
			{
				searchByPersonalViewModel.UserId = Guid.Parse(text);
				searchByPersonalViewModel.UserName = Request.QueryString["userName"];
			}
			int year = DateTime.Now.Year;
			string text2 = Request.QueryString["startYear"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out year);
			}
			int month = DateTime.Now.Month;
			string text3 = Request.QueryString["startMonth"];
			if (!string.IsNullOrEmpty(text3))
			{
				int.TryParse(text3, out month);
			}
			int year2 = DateTime.Now.Year;
			string text4 = Request.QueryString["endYear"];
			if (!string.IsNullOrEmpty(text4))
			{
				int.TryParse(text4, out year2);
			}
			int month2 = DateTime.Now.Month;
			string text5 = Request.QueryString["endMonth"];
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
			searchByPersonalViewModel.WeeklyReportList = _weeklyReportManager.GetWeeklyReportListByPerson(searchByPersonalViewModel.UserId, year, month, year2, month2);

		    ViewBag.CurrentWeekOfYear = searchByPersonalViewModel.CurrentWeekOfYear;

            return View(searchByPersonalViewModel);
		}

		[AuthorizationFilter("WeeklyReport_SearchByWorkType")]
		public ActionResult SearchByWorkType()
		{
			SearchByWorkTypeViewModel searchByWorkTypeViewModel = new SearchByWorkTypeViewModel();
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			searchByWorkTypeViewModel.CurrentWeekOfYear = weekOfYear;
			int year = DateTime.Now.Year;
			string text = Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text))
			{
				int.TryParse(text, out year);
			}
			int month = DateTime.Now.Month;
			string text2 = Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			searchByWorkTypeViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(Request.QueryString["week"], searchByWorkTypeViewModel.WeekList);
			searchByWorkTypeViewModel.Year = year;
			searchByWorkTypeViewModel.Month = month;
			searchByWorkTypeViewModel.WeekOfYear = weekOfYear2;
			searchByWorkTypeViewModel.WorkTypeList = _settingsManager.GetUserWorkTypeList(UserContext.User.Id);
			searchByWorkTypeViewModel.WorkTaskList = _settingsManager.GetWorkTaskList(UserContext.Domain.Id);
			string text3 = Request.QueryString["workType"];
			if (text3 == "null")
			{
				text3 = null;
			}
			Guid? workType = null;
			if (!string.IsNullOrEmpty(text3))
			{
				workType = new Guid?(Guid.Parse(text3));
			}
			string text4 = Request.QueryString["workTask"];
			Guid? workTask = null;
			if (!string.IsNullOrEmpty(text4))
			{
				workTask = new Guid?(Guid.Parse(text4));
			}
			if (workType.HasValue)
			{
				searchByWorkTypeViewModel.WeeklyReportList = _weeklyReportManager.GetWeeklyReportListByWorkType(UserContext.Domain.Id, workType, workTask, year, weekOfYear2);
			}
			else
			{
				searchByWorkTypeViewModel.WeeklyReportList = new List<WeeklyReport>();
			}

		    ViewBag.CurrentWeekOfYear = searchByWorkTypeViewModel.CurrentWeekOfYear;

            return View(searchByWorkTypeViewModel);
		}

		[AuthorizationFilter("WeeklyReport_Check")]
		public ActionResult Check()
		{
			CheckViewModel checkViewModel = new CheckViewModel();
			int weekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			checkViewModel.CurrentWeekOfYear = weekOfYear;
			int year = DateTime.Now.Year;
			string text = Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text))
			{
				int.TryParse(text, out year);
			}
			int month = DateTime.Now.Month;
			string text2 = Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			checkViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(Request.QueryString["week"], checkViewModel.WeekList);
			checkViewModel.Year = year;
			checkViewModel.Month = month;
			checkViewModel.WeekOfYear = weekOfYear2;
			string value = Request.QueryString["checkViewType"];
			CheckViewType checkViewType;
			if (!string.IsNullOrEmpty(value) && Enum.TryParse<CheckViewType>(value, out checkViewType))
			{
				checkViewModel.CheckViewType = checkViewType;
			}
			checkViewModel.WeeklyReportList = _weeklyReportManager.GetWeeklyReportListForCheck(UserContext.Domain.Id, UserContext.User.Id, year, weekOfYear2, checkViewModel.CheckViewType);

		    ViewBag.CurrentWeekOfYear = checkViewModel.CurrentWeekOfYear;

            return View(checkViewModel);
		}

		[AuthorizationFilter("WeeklyReport_ReportByOrganization")]
		public ActionResult ReportByOrganization()
		{
			ReportByOrganizationViewModel reportByOrganizationViewModel = new ReportByOrganizationViewModel();
			string text = Request.QueryString["organizationId"];
			if (string.IsNullOrEmpty(text))
			{
				reportByOrganizationViewModel.OrganizationId = UserContext.RootOrganization.Id;
				reportByOrganizationViewModel.OrganizationName = UserContext.RootOrganization.Name;
			}
			else
			{
				Organization organization = _domainManager.GetOrganization(Guid.Parse(text));
				if (organization == null)
				{
					reportByOrganizationViewModel.OrganizationId = UserContext.RootOrganization.Id;
					reportByOrganizationViewModel.OrganizationName = UserContext.RootOrganization.Name;
				}
				else
				{
					reportByOrganizationViewModel.OrganizationId = organization.Id;
					reportByOrganizationViewModel.OrganizationName = organization.Name;
				}
			}
			int year = DateTime.Now.Year;
			string text2 = Request.QueryString["startYear"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out year);
			}
			int month = DateTime.Now.Month;
			string text3 = Request.QueryString["startMonth"];
			if (!string.IsNullOrEmpty(text3))
			{
				int.TryParse(text3, out month);
			}
			int year2 = DateTime.Now.Year;
			string text4 = Request.QueryString["endYear"];
			if (!string.IsNullOrEmpty(text4))
			{
				int.TryParse(text4, out year2);
			}
			int month2 = DateTime.Now.Month;
			string text5 = Request.QueryString["endMonth"];
			if (!string.IsNullOrEmpty(text5))
			{
				int.TryParse(text5, out month2);
			}
			reportByOrganizationViewModel.StartYear = year;
			reportByOrganizationViewModel.StartMonth = month;
			reportByOrganizationViewModel.EndYear = year2;
			reportByOrganizationViewModel.EndMonth = month2;
			reportByOrganizationViewModel.Data = _weeklyReportManager.ReportByOrganization(UserContext.Domain.Id, reportByOrganizationViewModel.OrganizationId, year, month, year2, month2);

		    ViewBag.CurrentWeekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);

            return View(reportByOrganizationViewModel);
		}

		[AuthorizationFilter("WeeklyReport_ReportBySubmit")]
		public ActionResult ReportBySubmit()
		{
			ReportBySubmitViewModel reportBySubmitViewModel = new ReportBySubmitViewModel();
			string text = Request.QueryString["organizationId"];
			if (string.IsNullOrEmpty(text))
			{
				reportBySubmitViewModel.OrganizationId = UserContext.RootOrganization.Id;
				reportBySubmitViewModel.OrganizationName = UserContext.RootOrganization.Name;
			}
			else
			{
				Organization organization = _domainManager.GetOrganization(Guid.Parse(text));
				if (organization == null)
				{
					reportBySubmitViewModel.OrganizationId = UserContext.RootOrganization.Id;
					reportBySubmitViewModel.OrganizationName = UserContext.RootOrganization.Name;
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
			string text2 = Request.QueryString["year"];
			if (!string.IsNullOrEmpty(text2))
			{
				int.TryParse(text2, out year);
			}
			int month = DateTime.Now.Month;
			string text3 = Request.QueryString["month"];
			if (!string.IsNullOrEmpty(text3))
			{
				int.TryParse(text3, out month);
			}
			if (year == DateTime.Now.Year && month > DateTime.Now.Month)
			{
				month = DateTime.Now.Month;
			}
			reportBySubmitViewModel.WeekList = DateTimeHelper.GetWeekListOfMonth(year, month);
			int weekOfYear2 = DateTimeHelper.GetWeekOfYear(Request.QueryString["week"], reportBySubmitViewModel.WeekList);
			reportBySubmitViewModel.Year = year;
			reportBySubmitViewModel.Month = month;
			reportBySubmitViewModel.WeekOfYear = weekOfYear2;
			reportBySubmitViewModel.Data = _weeklyReportManager.ReportBySumbit(UserContext.Domain.Id, reportBySubmitViewModel.OrganizationId, year, weekOfYear2);

		    ViewBag.CurrentWeekOfYear = reportBySubmitViewModel.CurrentWeekOfYear;

            return View(reportBySubmitViewModel);
		}
	}
}
