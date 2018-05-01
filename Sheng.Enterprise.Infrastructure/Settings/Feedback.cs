using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class Feedback
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

		public Guid User
		{
			get;
			set;
		}

		public string IP
		{
			get;
			set;
		}

		public DateTime Time
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Contact
		{
			get;
			set;
		}

		public string Content
		{
			get;
			set;
		}
	}
}
