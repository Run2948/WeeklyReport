using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class CheckViewModel
	{
		private CheckViewType _checkViewType = CheckViewType.All;

		public CheckViewType CheckViewType
		{
			get
			{
				return this._checkViewType;
			}
			set
			{
				this._checkViewType = value;
			}
		}

		public int Year
		{
			get;
			set;
		}

		public int Month
		{
			get;
			set;
		}

		public int WeekOfYear
		{
			get;
			set;
		}

		public int CurrentWeekOfYear
		{
			get;
			set;
		}

		public List<Week> WeekList
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
