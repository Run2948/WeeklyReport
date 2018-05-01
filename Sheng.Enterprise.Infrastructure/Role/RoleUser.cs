using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class RoleUser
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

		public Guid User
		{
			get;
			set;
		}
	}
}
