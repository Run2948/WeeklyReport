using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sheng.Enterprise.Infrastructure
{
	public static class DateTimeHelper
	{
		public static int GetWeekOfYear(DateTime dt)
		{
			return new GregorianCalendar().GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
		}

		public static int GetWeekOfYear(string strWeek, List<Week> weekList)
		{
			int currentWeeekOfYear = DateTimeHelper.GetWeekOfYear(DateTime.Now);
			int num = -1;
			if (!string.IsNullOrEmpty(strWeek))
			{
				int.TryParse(strWeek, out num);
			}
			if (num < weekList[0].WeekOfYear || num > weekList[weekList.Count - 1].WeekOfYear)
			{
				if ((from c in weekList
				where c.WeekOfYear == currentWeeekOfYear
				select c).ToList<Week>().Count > 0)
				{
					num = currentWeeekOfYear;
				}
				else
				{
					num = weekList[0].WeekOfYear;
				}
			}
			return num;
		}

		public static int GetWeekOfMonth(DateTime dt)
		{
			int num = 1;
			int num2 = (int)Convert.ToDateTime(string.Concat(new object[]
			{
				dt.Date.Year,
				"-",
				dt.Date.Month,
				"-",
				1
			})).Date.DayOfWeek;
			if (num2 == 0)
			{
				num2 = 7;
			}
			if (num == 1)
			{
				return (dt.Date.Day + num2 - 2) / 7 + 1;
			}
			if (num == 2)
			{
				return (dt.Date.Day + num2 - 1) / 7;
			}
			return 0;
		}

		public static DateTime CalculateFirstDateOfWeek(DateTime someDate)
		{
			int num = someDate.DayOfWeek - DayOfWeek.Monday;
			if (num == -1)
			{
				num = 6;
			}
			TimeSpan value = new TimeSpan(num, 0, 0, 0);
			return someDate.Subtract(value);
		}

		public static DateTime CalculateLastDateOfWeek(DateTime someDate)
		{
			int num = someDate.DayOfWeek - DayOfWeek.Sunday;
			if (num != 0)
			{
				num = 7 - num;
			}
			TimeSpan value = new TimeSpan(num, 0, 0, 0);
			return someDate.Add(value);
		}

		public static void GetWeek(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
		{
			DateTime d = new DateTime(nYear, 1, 1);
			d += new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
			int num = (int)d.DayOfWeek;
			if (num == 0)
			{
				num = 7;
			}
			dtWeekStart = d.AddDays((double)(-(double)num + 1));
			dtWeekeEnd = d.AddDays((double)(6 - num + 1));
		}

		public static bool IsThisWeek(DateTime someDate)
		{
			DateTime arg_12_0 = DateTimeHelper.CalculateFirstDateOfWeek(someDate);
			DateTime d = DateTimeHelper.CalculateFirstDateOfWeek(DateTime.Now);
			TimeSpan t = arg_12_0 - d;
			if (t.Days < 0)
			{
				t = -t;
			}
			return t.Days < 7;
		}

		public static int GetYearWeekCount(int strYear)
		{
			DateTime dateTime = DateTime.Parse(strYear.ToString() + "-01-01");
			if (Convert.ToInt32(dateTime.DayOfWeek) == 1)
			{
				return dateTime.AddYears(1).AddDays(-1.0).DayOfYear / 7 + 1;
			}
			return dateTime.AddYears(1).AddDays(-1.0).DayOfYear / 7 + 2;
		}

		public static List<Week> GetWeekListOfMonth(int year, int month)
		{
			DateTime now = DateTime.Now;
			if (year == 0)
			{
				year = now.Year;
			}
			if (month == 0)
			{
				month = now.Month;
			}
			List<Week> list = new List<Week>();
			int daysInMonth = new GregorianCalendar().GetDaysInMonth(year, month);
			int num = DateTimeHelper.GetWeekOfYear(new DateTime(year, month, 1));
			DateTime dateTime2;
			do
			{
				DateTime dateTime;
				DateTimeHelper.GetWeek(year, num, out dateTime, out dateTime2);
				Week week = new Week();
				week.WeekOfYear = num;
				if (dateTime.Month == month)
				{
					week.WeekOfMonth = DateTimeHelper.GetWeekOfMonth(dateTime);
				}
				else
				{
					week.WeekOfMonth = DateTimeHelper.GetWeekOfMonth(dateTime2);
				}
				week.Monday = dateTime;
				week.Sunday = dateTime2;
				if (now >= dateTime && now <= dateTime2)
				{
					week.CurrentWeek = true;
				}
				list.Add(week);
				num++;
			}
			while (dateTime2.Month == month && dateTime2.Day != daysInMonth);
			return list;
		}

		public static List<Week> GetRecentlyWeekList(int weekOfYear)
		{
			List<Week> list = new List<Week>();
			DateTime now = DateTime.Now;
			int num = weekOfYear;
			if (weekOfYear < 0)
			{
				weekOfYear = DateTimeHelper.GetWeekOfYear(now);
			}
			weekOfYear--;
			for (int i = 0; i <= 2; i++)
			{
				DateTime dateTime;
				DateTime dateTime2;
				DateTimeHelper.GetWeek(now.Year, weekOfYear, out dateTime, out dateTime2);
				Week week = new Week();
				week.WeekOfYear = weekOfYear;
				if (dateTime.Month == now.Month)
				{
					week.WeekOfMonth = DateTimeHelper.GetWeekOfMonth(dateTime);
				}
				else
				{
					week.WeekOfMonth = DateTimeHelper.GetWeekOfMonth(dateTime2);
				}
				week.Monday = dateTime;
				week.Sunday = dateTime2;
				if (num == weekOfYear)
				{
					week.CurrentWeek = true;
				}
				list.Add(week);
				weekOfYear++;
			}
			return list;
		}
	}
}
