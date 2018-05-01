using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class WeeklyReportItem
	{
		private Guid _id = Guid.NewGuid();

		private string _content;

		public Guid Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public Guid Domain
		{
			get;
			set;
		}

		public Guid? Organization
		{
			get;
			set;
		}

		public Guid User
		{
			get;
			set;
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

		public DateTime Monday
		{
			get;
			set;
		}

		public DateTime Sunday
		{
			get;
			set;
		}

		public Guid WeeklyReport
		{
			get;
			set;
		}

		public Guid WorkType
		{
			get;
			set;
		}

		public Guid WorkTask
		{
			get;
			set;
		}

		public string Content
		{
			get
			{
				if (this._content == null)
				{
					return string.Empty;
				}
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		public Guid Status
		{
			get;
			set;
		}

		public double Time
		{
			get;
			set;
		}

		public DateTime? Date
		{
			get;
			set;
		}

		public int Sort
		{
			get;
			set;
		}

		[NotMapped]
		public string OrganizationName
		{
			get;
			set;
		}

		[NotMapped]
		public string WorkTypeName
		{
			get;
			set;
		}

		[NotMapped]
		public string WorkTaskName
		{
			get;
			set;
		}

		[NotMapped]
		public string StatusName
		{
			get;
			set;
		}
	}
}
