using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class RoleAuthorization
	{
		public Guid Domain
		{
			get;
			set;
		}

		public Guid Role
		{
			get;
			set;
		}

		public string AuthorizationKey
		{
			get;
			set;
		}
	}
}
