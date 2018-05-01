using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class SearchByPersonalViewModel
	{
		public Guid UserId
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public int StartYear
		{
			get;
			set;
		}

		public int StartMonth
		{
			get;
			set;
		}

		public int EndYear
		{
			get;
			set;
		}

		public int EndMonth
		{
			get;
			set;
		}

		public int CurrentWeekOfYear
		{
			get;
			set;
		}

		public List<WeeklyReport> WeeklyReportList
		{
			get;
			set;
		}
	}
}
