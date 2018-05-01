using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class UserRegisterResult
	{
		public UserRegisterResultEnum Result
		{
			get;
			set;
		}

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
	}
}
