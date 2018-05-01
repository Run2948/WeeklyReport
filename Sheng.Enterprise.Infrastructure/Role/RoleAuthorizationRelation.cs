using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class RoleAuthorizationRelation
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

		public List<Authorization> AuthorizationList
		{
			get;
			set;
		}
	}
}
