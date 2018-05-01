using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class ExportByOrganizationArgs
	{
		public Guid Domain
		{
			get;
			set;
		}

		public int Year
		{
			get;
			set;
		}

		public int WeekOfYear
		{
			get;
			set;
		}

		public Guid Organization
		{
			get;
			set;
		}

		public List<Guid> UserList
		{
			get;
			set;
		}
	}
}
