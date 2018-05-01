using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class WeeklyReportCheckList
	{
		private Guid _id = Guid.NewGuid();

		[Key]
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

		public Guid CheckPoint
		{
			get;
			set;
		}

		public Guid Value
		{
			get;
			set;
		}
	}
}
