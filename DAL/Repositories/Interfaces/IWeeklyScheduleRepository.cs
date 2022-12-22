using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IWeeklyScheduleRepository
    {
        WeeklySchedule Add(WeeklySchedule entity);
        WeeklySchedule Update(WeeklySchedule entity);
        IEnumerable<WeeklySchedule> UpdateRange(IEnumerable<WeeklySchedule> entities);
        WeeklySchedule AddUpdate(WeeklySchedule entity);
        IEnumerable<WeeklySchedule> AddUpdateRange(IEnumerable<WeeklySchedule> entities);
        WeeklySchedule Get(int id);
        IEnumerable<WeeklySchedule> GetDefaultSchedules(int yearId, int week);
        IEnumerable<WeeklySchedule> GetAll(int startYearId, int week);
        IEnumerable<SundayOfTheYear> GetYearList();
        int GetLatestWeek(int yearId);
        DateTime GetFirstDayOfTheYear(int yearId);
        Tuple<DateTime, DateTime> GetStartEndDates(int yearId, int week);
    }
}
