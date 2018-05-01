using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class Organization
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

		public Guid? Parent
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Telphone
		{
			get;
			set;
		}

		[OrderBy(OrderBy = OrderBy.ASC)]
		public int Sort
		{
			get;
			set;
		}
	}
}
