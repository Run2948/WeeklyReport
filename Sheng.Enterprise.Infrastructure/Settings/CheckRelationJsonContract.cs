using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class CheckRelationJsonContract
	{
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

		public List<Guid> StaffList
		{
			get;
			set;
		}
	}
}
