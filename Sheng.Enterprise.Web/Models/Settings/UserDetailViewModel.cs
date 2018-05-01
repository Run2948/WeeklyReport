using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;

namespace Sheng.Enterprise.Web.Models
{
	public class UserDetailViewModel
	{
		private List<WorkType> _workTypeList = new List<WorkType>();

		private List<UserWorkType> _userWorkTypeList = new List<UserWorkType>();

		public List<JobTitle> JobTitleList
		{
			get;
			set;
		}

		public List<JobLevel> JobLevelList
		{
			get;
			set;
		}

		public List<OfficeLocation> OfficeLocationList
		{
			get;
			set;
		}

		public List<WorkType> WorkTypeList
		{
			get
			{
				return this._workTypeList;
			}
			set
			{
				this._workTypeList = value;
			}
		}

		public UserDataWrapper User
		{
			get;
			set;
		}

		public List<UserWorkType> UserWorkTypeList
		{
			get
			{
				return this._userWorkTypeList;
			}
			set
			{
				this._userWorkTypeList = value;
			}
		}
	}
}
