using Linkup.Data;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sheng.Enterprise.Core
{
	public class DomainManager
	{
		private static readonly DomainManager _instance = new DomainManager();

		private DatabaseWrapper _dataBase = ServiceUnity.Instance.Database;

		private object _lockObj = new object();

		public static DomainManager Instance
		{
			get
			{
				return DomainManager._instance;
			}
		}

		private DomainManager()
		{
		}

		public void Create(Domain domain)
		{
			if (domain == null)
			{
				return;
			}
			this._dataBase.Insert(domain);
			WorkType workType = new WorkType();
			workType.Id = Guid.NewGuid();
			workType.Domain = domain.Id;
			workType.Name = "日常工作";
			this._dataBase.Insert(workType);
			WorkTask workTask = new WorkTask();
			workTask.Id = Guid.NewGuid();
			workTask.Domain = domain.Id;
			workTask.Name = "示例任务1";
			workTask.WorkType = workType.Id;
			this._dataBase.Insert(workTask);
			WorkTask workTask2 = new WorkTask();
			workTask2.Id = Guid.NewGuid();
			workTask2.Domain = domain.Id;
			workTask2.Name = "示例任务2";
			workTask2.WorkType = workType.Id;
			this._dataBase.Insert(workTask2);
			WorkStatus workStatus = new WorkStatus();
			workStatus.Id = Guid.NewGuid();
			workStatus.Domain = domain.Id;
			workStatus.Name = "已完成";
			this._dataBase.Insert(workStatus);
			WorkStatus workStatus2 = new WorkStatus();
			workStatus2.Id = Guid.NewGuid();
			workStatus2.Domain = domain.Id;
			workStatus2.Name = "进行中";
			this._dataBase.Insert(workStatus2);
		}

		public Domain GetDomain(Guid id)
		{
			Domain domain = new Domain();
			domain.Id = id;
			if (this._dataBase.Fill<Domain>(domain))
			{
				return domain;
			}
			return null;
		}

		public void UpdateDomain(Domain domain)
		{
		}

		public Organization GetOrganization(Guid id)
		{
			Organization organization = new Organization();
			organization.Id = id;
			if (this._dataBase.Fill<Organization>(organization))
			{
				return organization;
			}
			return null;
		}

		public List<Organization> GetOrganizationList(Guid domainId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Domain", domainId);
			return this._dataBase.Select<Organization>(dictionary);
		}

		public void UpdateOrganization(Organization organization)
		{
			if (organization == null)
			{
				return;
			}
			this._dataBase.Update(organization);
		}

		public void CreateOrganization(Organization organization)
		{
			if (organization == null)
			{
				return;
			}
			int organizationSort = this.GetOrganizationSort(organization.Domain);
			organization.Sort = organizationSort + 1;
			this._dataBase.Insert(organization);
		}

		public void RemoveOrganization(Guid id)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", id));
			this._dataBase.ExecuteNonQuery(CommandType.StoredProcedure, "RemoveOrganization", list);
		}

		public int GetOrganizationSort(Guid domainId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@domain", domainId));
			int count = 0;
			this._dataBase.ExecuteScalar<int>("SELECT TOP 1 [Sort] FROM [Organization] WHERE [Domain] = @domain ORDER BY [Sort] DESC", list, delegate(int scalarValue)
			{
				count = scalarValue;
			});
			return count;
		}

		public void SwapSort(Guid id1, Guid id2, string table)
		{
			SortDigest sortDigest = new SortDigest();
			sortDigest.Id = id1;
			this._dataBase.Fill<SortDigest>(sortDigest, table);
			SortDigest sortDigest2 = new SortDigest();
			sortDigest2.Id = id2;
			this._dataBase.Fill<SortDigest>(sortDigest2, table);
			int sort = sortDigest.Sort;
			sortDigest.Sort = sortDigest2.Sort;
			sortDigest2.Sort = sort;
			this._dataBase.Update(sortDigest, table);
			this._dataBase.Update(sortDigest2, table);
		}
	}
}
