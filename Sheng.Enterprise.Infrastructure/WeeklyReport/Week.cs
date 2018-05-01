using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class Week
	{
		private bool _currentWeek;

		public int WeekOfYear
		{
			get;
			set;
		}

		public int WeekOfMonth
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

		public bool CurrentWeek
		{
			get
			{
				return this._currentWeek;
			}
			set
			{
				this._currentWeek = value;
			}
		}
	}
}
