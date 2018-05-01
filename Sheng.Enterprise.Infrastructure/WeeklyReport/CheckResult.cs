using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class CheckResult
	{
		public Guid WeeklyReport
		{
			get;
			set;
		}

		public Guid Domain
		{
			get;
			set;
		}

		public Guid Checker
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

		public string Remark
		{
			get;
			set;
		}
	}
}
