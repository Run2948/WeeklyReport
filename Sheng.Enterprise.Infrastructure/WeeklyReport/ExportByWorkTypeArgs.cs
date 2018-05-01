using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class ExportByWorkTypeArgs
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

		public Guid? WorkType
		{
			get;
			set;
		}

		public Guid? WorkTask
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
