using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class User
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

		[Column("Domain")]
		public Guid DomainId
		{
			get;
			set;
		}

		[Column("Organization")]
		public Guid OrganizationId
		{
			get;
			set;
		}

		public string Account
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string Number
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		[Column("JobTitle")]
		public Guid JobTitleId
		{
			get;
			set;
		}

		[Column("JobLevel")]
		public Guid JobLevelId
		{
			get;
			set;
		}

		[Column("OfficeLocation")]
		public Guid OfficeLocationId
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public string ExtTelphone
		{
			get;
			set;
		}

		public string Telphone
		{
			get;
			set;
		}

		public string Cellphone
		{
			get;
			set;
		}

		public bool Notify
		{
			get;
			set;
		}

		public bool Removed
		{
			get;
			set;
		}
	}
}
