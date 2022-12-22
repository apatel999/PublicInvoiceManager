using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class InvoicingYearRepository: Repository
    {
        public InvoicingYearRepository(string conString, ILogger log) : base(conString, log)
        {
           
        }

        public SundayOfTheYear Get(int yearId)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT * FROM sundayoftheyear WHERE Id =@YearId";
                return con.Query<SundayOfTheYear>(sql, new { YearId = yearId }).FirstOrDefault();
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

        public SundayOfTheYear GetCurrentYear(int yearLapse = 0)
        {
            IEnumerable<SundayOfTheYear> years = this.GetYearList();
            int currentYearIndex = 0;
            for (int i = 0; i < years.Count(); i++)
            {
                if (years.ElementAt(i).IsCurrentYear)
                {
                    currentYearIndex = i;
                }

            }

            return years.ElementAtOrDefault(currentYearIndex + yearLapse);
        }
    }
}
