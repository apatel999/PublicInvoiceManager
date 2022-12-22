using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Repositories.Interfaces;
using System.Linq.Expressions;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class OrdersRepository :  Repository, IOrdersRepository
    {
        public OrdersRepository(string conString, ILogger log) : base(conString, log)
        { }
        
        public Order Add(Order entity)
        {
            using (var con = this.Connection)
            {
                int startInvoiceNo = 200000;
                string invoiceNumberSql = "SELECT MAX(InvoiceNumber) AS invoiceNumber FROM orders;";
                int invoiceNumber = con.Query<int>(invoiceNumberSql).FirstOrDefault();
                if(invoiceNumber <= startInvoiceNo)
                {
                    invoiceNumber = startInvoiceNo; 
                }

                entity.InvoiceNumber = invoiceNumber + 1;

                //string sql = "INSERT INTO orders (StoreId, CreateDate,UpdateDate,InvoiceDate,TaxAmount, TaxPercent, DiscountPercent, DiscountAmount, SubTotal, Total, OrderStatus, RecordStatus, Note, RouteWeeklyScheduleId , InvoiceNumber )" +
                //"VALUES (@StoreId, NOW(), NOW(), @InvoiceDate,@TaxAmount, @TaxPercent, @DiscountPercent, @DiscountAmount, @SubTotal, @Total, 'ORDER', 'ACT', @Note, @RouteWeeklyScheduleId" +
                //", ((SELECT iNo FROM (SELECT COALESCE(MAX(InvoiceNumber),200000) AS iNo FROM orders) AS sub_iNo) + 1) );" +
                //"SELECT LAST_INSERT_ID(); ";
                string sql = "INSERT INTO orders (StoreId, CreateDate,UpdateDate,InvoiceDate,TaxAmount, TaxPercent, DiscountPercent, DiscountAmount, SubTotal, Total, OrderStatus, RecordStatus, Note, RouteWeeklyScheduleId , InvoiceNumber )" +
                "VALUES (@StoreId, NOW(), NOW(), @InvoiceDate,@TaxAmount, @TaxPercent, @DiscountPercent, @DiscountAmount, @SubTotal, @Total, 'ORDER', 'ACT', @Note, @RouteWeeklyScheduleId" +
                ",@InvoiceNumber );" +
                "SELECT LAST_INSERT_ID(); ";

                int insertId = con.Query<int>(sql, entity).FirstOrDefault();

                if (insertId > 0)
                    return this.Get(insertId);

                return null;
            }
        }

        public void AddRange(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        
        public IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            using(var con = this.Connection)
            {
                return con.Query<Order, Store, BillingInformation, DeliveryRoute, Order>("SELECT o.*, s.*, b.*, r.* FROM orders o JOIN stores s ON o.StoreId = s.Id " +
                    " JOIN billinginformation b ON s.BillingInformationId = b.Id " +
                    " JOIN deliveryroutes r ON s.RouteId = r.Id " +
                    " WHERE o.Id = @Id",
                        (order, store, billingInfo, deliveryRoute) =>
                        {
                            store.BillingInformation = billingInfo;
                            store.Route = deliveryRoute;
                            order.StoreInfo = store;
                            return order;
                        }, new { Id = id }).SingleOrDefault();
            }
        }
        
        public int Count(SearchParam searchParam)
        {
            using (var con = this.Connection)
            {
                var where = this.GetAllWhereClause(searchParam);

                var sql = "SELECT COUNT(DISTINCT o.Id) c FROM  orders AS o " +
               " JOIN stores s ON o.StoreId = s.Id " +
               " JOIN billinginformation b ON s.BillingInformationId = b.Id  " +
               " JOIN deliveryroutes r ON s.RouteId = r.Id ";

                sql += !string.IsNullOrEmpty(where) ?  where : "";

                return con.Query<int>(sql).FirstOrDefault();
            }
        }

        public IEnumerable<int> GetAllOrderIds(SearchParam searchParam)
        {
            var where = GetAllWhereClause(searchParam);

            var sql = "SELECT o.Id FROM  orders AS o " +
                " JOIN stores s ON o.StoreId = s.Id " +
                " JOIN billinginformation b ON s.BillingInformationId = b.Id  " +
                " JOIN deliveryroutes r ON s.RouteId = r.Id " +
                " LEFT JOIN routeweeklyschedules rws ON o.RouteWeeklyScheduleId = rws.Id ";

            sql += !string.IsNullOrEmpty(where) ? where : "";

            sql += " ORDER BY  r.RouteNumber ASC, s.RouteOrderNo ASC, o.InvoiceDate DESC";

            this.log.LogInformation("GetAll orders Ids SQL: {sql} ", sql);
            using (var con = this.Connection)
            {
                return con.Query<int>(sql);
            }
        }

        public IEnumerable<Order> GetAll(SearchParam searchParam = null)
        {
            int offset = searchParam.Page * AppConstants.RowsPerPage;

            var where = GetAllWhereClause(searchParam);

            var sql = "SELECT o.*, s.*, b.*, r.* , rws.* FROM  orders AS o " +
                " JOIN stores s ON o.StoreId = s.Id " +
                " JOIN billinginformation b ON s.BillingInformationId = b.Id  " +
                " JOIN deliveryroutes r ON s.RouteId = r.Id " +
                " LEFT JOIN routeweeklyschedules rws ON o.RouteWeeklyScheduleId = rws.Id ";

            sql += !string.IsNullOrEmpty(where) ? where : "";

            sql += " ORDER BY  r.RouteNumber ASC, s.RouteOrderNo ASC, o.InvoiceDate DESC";
            sql += " LIMIT @offset," + AppConstants.RowsPerPage;
            this.log.LogInformation("GetAll orders SQL: {sql} ", sql);
            using (var con = this.Connection)
            {
                return con.Query<Order, Store, BillingInformation, DeliveryRoute, WeeklySchedule, Order>(sql,
                        (order, store, billingInfo, deliveryRoute, weeklySchedule) =>
                        {
                            store.BillingInformation = billingInfo;
                            store.Route = deliveryRoute;
                            order.StoreInfo = store;
                            order.Week = weeklySchedule!=null? weeklySchedule.Week :0;
                            return order;
                        }, new { offset = offset });
            }
        }

        private string GetAllWhereClause(SearchParam searchParam = null)
        {
            var where = " o.RecordStatus ='ACT' ";
            if (searchParam.OrderStatus != null && searchParam.OrderStatus.Count > 0)
            {
                var status = string.Join("', '", searchParam.OrderStatus.ToArray());
                where += !string.IsNullOrEmpty(where) ? " AND " : "";
                where += " o.OrderStatus IN ( '" + status.ToUpper() + "') ";
            }
            if (searchParam != null && searchParam.InvoiceNumber > 0)
            {
                where += !string.IsNullOrEmpty(where) ? " AND " : "";
                where += " o.InvoiceNumber ='" + searchParam.InvoiceNumber + "' ";
            }

            if (searchParam != null && !string.IsNullOrEmpty(searchParam.CustomerName))
            {
                where += !string.IsNullOrEmpty(where) ? " AND " : "";
                where += " b.Name LIKE '%" + searchParam.CustomerName + "%' ";
            }
            if (searchParam != null && searchParam.BillingInformationId > 0)
            {
                where += !string.IsNullOrEmpty(where) ? " AND " : "";
                where += " b.Id = '" + searchParam.BillingInformationId + "' ";
            }

            if (searchParam != null && searchParam.StoreId > 0)
            {
                where += !string.IsNullOrEmpty(where) ? " AND " : "";
                where += " o.StoreId = '" + searchParam.StoreId + "' ";
            }

            if (searchParam != null && !string.IsNullOrEmpty(searchParam.Route))
            {
                where += !string.IsNullOrEmpty(where) ? " AND " : "";
                where += " CONCAT(r.RouteNumber ,'.', COALESCE(s.RouteOrderNo,'')) LIKE '" + searchParam.Route + "%' ";
            }

            if (searchParam != null && searchParam.StartDate !=null)
            {
                where += !string.IsNullOrEmpty(where) ? " AND " : "";
                where += " o.InvoiceDate >= '" + searchParam.StartDate + "' ";
            }
            if (searchParam != null && searchParam.EndDate != null && (searchParam.EndDate > searchParam.StartDate))
            {
                where += !string.IsNullOrEmpty(where) ? " AND " : "";
                where += " o.InvoiceDate <= '" + searchParam.EndDate + "' ";
            }

            where = !string.IsNullOrEmpty(where) ? " WHERE " + where : "";

            return where;
        }
        
        public Order GetSingleOrDefault(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(Order entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order entity)
        {
            using (var con = this.Connection)
            {
                string sql = "UPDATE orders SET OrderStatus=@OrderStatus, UpdateDate= now(), TaxAmount=@TaxAmount, TaxPercent=@TaxPercent, DiscountPercent=@DiscountPercent" +
                    ", DiscountAmount=@DiscountAmount, SubTotal=@SubTotal, Total=@Total, Note=@Note ";

                //record changed form ORDER to INVOICE
                if (entity.InvoiceNumber <= 0 && entity.OrderStatus == "INVOICE")
                {
                //    sql += ", InvoiceNumber= ((SELECT iNo FROM (SELECT MAX(InvoiceNumber) AS iNo FROM Orders) AS sub_iNo) + 1) ";
                }

                sql += " WHERE Id=@Id ";
                con.Execute(sql, entity);

                return entity;
            }
        }
        

        public IEnumerable<Order> UpdateRange(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        
    }
}
