using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class SortDigest
	{
		[Key]
		public Guid Id
		{
			get;
			set;
		}

		public int Sort
		{
			get;
			set;
		}
	}
}
