using Linkup.DataRelationalMapping;
using System;

namespace Sheng.Enterprise.Infrastructure
{
	[Table("RoleAuthorization")]
	public class Authorization
	{
		[Column("AuthorizationKey")]
		public string Key
		{
			get;
			set;
		}
	}
}
