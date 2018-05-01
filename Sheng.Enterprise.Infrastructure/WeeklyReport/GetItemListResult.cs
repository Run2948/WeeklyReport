using System;
using System.Collections.Generic;
using System.Data;

namespace Sheng.Enterprise.Infrastructure
{
	public class GetItemListResult
	{
		public int TotalPage
		{
			get;
			set;
		}

		public int Page
		{
			get;
			set;
		}

		public int TotalCount
		{
			get;
			set;
		}

		public DataTable ItemList
		{
			get;
			set;
		}
	}
	public class GetItemListResult<T>
	{
		public int TotalPage
		{
			get;
			set;
		}

		public int Page
		{
			get;
			set;
		}

		public List<T> ItemList
		{
			get;
			set;
		}
	}
}
