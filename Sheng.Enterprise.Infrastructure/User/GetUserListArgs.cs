using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class GetUserListArgs : GetItemListArgs
	{
		public Guid DomainId
		{
			get;
			set;
		}

		public Guid OrganizationId
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int SearchOrganization
		{
			get;
			set;
		}
	}
}
