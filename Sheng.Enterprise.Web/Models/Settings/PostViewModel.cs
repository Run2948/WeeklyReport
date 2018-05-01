using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class PostViewModel
	{
		private List<WorkType> _workTypeList = new List<WorkType>();

		private List<WorkTask> _workTaskList = new List<WorkTask>();

		private List<WorkStatus> _workStatusList = new List<WorkStatus>();

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

		public List<WorkStatus> WorkStatusList
		{
			get
			{
				return this._workStatusList;
			}
			set
			{
				this._workStatusList = value;
			}
		}

		public WeeklyReport WeeklyReport
		{
			get;
			set;
		}

		public DateTime ItemStartDate
		{
			get;
			set;
		}

		public DateTime ItemEndDate
		{
			get;
			set;
		}

		public bool AllowPost
		{
			get;
			set;
		}

		public bool Notify
		{
			get;
			set;
		}

		public bool AlertNotify
		{
			get;
			set;
		}
	}
}
