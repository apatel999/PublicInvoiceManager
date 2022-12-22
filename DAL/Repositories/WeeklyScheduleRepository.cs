using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq.Expressions;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class WeeklyScheduleRepository:Repository, IWeeklyScheduleRepository
    {
        protected ILogger log;
        public WeeklyScheduleRepository(string conString, ILogger log) : base(conString, log)
        {
            this.log = log;
        }

        public WeeklySchedule Add(WeeklySchedule entity)
        {
            using (var con = this.Connection)
            {
                var sql = "INSERT INTO routeweeklyschedules (RouteId, DeliveryDate, Week, SundayOfTheYearId) " +
                    "VALUES(@RouteId, @DeliveryDate, @Week, @SundayOfTheYearId); " +
                    "SELECT LAST_INSERT_ID();";
                var insertId = con.Query<int>(sql,entity).FirstOrDefault<int>();
                return this.Get(insertId);
            }
        }
                
        public IEnumerable<WeeklySchedule> GetDefaultSchedules(int yearId, int week)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT rws.Id, rws.RouteId, rws.DeliveryDate, rws.Week, rws.SundayOfTheYearId, dr.RouteNumber, dr.Description " +
                    "FROM ( SELECT * FROM routeweeklyschedules WHERE SundayOfTheYearId = @YearId AND Week = @Week) as rws LEFT JOIN deliveryroutes dr ON rws.RouteId = dr.Id " +
                    " ORDER BY RouteNumber";
                var weeklySchedules = con.Query<WeeklySchedule>(sql, new { YearId = yearId, Week = week }).ToList<WeeklySchedule>();

                var defaultSchedules = this.GetDefaultWeeklySchedules(yearId, week);
                IList<WeeklySchedule> mergedSchedules = new List<WeeklySchedule>();
                
                foreach (var defaultSchedule in defaultSchedules)
                {
                    var temp = weeklySchedules.Find(weeklySchedule => weeklySchedule.RouteId == defaultSchedule.RouteId);
                    var schedule = temp != null? temp : defaultSchedule;
                    mergedSchedules.Add(schedule);
                }

                return mergedSchedules;
            }
        }

        public IEnumerable<WeeklySchedule> GetAll(int yearId, int week)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT rws.Id, rws.RouteId, rws.DeliveryDate, rws.Week, rws.SundayOfTheYearId, dr.RouteNumber, dr.Description " +
                    "FROM routeweeklyschedules as rws JOIN deliveryroutes dr ON rws.RouteId = dr.Id WHERE rws.SundayOfTheYearId = @yearId AND rws.Week = @Week ";
                return con.Query<WeeklySchedule>(sql, new { YearId = yearId, Week = week });
            }
        }



        public IEnumerable<WeeklySchedule> GetDefaultWeeklySchedules(int yearId, int week)
        {
            using (var con = this.Connection)
            {
                var firstDayOfYear = this.GetFirstDayOfTheYear(yearId);

                var routes = con.Query<DeliveryRoute>("SELECT * FROM deliveryroutes");
                IList<WeeklySchedule> weeklySchedules = new List<WeeklySchedule>();

                foreach( var route in routes)
                {
                    var days = week * 7 + route.DeliveryDay;
                    var weeklySchedule = new WeeklySchedule
                    {
                        RouteId = route.Id,
                        RouteNumber = route.RouteNumber,
                        Description = route.Description,
                        SundayOfTheYearId = yearId,
                        Week = week,
                        DeliveryDate = firstDayOfYear.AddDays(days)
                    };
                    weeklySchedules.Add(weeklySchedule);
                }

                return weeklySchedules;
            }
        }

        public DateTime GetFirstDayOfTheYear(int yearId)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT FirstSundayOfTheYear FROM sundayoftheyear WHERE Id=@Id";
                string firstSundayOfTheYear = con.Query<string>(sql, new { Id = yearId }).FirstOrDefault();
                var firstDayOfYear = DateTime.Parse(firstSundayOfTheYear);
                return firstDayOfYear;
            }
        }


        public void Remove(WeeklySchedule entity)
        {
            throw new NotImplementedException();
        }

        public WeeklySchedule Update(WeeklySchedule entity)
        {
            using (var con = this.Connection)
            {
                var sql = "UPDATE routeweeklyschedules  SET RouteId = @RouteId, DeliveryDate = @DeliveryDate," +
                    " Week = @Week, SundayOfTheYearId = @SundayOfTheYearId " +
                    " WHERE Id=@Id";
                con.Execute(sql, entity);
                return this.Get(entity.Id);
            }
        }
     
        public IEnumerable<WeeklySchedule> UpdateRange(IEnumerable<WeeklySchedule> entities)
        {
            throw new NotImplementedException();
        }

        public WeeklySchedule Get(int id)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT rws.Id, rws.RouteId, rws.DeliveryDate, rws.Week, rws.SundayOfTheYearId, dr.RouteNumber, dr.Description " +
                    "FROM routeweeklyschedules rws JOIN deliveryroutes dr ON rws.RouteId = dr.Id " +
                    "WHERE rws.Id=@Id";
                return con.Query<WeeklySchedule>(sql, new {Id = id }).FirstOrDefault();
            }
        }

        public SundayOfTheYear GetYear(int yearId)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT * FROM sundayoftheyear WHERE Id = @Id";
                return con.Query<SundayOfTheYear>(sql,new { Id = yearId}).FirstOrDefault();
            }
        }

        public IEnumerable<SundayOfTheYear> GetYearList()
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT * FROM sundayoftheyear";
                return con.Query<SundayOfTheYear>(sql);
            }
        }

        public int GetLatestWeek(int yearId)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT COALESCE(max(week), 0) FROM routeweeklyschedules WHERE SundayOfTheYearId = @YearId";
                return con.Query<int>(sql, new { YearId= yearId }).Single();
            }
        }

        public WeeklySchedule AddUpdate(WeeklySchedule entity)
        {
            if(entity.Id>0)
            {
                return this.Update(entity);
            }

            return this.Add(entity);
        }

        public IEnumerable<WeeklySchedule> AddUpdateRange(IEnumerable<WeeklySchedule> entities)
        {
            var schedules = new List<WeeklySchedule>();
            foreach(WeeklySchedule entity in entities)
            {
                var resultValue = this.AddUpdate(entity);
                schedules.Add(resultValue);
            }
            return schedules;
        }

        public Tuple<DateTime, DateTime> GetStartEndDates(int yearId, int week)
        {
            DateTime startDay = new DateTime();
            DateTime endDay = new DateTime();

            //startDay = this.GetFirstDayOfTheYear(yearId);
            var year = this.GetYear(yearId);
            this.log.LogInformation("selected year: {@year} ", year);

            if(year !=null)
            {
                startDay = year.FirstSundayOfTheYear;
                
                if (week > 0)
                {
                    startDay = startDay.AddDays(week * 7);
                    endDay = startDay.AddDays(6);
                    this.log.LogInformation("Week > 0 Start End days: {startDay} {endDay} ", startDay, endDay);
                }
                else
                {
                    endDay = year.LastDayOfTheYear;
                }

                this.log.LogInformation("Start End days: {startDay} {endDay} ", startDay, endDay);
            }

            return new Tuple<DateTime, DateTime>(startDay, endDay);
        }
       
    }
}
