using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class JobTitleViewModel
	{
		public Domain Domain
		{
			get;
			set;
		}

		public List<JobTitle> JobTitleList
		{
			get;
			set;
		}
	}
}
