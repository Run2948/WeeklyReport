using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Sheng.Enterprise.Core;
using Sheng.Enterprise.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sheng.Enterprise.Web
{
	public class ExcelHelper
	{
		private static string _templateFile = "template.xls";

		private static readonly WeeklyReportManager _weeklyReportManager = WeeklyReportManager.Instance;

		private static readonly UserManager _userManager = UserManager.Instance;

		private static readonly SettingsManager _settingsManager = SettingsManager.Instance;

		private static readonly DomainManager _domainManager = DomainManager.Instance;

		public static string ExportPersonal(string rootPath, ExportByPersonalArgs args)
		{
			List<WeeklyReport> arg_2E_0 = ExcelHelper._weeklyReportManager.GetWeeklyReportListByPerson(args.User, args.StartYear, args.StartMonth, args.EndYear, args.EndMonth);
			List<WeeklyReport> list = new List<WeeklyReport>();
			foreach (WeeklyReport current in arg_2E_0)
			{
				foreach (ExportByPersonalItem current2 in args.ExportItemList)
				{
					if (current.Year == current2.Year && current.WeekOfYear == current2.WeekOfYear)
					{
						list.Add(current);
						break;
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			IWorkbook workbook = null;
			FileStream fileStream = null;
			string text = Path.Combine(rootPath, ExcelHelper._templateFile);
			FileStream fileStream2 = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read);
			if (text.IndexOf(".xlsx") > 0)
			{
				workbook = new XSSFWorkbook(fileStream2);
			}
			else if (text.IndexOf(".xls") > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
			}
			ISheet sheet = null;
			string text3;
			try
			{
				sheet = workbook.GetSheetAt(0);
				string text2 = list[0].Monday.ToString("yyyyMMdd") + "-";
				text2 += list[list.Count - 1].Sunday.ToString("yyyyMMdd");
				text2 += "-";
				User user = ExcelHelper._userManager.GetUser(args.User);
				text2 += user.Name;
				text2 += "-周报";
				sheet.GetRow(0).GetCell(0).SetCellValue(text2);
				ICellStyle cellStyle = workbook.CreateCellStyle();
				cellStyle.BorderBottom = BorderStyle.Thin;
				cellStyle.BorderLeft = BorderStyle.Thin;
				cellStyle.BorderRight = BorderStyle.Thin;
				cellStyle.BorderTop = BorderStyle.Thin;
				cellStyle.VerticalAlignment = VerticalAlignment.Center;
				cellStyle.WrapText = true;
				int num = 2;
				foreach (WeeklyReport current3 in list)
				{
					int firstRow = num;
					foreach (WeeklyReportItem current4 in current3.ItemList)
					{
						IRow expr_234 = sheet.CreateRow(num);
						expr_234.Height = 350;
						expr_234.CreateCell(0).SetCellValue(current3.Monday.ToString("yyyyMMdd") + "-" + current3.Sunday.ToString("yyyyMMdd"));
						expr_234.CreateCell(1).SetCellValue(current4.OrganizationName);
						expr_234.CreateCell(2).SetCellValue(current3.UserName);
						expr_234.CreateCell(3).SetCellValue(current4.WorkTypeName);
						expr_234.CreateCell(4).SetCellValue(current4.WorkTaskName);
						expr_234.CreateCell(5).SetCellValue(current4.Content.Replace("<br/>", "\r\n"));
						expr_234.CreateCell(6).SetCellValue(current4.StatusName);
						ICell cell = expr_234.CreateCell(7);
						if (current4.Date.HasValue)
						{
							cell.SetCellValue(current4.Date.Value.ToString("yyyy-MM-dd"));
						}
						using (List<ICell>.Enumerator enumerator4 = expr_234.Cells.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								enumerator4.Current.CellStyle = cellStyle;
							}
						}
						num++;
					}
					CellRangeAddress region = new CellRangeAddress(firstRow, num - 1, 0, 0);
					sheet.AddMergedRegion(region);
					region = new CellRangeAddress(firstRow, num - 1, 1, 1);
					sheet.AddMergedRegion(region);
					region = new CellRangeAddress(firstRow, num - 1, 2, 2);
					sheet.AddMergedRegion(region);
				}
				text3 = Path.Combine(rootPath, "ExcelExport", text2 + "(" + DateTime.Now.ToString("ddHHmmss") + ").xls");
				fileStream = new FileStream(text3, FileMode.OpenOrCreate, FileAccess.Write);
				workbook.Write(fileStream);
			}
			catch (Exception arg_440_0)
			{
				throw arg_440_0;
			}
			finally
			{
				fileStream2.Close();
				fileStream2.Dispose();
				if (fileStream != null)
				{
					fileStream.Flush();
					fileStream.Close();
					fileStream.Dispose();
				}
			}
			return text3;
		}

		public static string ExportWorkType(string rootPath, ExportByWorkTypeArgs args)
		{
			List<WeeklyReport> arg_2E_0 = ExcelHelper._weeklyReportManager.GetWeeklyReportListByWorkType(args.Domain, args.WorkType, args.WorkTask, args.Year, args.WeekOfYear);
			List<WeeklyReport> list = new List<WeeklyReport>();
			foreach (WeeklyReport current in arg_2E_0)
			{
				if (args.UserList.Contains(current.User))
				{
					list.Add(current);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			string text = "全部项目";
			if (args.WorkType.HasValue)
			{
				WorkType workType = ExcelHelper._settingsManager.GetWorkType(args.WorkType.Value);
				if (workType == null)
				{
					text = "未知";
				}
				else
				{
					text = workType.Name;
				}
			}
			string text2 = "全部子项目";
			if (args.WorkTask.HasValue)
			{
				WorkTask workTask = ExcelHelper._settingsManager.GetWorkTask(args.WorkTask.Value);
				if (workTask == null)
				{
					text2 = "未知";
				}
				else
				{
					text2 = workTask.Name;
				}
			}
			IWorkbook workbook = null;
			FileStream fileStream = null;
			string text3 = Path.Combine(rootPath, ExcelHelper._templateFile);
			FileStream fileStream2 = new FileStream(text3, FileMode.Open, FileAccess.Read, FileShare.Read);
			if (text3.IndexOf(".xlsx") > 0)
			{
				workbook = new XSSFWorkbook(fileStream2);
			}
			else if (text3.IndexOf(".xls") > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
			}
			ISheet sheet = null;
			string text5;
			try
			{
				sheet = workbook.GetSheetAt(0);
				string text4 = list[0].Monday.ToString("yyyyMMdd") + "-";
				text4 += list[list.Count - 1].Sunday.ToString("yyyyMMdd");
				text4 += "-";
				text4 = string.Concat(new string[]
				{
					text4,
					text,
					"-",
					text2,
					"-周报"
				});
				sheet.GetRow(0).GetCell(0).SetCellValue(text4);
				ICellStyle cellStyle = workbook.CreateCellStyle();
				cellStyle.BorderBottom = BorderStyle.Thin;
				cellStyle.BorderLeft = BorderStyle.Thin;
				cellStyle.BorderRight = BorderStyle.Thin;
				cellStyle.BorderTop = BorderStyle.Thin;
				cellStyle.VerticalAlignment = VerticalAlignment.Center;
				cellStyle.WrapText = true;
				int num = 2;
				foreach (WeeklyReport current2 in list)
				{
					int firstRow = num;
					foreach (WeeklyReportItem current3 in current2.ItemList)
					{
						IRow expr_27C = sheet.CreateRow(num);
						expr_27C.Height = 350;
						expr_27C.CreateCell(0).SetCellValue(current2.Monday.ToString("yyyyMMdd") + "-" + current2.Sunday.ToString("yyyyMMdd"));
						expr_27C.CreateCell(1).SetCellValue(current3.OrganizationName);
						expr_27C.CreateCell(2).SetCellValue(current2.UserName);
						expr_27C.CreateCell(3).SetCellValue(current3.WorkTypeName);
						expr_27C.CreateCell(4).SetCellValue(current3.WorkTaskName);
						expr_27C.CreateCell(5).SetCellValue(current3.Content.Replace("<br/>", "\r\n"));
						expr_27C.CreateCell(6).SetCellValue(current3.StatusName);
						ICell cell = expr_27C.CreateCell(7);
						if (current3.Date.HasValue)
						{
							cell.SetCellValue(current3.Date.Value.ToString("yyyy-MM-dd"));
						}
						using (List<ICell>.Enumerator enumerator3 = expr_27C.Cells.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								enumerator3.Current.CellStyle = cellStyle;
							}
						}
						num++;
					}
					CellRangeAddress region = new CellRangeAddress(firstRow, num - 1, 0, 0);
					sheet.AddMergedRegion(region);
					region = new CellRangeAddress(firstRow, num - 1, 1, 1);
					sheet.AddMergedRegion(region);
					region = new CellRangeAddress(firstRow, num - 1, 2, 2);
					sheet.AddMergedRegion(region);
				}
				text5 = Path.Combine(rootPath, "ExcelExport", text4 + "(" + DateTime.Now.ToString("ddHHmmss") + ").xls");
				fileStream = new FileStream(text5, FileMode.OpenOrCreate, FileAccess.Write);
				workbook.Write(fileStream);
			}
			catch (Exception arg_48C_0)
			{
				throw arg_48C_0;
			}
			finally
			{
				fileStream2.Close();
				fileStream2.Dispose();
				if (fileStream != null)
				{
					fileStream.Flush();
					fileStream.Close();
					fileStream.Dispose();
				}
			}
			return text5;
		}

		public static string ExportOrganization(string rootPath, ExportByOrganizationArgs args)
		{
			List<WeeklyReport> arg_28_0 = ExcelHelper._weeklyReportManager.GetWeeklyReportListByOrganization(args.Domain, args.Organization, args.Year, args.WeekOfYear);
			List<WeeklyReport> list = new List<WeeklyReport>();
			foreach (WeeklyReport current in arg_28_0)
			{
				if (args.UserList.Contains(current.User))
				{
					list.Add(current);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			IWorkbook workbook = null;
			FileStream fileStream = null;
			string text = Path.Combine(rootPath, ExcelHelper._templateFile);
			FileStream fileStream2 = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read);
			if (text.IndexOf(".xlsx") > 0)
			{
				workbook = new XSSFWorkbook(fileStream2);
			}
			else if (text.IndexOf(".xls") > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
			}
			ISheet sheet = null;
			string text3;
			try
			{
				sheet = workbook.GetSheetAt(0);
				string text2 = list[0].Monday.ToString("yyyyMMdd") + "-";
				text2 += list[list.Count - 1].Sunday.ToString("yyyyMMdd");
				text2 += "-";
				Organization organization = ExcelHelper._domainManager.GetOrganization(args.Organization);
				text2 += organization.Name;
				text2 += "-周报";
				sheet.GetRow(0).GetCell(0).SetCellValue(text2);
				ICellStyle cellStyle = workbook.CreateCellStyle();
				cellStyle.BorderBottom = BorderStyle.Thin;
				cellStyle.BorderLeft = BorderStyle.Thin;
				cellStyle.BorderRight = BorderStyle.Thin;
				cellStyle.BorderTop = BorderStyle.Thin;
				cellStyle.VerticalAlignment = VerticalAlignment.Center;
				cellStyle.WrapText = true;
				int num = 2;
				foreach (WeeklyReport current2 in list)
				{
					int firstRow = num;
					foreach (WeeklyReportItem current3 in current2.ItemList)
					{
						IRow expr_1EF = sheet.CreateRow(num);
						expr_1EF.Height = 350;
						expr_1EF.CreateCell(0).SetCellValue(current2.Monday.ToString("yyyyMMdd") + "-" + current2.Sunday.ToString("yyyyMMdd"));
						expr_1EF.CreateCell(1).SetCellValue(current3.OrganizationName);
						expr_1EF.CreateCell(2).SetCellValue(current2.UserName);
						expr_1EF.CreateCell(3).SetCellValue(current3.WorkTypeName);
						expr_1EF.CreateCell(4).SetCellValue(current3.WorkTaskName);
						expr_1EF.CreateCell(5).SetCellValue(current3.Content.Replace("<br/>", "\r\n"));
						expr_1EF.CreateCell(6).SetCellValue(current3.StatusName);
						ICell cell = expr_1EF.CreateCell(7);
						if (current3.Date.HasValue)
						{
							cell.SetCellValue(current3.Date.Value.ToString("yyyy-MM-dd"));
						}
						using (List<ICell>.Enumerator enumerator3 = expr_1EF.Cells.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								enumerator3.Current.CellStyle = cellStyle;
							}
						}
						num++;
					}
					CellRangeAddress region = new CellRangeAddress(firstRow, num - 1, 0, 0);
					sheet.AddMergedRegion(region);
					region = new CellRangeAddress(firstRow, num - 1, 1, 1);
					sheet.AddMergedRegion(region);
					region = new CellRangeAddress(firstRow, num - 1, 2, 2);
					sheet.AddMergedRegion(region);
				}
				text3 = Path.Combine(rootPath, "ExcelExport", text2 + "(" + DateTime.Now.ToString("ddHHmmss") + ").xls");
				fileStream = new FileStream(text3, FileMode.OpenOrCreate, FileAccess.Write);
				workbook.Write(fileStream);
			}
			catch (Exception arg_3FB_0)
			{
				throw arg_3FB_0;
			}
			finally
			{
				fileStream2.Close();
				fileStream2.Dispose();
				if (fileStream != null)
				{
					fileStream.Flush();
					fileStream.Close();
					fileStream.Dispose();
				}
			}
			return text3;
		}
	}
}
