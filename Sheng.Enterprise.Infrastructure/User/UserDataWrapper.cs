using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class UserDataWrapper : User
	{
		private string _organizationName;

		[NotMapped]
		public string DomainName
		{
			get;
			set;
		}

		[NotMapped]
		public string OrganizationName
		{
			get
			{
				if (string.IsNullOrEmpty(this._organizationName))
				{
					return this.DomainName;
				}
				return this._organizationName;
			}
			set
			{
				this._organizationName = value;
			}
		}

		[NotMapped]
		public string JobTitleName
		{
			get;
			set;
		}

		[NotMapped]
		public string JobLevelName
		{
			get;
			set;
		}

		[NotMapped]
		public string OfficeLocationName
		{
			get;
			set;
		}
	}
}
