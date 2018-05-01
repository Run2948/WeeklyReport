using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class CheckerWrapper
	{
		private List<CheckStaffWrapper> _staffList = new List<CheckStaffWrapper>();

		public Guid Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public List<CheckStaffWrapper> StaffList
		{
			get
			{
				return this._staffList;
			}
			set
			{
				this._staffList = value;
			}
		}
	}
}
