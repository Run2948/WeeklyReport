using System;
using System.Data;

namespace Sheng.Enterprise.Web.Models
{
	public class ReportByOrganizationViewModel
	{
		public Guid OrganizationId
		{
			get;
			set;
		}

		public string OrganizationName
		{
			get;
			set;
		}

		public int StartYear
		{
			get;
			set;
		}

		public int StartMonth
		{
			get;
			set;
		}

		public int EndYear
		{
			get;
			set;
		}

		public int EndMonth
		{
			get;
			set;
		}

		public DataTable Data
		{
			get;
			set;
		}
	}
}
