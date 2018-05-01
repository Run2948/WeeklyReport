using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class JobLevelViewModel
	{
		public Domain Domain
		{
			get;
			set;
		}

		public List<JobLevel> JobLevelList
		{
			get;
			set;
		}
	}
}
