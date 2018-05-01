using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class ResetPasswordArgs
	{
		public string Account
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}
	}
}
