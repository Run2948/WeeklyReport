using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class UserDto
	{
		public User User
		{
			get;
			set;
		}

		public List<Guid> WorkTypeList
		{
			get;
			set;
		}
	}
}
