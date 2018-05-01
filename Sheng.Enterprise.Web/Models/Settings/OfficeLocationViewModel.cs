using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class OfficeLocationViewModel
	{
		public Domain Domain
		{
			get;
			set;
		}

		public List<OfficeLocation> OfficeLocationList
		{
			get;
			set;
		}
	}
}
