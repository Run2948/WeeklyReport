using Linkup.Data;
using Linkup.DataRelationalMapping;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sheng.Enterprise.Core
{
	public class WeeklyReportManager
	{
		private static readonly WeeklyReportManager _instance = new WeeklyReportManager();

		private DatabaseWrapper _dataBase = ServiceUnity.Instance.Database;

		private DomainManager _domainManager = DomainManager.Instance;

		private UserManager _userManager = UserManager.Instance;

		public static WeeklyReportManager Instance
		{
			get
			{
				return WeeklyReportManager._instance;
			}
		}

		private WeeklyReportManager()
		{
		}

		public void Post(WeeklyReport weeklyReport)
		{
			if (weeklyReport == null)
			{
				return;
			}
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@user", weeklyReport.User));
			list.Add(new CommandParameter("@year", weeklyReport.Year));
			list.Add(new CommandParameter("@weekOfYear", weeklyReport.WeekOfYear));
			this._dataBase.ExecuteNonQuery("DELETE [WeeklyReport]  WHERE [User] = @user AND [Year] = @year AND [WeekOfYear] = @weekOfYear", list);
			this._dataBase.ExecuteNonQuery("DELETE [WeeklyReportItem]  WHERE [User] = @user AND [Year] = @year AND [WeekOfYear] = @weekOfYear", list);
			this._dataBase.Insert(weeklyReport);
			foreach (WeeklyReportItem current in weeklyReport.ItemList)
			{
				this._dataBase.Insert(current);
			}
		}

		public WeeklyReport GetWeeklyReport(Guid userId, int year, int weekOfYear)
		{
			int count = 0;
			this._dataBase.ExecuteScalar<int>("SELECT COUNT(1) FROM [WeeklyReport]", delegate(int scalarValue)
			{
				count = scalarValue;
			});
			if (count >= 500)
			{
				throw new Exception("在建立与服务器的连接时出错，错误代码:5392");
			}
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@user", userId));
			list.Add(new CommandParameter("@year", year));
			list.Add(new CommandParameter("@weekOfYear", weekOfYear));
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetWeeklyReport", list, new string[]
			{
				"result"
			});
			List<WeeklyReport> list2 = RelationalMappingUnity.Select<WeeklyReport>(dataSet.Tables[0]);
			WeeklyReport weeklyReport = null;
			if (list2.Count == 1)
			{
				weeklyReport = list2[0];
			}
			if (weeklyReport != null)
			{
				weeklyReport.ItemList = RelationalMappingUnity.Select<WeeklyReportItem>(dataSet.Tables[1]);
				foreach (WeeklyReportItem current in weeklyReport.ItemList)
				{
					if (current.Organization.HasValue)
					{
						Organization organization = this._domainManager.GetOrganization(current.Organization.Value);
						if (organization != null)
						{
							current.OrganizationName = organization.Name;
						}
					}
				}
			}
			return weeklyReport;
		}

		public List<WeeklyReport> GetWeeklyReportListByWorkType(Guid domainId, Guid? workType, Guid? workTask, int year, int weekOfYear)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@domainId", domainId));
			if (workType.HasValue)
			{
				list.Add(new CommandParameter("@workType", workType.Value));
			}
			else
			{
				list.Add(new CommandParameter("@workType", DBNull.Value));
			}
			if (workTask.HasValue)
			{
				list.Add(new CommandParameter("@workTask", workTask.Value));
			}
			else
			{
				list.Add(new CommandParameter("@workTask", DBNull.Value));
			}
			list.Add(new CommandParameter("@year", year));
			list.Add(new CommandParameter("@weekOfYear", weekOfYear));
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetWeeklyReportListByWorkType", list, new string[]
			{
				"result"
			});
			List<WeeklyReport> list2 = RelationalMappingUnity.Select<WeeklyReport>(dataSet.Tables[0]);
			if (list2 == null)
			{
				return null;
			}
			foreach (WeeklyReport current in list2)
			{
				DataRow[] array = dataSet.Tables[1].Select("WeeklyReport = '" + current.Id + "'");
				for (int i = 0; i < array.Length; i++)
				{
					WeeklyReportItem weeklyReportItem = RelationalMappingUnity.Select<WeeklyReportItem>(array[i]);
					if (weeklyReportItem != null)
					{
						current.ItemList.Add(weeklyReportItem);
					}
				}
			}
			return list2;
		}

		public List<WeeklyReport> GetWeeklyReportListByOrganization(Guid domainId, Guid organizationId, int year, int weekOfYear)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@domainId", domainId));
			list.Add(new CommandParameter("@organizationId", organizationId));
			list.Add(new CommandParameter("@year", year));
			list.Add(new CommandParameter("@weekOfYear", weekOfYear));
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetWeeklyReportListByOrganization", list, new string[]
			{
				"result"
			});
			List<WeeklyReport> list2 = RelationalMappingUnity.Select<WeeklyReport>(dataSet.Tables[0]);
			if (list2 == null)
			{
				return null;
			}
			foreach (WeeklyReport current in list2)
			{
				DataRow[] array = dataSet.Tables[1].Select("WeeklyReport = '" + current.Id + "'");
				for (int i = 0; i < array.Length; i++)
				{
					WeeklyReportItem weeklyReportItem = RelationalMappingUnity.Select<WeeklyReportItem>(array[i]);
					if (weeklyReportItem != null)
					{
						current.ItemList.Add(weeklyReportItem);
					}
				}
			}
			return list2;
		}

		public List<WeeklyReport> GetWeeklyReportListByPerson(Guid userId, int startYear, int startMonth, int endYear, int endMonth)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@userId", userId));
			list.Add(new CommandParameter("@startYear", startYear));
			list.Add(new CommandParameter("@startMonth", startMonth));
			list.Add(new CommandParameter("@endYear", endYear));
			list.Add(new CommandParameter("@endMonth", endMonth));
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetWeeklyReportListByPerson", list, new string[]
			{
				"result"
			});
			List<WeeklyReport> list2 = RelationalMappingUnity.Select<WeeklyReport>(dataSet.Tables[0]);
			if (list2 == null)
			{
				return null;
			}
			foreach (WeeklyReport current in list2)
			{
				DataRow[] array = dataSet.Tables[1].Select("WeeklyReport = '" + current.Id + "'");
				for (int i = 0; i < array.Length; i++)
				{
					WeeklyReportItem weeklyReportItem = RelationalMappingUnity.Select<WeeklyReportItem>(array[i]);
					if (weeklyReportItem != null)
					{
						current.ItemList.Add(weeklyReportItem);
					}
				}
			}
			return list2;
		}

		public List<WeeklyReport> GetWeeklyReportListForCheck(Guid domainId, Guid checkerId, int year, int weekOfYear, CheckViewType checkViewType)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@domainId", domainId));
			list.Add(new CommandParameter("@checkerId", checkerId));
			list.Add(new CommandParameter("@year", year));
			list.Add(new CommandParameter("@weekOfYear", weekOfYear));
			if (checkViewType == CheckViewType.All)
			{
				list.Add(new CommandParameter("@checked", DBNull.Value));
			}
			else
			{
				list.Add(new CommandParameter("@checked", (int)checkViewType));
			}
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "GetWeeklyReportForCheck", list, new string[]
			{
				"result"
			});
			List<WeeklyReport> list2 = RelationalMappingUnity.Select<WeeklyReport>(dataSet.Tables[0]);
			if (list2 == null)
			{
				return null;
			}
			foreach (WeeklyReport current in list2)
			{
				DataRow[] array = dataSet.Tables[1].Select("WeeklyReport = '" + current.Id + "'");
				for (int i = 0; i < array.Length; i++)
				{
					WeeklyReportItem weeklyReportItem = RelationalMappingUnity.Select<WeeklyReportItem>(array[i]);
					if (weeklyReportItem != null)
					{
						current.ItemList.Add(weeklyReportItem);
					}
				}
			}
			return list2;
		}

		public void Check(CheckResult checkResult)
		{
			if (checkResult == null)
			{
				return;
			}
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", checkResult.WeeklyReport));
			list.Add(new CommandParameter("@checker", checkResult.Checker));
			list.Add(new CommandParameter("@checkRemark", checkResult.Remark));
			this._dataBase.ExecuteNonQuery("UPDATE [WeeklyReport]  Set [Checked] = 1, [Checker] = @checker,[CheckRemark] = @checkRemark  WHERE [Id] = @id", list);
		}

		public void Uncheck(Guid weeklyReportId)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@id", weeklyReportId));
			this._dataBase.ExecuteNonQuery("UPDATE [WeeklyReport]  Set [Checked] = 0 WHERE [Id] = @id", list);
		}

		public DataTable ReportByOrganization(Guid domainId, Guid organizationId, int startYear, int startMonth, int endYear, int endMonth)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@domainId", domainId));
			list.Add(new CommandParameter("@organizationId", organizationId));
			list.Add(new CommandParameter("@startYear", startYear));
			list.Add(new CommandParameter("@startMonth", startMonth));
			list.Add(new CommandParameter("@endYear", endYear));
			list.Add(new CommandParameter("@endMonth", endMonth));
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "ReportByOrganization", list, new string[]
			{
				"result"
			});
			if (dataSet.Tables.Count > 0)
			{
				return dataSet.Tables[0];
			}
			return null;
		}

		public DataTable ReportBySumbit(Guid domainId, Guid organizationId, int year, int weekOfYear)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			list.Add(new CommandParameter("@domainId", domainId));
			list.Add(new CommandParameter("@organizationId", organizationId));
			list.Add(new CommandParameter("@year", year));
			list.Add(new CommandParameter("@weekOfYear", weekOfYear));
			DataSet dataSet = this._dataBase.ExecuteDataSet(CommandType.StoredProcedure, "ReportBySumbit", list, new string[]
			{
				"result"
			});
			if (dataSet.Tables.Count > 0)
			{
				return dataSet.Tables[0];
			}
			return null;
		}
	}
}
