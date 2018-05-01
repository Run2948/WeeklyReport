using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class CheckRelation
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

		public Guid Checker
		{
			get;
			set;
		}

		public Guid Staff
		{
			get;
			set;
		}
	}
}
