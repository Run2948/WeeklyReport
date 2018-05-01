using System;

namespace Sheng.Enterprise.Infrastructure
{
	public class GetItemListArgs
	{
		private int _pageSize = 10;

		public int Page
		{
			get;
			set;
		}

		public int PageSize
		{
			get
			{
				return this._pageSize;
			}
			set
			{
				this._pageSize = value;
			}
		}
	}
}
