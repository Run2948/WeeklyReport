using Linkup.DataRelationalMapping;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class WeeklyReport
	{
		private Guid _id = Guid.NewGuid();

		private List<WeeklyReportItem> _itemList = new List<WeeklyReportItem>();

		[Key]
		public Guid Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public Guid Domain
		{
			get;
			set;
		}

		public Guid User
		{
			get;
			set;
		}

		[NotMapped]
		public string UserName
		{
			get;
			set;
		}

		public int Year
		{
			get;
			set;
		}

		public int Month
		{
			get;
			set;
		}

		public int WeekOfYear
		{
			get;
			set;
		}

		public DateTime Monday
		{
			get;
			set;
		}

		public DateTime Sunday
		{
			get;
			set;
		}

		[NotMapped]
		public double AllTime
		{
			get
			{
				if (this._itemList == null)
				{
					return 0.0;
				}
				double num = 0.0;
				foreach (WeeklyReportItem current in this._itemList)
				{
					num += current.Time;
				}
				return num;
			}
		}

		public bool Checked
		{
			get;
			set;
		}

		public Guid Checker
		{
			get;
			set;
		}

		public string CheckRemark
		{
			get;
			set;
		}

		[NotMapped]
		public string OrganizationName
		{
			get;
			set;
		}

		[NotMapped]
		public List<WeeklyReportItem> ItemList
		{
			get
			{
				return this._itemList;
			}
			set
			{
				this._itemList = value;
			}
		}
	}
}
