using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class ExportByPersonalArgs
	{
		public Guid User
		{
			get;
			set;
		}

		public int StartYear
		{
			get;
			set;
		}

		public int StartMonth
		{
			get;
			set;
		}

		public int EndYear
		{
			get;
			set;
		}

		public int EndMonth
		{
			get;
			set;
		}

		public List<ExportByPersonalItem> ExportItemList
		{
			get;
			set;
		}
	}
}
