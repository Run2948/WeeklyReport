using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class CheckRelationViewModel
	{
		private List<CheckerWrapper> _checkerWrapperList = new List<CheckerWrapper>();

		public List<CheckerWrapper> CheckerWrapperList
		{
			get
			{
				return this._checkerWrapperList;
			}
			set
			{
				this._checkerWrapperList = value;
			}
		}
	}
}
