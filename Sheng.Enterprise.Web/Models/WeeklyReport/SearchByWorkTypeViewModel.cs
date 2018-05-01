using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class SearchByWorkTypeViewModel
	{
		private List<WorkType> _workTypeList = new List<WorkType>();

		private List<WorkTask> _workTaskList = new List<WorkTask>();

		public List<WorkType> WorkTypeList
		{
			get
			{
				return this._workTypeList;
			}
			set
			{
				this._workTypeList = value;
			}
		}

		public List<WorkTask> WorkTaskList
		{
			get
			{
				return this._workTaskList;
			}
			set
			{
				this._workTaskList = value;
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
