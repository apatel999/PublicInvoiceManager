// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using DAL.Models;
using DAL.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class BillingInformationRepository : Repository, IBillingInformationRepository
    {
        private InvoicingYearRepository invoicingYearRepo;
        private ILogger _log;
        public BillingInformationRepository(string conString, ILogger log) : base(conString, log)
        {
            this._log = log;
            this.invoicingYearRepo = new InvoicingYearRepository(conString, this._log);
        }

        public BillingInformation Add(BillingInformation entity)
        {
            using (var con = this.Connection)
            {
                return con.Query<BillingInformation>("INSERT INTO billinginformation(Name, BillingGroupId, PaymentType, PercentDiscount, Note, HstNumber, PstExempt, Status, Address1, Address2, City, State, Country, PostalCode, ContactPerson, Phone, Fax, DateCreated, DateModified)" +
                "VALUES(@Name, @BillingGroupId, @PaymentType, @PercentDiscount, @Note, @HstNumber,@PstExempt, @Status, @Address1, @Address2, @City, @State, @Country, @PostalCode, @ContactPerson, @Phone, @Fax, now(), now() ); " +
                "SELECT Id, Name, BillingGroupId, PaymentType, Note, HstNumber, PstExempt, Status, Address1, Address2, City, State, Country, PostalCode, ContactPerson, Phone, Fax, DateCreated, DateModified FROM billinginformation " +
                " WHERE LAST_INSERT_ID() > 0 AND Id = LAST_INSERT_ID(); ", entity).FirstOrDefault();
            }
        }
        

        public int Count(SearchParam searchParam)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT  COUNT(DISTINCT b.ID) c " +
                           " FROM billinginformation b LEFT JOIN stores s ON b.Id= s.billinginformationId " +
                           " LEFT JOIN deliveryroutes d ON s.RouteId = d.Id ";

                var where = "";

                if (!string.IsNullOrEmpty(searchParam.CustomerName))
                {
                    where += !string.IsNullOrEmpty(where) ? " OR " : "";
                    where += " b.Name LIKE '%" + searchParam.CustomerName + "%' ";
                }
                if (!string.IsNullOrEmpty(searchParam.Route))
                {
                    where += !string.IsNullOrEmpty(where) ? " OR " : "";
                    //where += " d.RouteNumber = " + searchParam.Route;
                    where += " CONCAT(d.RouteNumber ,'.', s.RouteOrderNo) LIKE '" + searchParam.Route + "%' ";
                }

                if (!searchParam.AllRecords)
                {
                    where += !string.IsNullOrEmpty(where) ? "( " + where + ") AND ( b.Status = 'ACT' AND s.Status = 'ACT' )" : " ( b.Status = 'ACT' AND s.Status = 'ACT' )";
                }

                sql += !string.IsNullOrEmpty(where) ? " WHERE " + where : "";

                return con.Query<int>(sql).FirstOrDefault();
            }
        }

        
        public BillingInformation Get(int id)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT b.*, s.*, d.* " +
                            "FROM billinginformation b LEFT JOIN stores s ON b.Id= s.BillingInformationId " +
                            " LEFT JOIN deliveryroutes d ON s.RouteId = d.Id " +
                            " WHERE b.Id=@Id ;";
                var dictionary = new Dictionary<int, BillingInformation>();
                con.Query<BillingInformation, Store, DeliveryRoute, BillingInformation>(sql, (bi, store, route) =>
                {
                    BillingInformation billingInfo;
                    if (!dictionary.TryGetValue(bi.Id, out billingInfo))
                    {
                        dictionary.Add(bi.Id, billingInfo = bi);
                    }

                    if (store != null)
                    {
                        store.Route = route;
                        billingInfo.Stores.Add(store);
                    }


                    return billingInfo;
                }, new { Id = id });
                
                BillingInformation returnValue;
                dictionary.TryGetValue(id, out returnValue);

                Tuple<decimal, decimal> ytdLtd = this.GetYtdLtd(id);
                returnValue.Ytd = ytdLtd.Item1;
                returnValue.Ltd = ytdLtd.Item2;

                return returnValue;
            }
        }

       public Tuple<decimal, decimal> GetYtdLtd(int billingInformationId)
       {
            var currentYear = this.invoicingYearRepo.GetCurrentYear();
            var ytd = this.GetYtd(billingInformationId, currentYear);

            var lastYear = this.invoicingYearRepo.GetCurrentYear(-1);
            var lYtd = this.GetYtd(billingInformationId, lastYear);

            return new Tuple<decimal, decimal>(ytd, lYtd);
       }

       public decimal GetYtd(int billingInformationId, SundayOfTheYear year)
       {
            if(year == null)
            {
                return 0;
            }

            using(var con = this.Connection)
            {
                var sql = "SELECT SUM(Total) FROM orders o" +
                            " JOIN stores s ON o.StoreId = s.Id" +
                            " JOIN billinginformation b ON s.BillingInformationId = b.Id" +
                            " WHERE" +
                            " RecordStatus = 'ACT'" +
                            " AND OrderStatus = 'INVOICE' " +
                            " AND InvoiceDate >= '"+ year.FirstSundayOfTheYear.ToString("yyyy-MM-dd") +"'" +
                            " AND InvoiceDate <= '" + year.LastDayOfTheYear.ToString("yyyy-MM-dd") + "'" +
                            " AND b.Id = @BillingInformationId";

                var result = con.Query<decimal?>(sql, new
                {
                    BillingInformationId = billingInformationId
                }).FirstOrDefault(); 

                if(result != null)
                {
                    return (decimal)result;
                }

                return 0;
            }

       }

        
        
        public IEnumerable<BillingInformation> GetAll(SearchParam searchParam=null)
        {
            int offset = searchParam.Page * AppConstants.RowsPerPage;

            using (var con = this.Connection)
            {

                var sql = " SELECT b.Id FROM billinginformation b LEFT JOIN stores s ON b.Id = s.BillingInformationId LEFT JOIN deliveryroutes d ON s.RouteId = d.Id ";
                
                var where = "";

                if (searchParam != null && !string.IsNullOrEmpty(searchParam.CustomerName))
                {
                    where += !string.IsNullOrEmpty(where) ? " OR " : "";
                    where += " b.Name LIKE '%" + searchParam.CustomerName + "%' ";
                }
                if (searchParam != null && !string.IsNullOrEmpty( searchParam.Route))
                {
                    where += !string.IsNullOrEmpty(where) ? " OR " : "";
                   // where += " d.RouteNumber = '" + searchParam.Route + "' ";
                    where += " CONCAT(d.RouteNumber ,'.', s.RouteOrderNo) LIKE '" + searchParam.Route + "%' ";
                }

                if(!searchParam.AllRecords)
                {
                    where += !string.IsNullOrEmpty(where) ? "( " + where + ") AND ( b.Status = 'ACT' AND s.Status = 'ACT' )" : " ( b.Status = 'ACT' AND s.Status = 'ACT' )";
                }

                sql += !string.IsNullOrEmpty(where) ? " WHERE " + where : "";

                sql += " GROUP BY b.Id LIMIT " + offset + ", " + AppConstants.RowsPerPage;

                var records = con.Query<int>(sql);
                string ids = string.Join(",", records);

                var dictionary = new Dictionary<int, BillingInformation>();
                if (!string.IsNullOrEmpty(ids))
                {
                    var sql2 = "SELECT b.*, s.*, d.* " +
                            " From billinginformation AS b LEFT JOIN stores s ON b.Id= s.BillingInformationId " +
                            " LEFT JOIN deliveryroutes d ON s.RouteId = d.Id " +
                            " WHERE b.Id IN(" + ids + ")" +
                            " Order by b.Id, RouteNumber, RouteOrderNo ";
                            //" GROUP BY b.Id";
                    con.Query<BillingInformation, Store, DeliveryRoute, BillingInformation>(sql2, (bi, store, route) =>
                    {
                        BillingInformation billingInfo;
                        if (!dictionary.TryGetValue(bi.Id, out billingInfo))
                        {
                            dictionary.Add(bi.Id, billingInfo = bi);
                        }

                        if (store != null)
                        {
                            store.Route = route;
                            billingInfo.Stores.Add(store);

                        }

                        return billingInfo;
                    });
                }

                return dictionary.Values;
            }

        }



        public BillingInformation Update(BillingInformation entity)
        {
            using(var con= this.Connection)
            {
                con.Execute("UPDATE billinginformation SET Name=@Name, BillingGroupId=@BillingGroupId, PaymentType=@PaymentType, PercentDiscount =@PercentDiscount , Note=@Note, HstNumber=@HstNumber, PstExempt=@PstExempt, " +
                "Status=@Status, Address1=@Address1, Address2=@Address2, City=@City, State=@State, Country=@Country, PostalCode=@PostalCode, ContactPerson=@ContactPerson, Phone=@Phone," +
                " Fax=@Fax, DateModified = now() WHERE Id=@Id;", entity);

                return this.Get(entity.Id);
            }
            
        }

        public BillingStatement GetWeeklyBillingStatement(int billingInformationId, int yearId, int week)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT CONVERT(s.StoreNumber, SIGNED) AS storeIntNumber, s.StoreNumber, InvoiceNumber, o.CreateDate, SubTotal, TaxAmount, Total " +
                    " FROM orders o" +
                    " JOIN stores s ON o.StoreId = s.Id" +
                    " JOIN billinginformation b ON s.BillingInformationId = b.Id" +
                    " JOIN routeweeklyschedules ws ON  o.RouteWeeklyScheduleId = ws.Id" +
                    " WHERE " +
                    " b.Status = 'ACT' AND s.Status = 'ACT' " +

                    " AND (" +
                    "   ((b.BillingGroupId IS NOT NULL OR b.BillingGroupId <> '') AND " +
                    "   b.BillingGroupId = (SELECT BillingGroupId FROM billinginformation WHERE Id = @BillingInformationId)) " +
                    "   OR b.Id = @BillingInformationId " +
                    ") " +
                    " AND SundayOfTheYearId = @YearId AND ws.Week = @Week" +
                    " ORDER BY storeIntNumber ";

                var result = con.Query<BillingStatementItem>(sql, new
                {
                    BillingInformationId = billingInformationId,
                    YearId = yearId,
                    Week = week
                });

                var sundayOfTheYear = this.invoicingYearRepo.Get(yearId);
                BillingStatement statement = new BillingStatement();
                statement.StatementItems = result;
                statement.Week = week;
                statement.Year = sundayOfTheYear.FirstSundayOfTheYear;

                return statement;
            }

        }
    }
}
