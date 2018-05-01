using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class ApiResult
	{
		public bool Success
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public object Data
		{
			get;
			set;
		}
	}
}
