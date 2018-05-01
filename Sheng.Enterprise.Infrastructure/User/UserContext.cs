using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class UserContext
	{
		public User User
		{
			get;
			set;
		}

		public Domain Domain
		{
			get;
			set;
		}

		public Organization RootOrganization
		{
			get;
			set;
		}

		public Organization Organization
		{
			get;
			set;
		}

		public List<Role> RoleList
		{
			get;
			set;
		}

		public AuthorizationWrapper Authorization
		{
			get;
			set;
		}

		public UserContext(User user, Domain domain)
		{
			this.User = user;
			this.Domain = domain;
		}
	}
}
