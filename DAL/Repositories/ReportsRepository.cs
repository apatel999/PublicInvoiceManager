using DAL.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ReportsRepository: Repository
    {
        private WeeklyScheduleRepository _weeklyScheduleRepository;
        public ReportsRepository(string conString, ILogger log) : base(conString, log)
        {
            this._weeklyScheduleRepository = new WeeklyScheduleRepository(conString, log);
        }
        

        public IEnumerable<ProductRouteOrder> GetProductionSchedule(int yearId, int week)
        {
            string sql = "SELECT p.Id as ProductId, r.Id as RouteId,  count(*) as Total FROM orders o" +
                " JOIN orderitems oi ON o.Id = oi.OrderId " +
                " JOIN products p ON oi.ProductId = p.Id " +
                " JOIN routeweeklyschedules ws ON o.RouteWeeklyScheduleId = ws.Id " +
                " JOIN deliveryroutes r ON ws.RouteId = r.Id " +
                " WHERE ws.SundayOfTheYearId = @YearId AND ws.Week = @Week " +
                " GROUP BY r.Id, p.Id;";
            using(var con = this.Connection)
            {
                var data = con.Query<ProductRouteOrder>(sql, new {YearId= yearId, Week = week });
                return data;
            }
        }

        
        public IEnumerable<Order> GetOrders(SearchParam searchParam)
        {
            
            var days = this._weeklyScheduleRepository.GetStartEndDates(searchParam.YearId, searchParam.Week);

            string sql = @"SELECT o.*, s.*, b.* , r.* , rws.*, oi.* FROM  orders AS o  
                            JOIN orderitems as oi ON o.Id = oi.OrderId
                            JOIN stores s ON o.StoreId = s.Id
                            JOIN billinginformation b ON s.BillingInformationId = b.Id
                            JOIN deliveryroutes r ON s.RouteId = r.Id
                            JOIN routeweeklyschedules rws ON o.RouteWeeklyScheduleId = rws.Id
                            WHERE o.RecordStatus = 'ACT'
                            AND o.OrderStatus = 'INVOICE'";

            if (days != null)
            {
                sql += " AND o.InvoiceDate>='" + days.Item1.ToShortDateString() + "' ";
                sql += " AND o.InvoiceDate<='" + days.Item2.ToShortDateString() + "' ";
            }
            if (!string.IsNullOrEmpty(searchParam.Route))
            {
                sql += " AND r.RouteNumber LIKE '" + searchParam.Route + "' ";
            }

            if (!string.IsNullOrEmpty(sql))
            {
                sql += " ORDER BY r.RouteNumber, s.StoreNumber ";
            }

            using (var con = this.Connection)
            {
                var orders = new Dictionary<int, Order>();

                var list = con.Query<Order, Store, BillingInformation, DeliveryRoute, WeeklySchedule, OrderItem, Order>(sql,
                        (order, store, billingInfo, deliveryRoute, weeklySchedule, orderItem) =>
                        {
                            Order orderEntry;

                            if(!orders.TryGetValue(order.Id, out orderEntry))
                            {
                                orderEntry = order;
                                store.BillingInformation = billingInfo;
                                store.Route = deliveryRoute;
                                orderEntry.StoreInfo = store;
                                orderEntry.Week = weeklySchedule != null ? weeklySchedule.Week : 0;
                                orderEntry.OrderItems = new List<OrderItem>();
                                orders.Add(orderEntry.Id, orderEntry);
                            }

                            orderEntry.OrderItems.AsList<OrderItem>().Add(orderItem);
                            return orderEntry;

                        });

                return orders.Values.AsList<Order>();
            }
        }

        public IEnumerable<StoreProductYtdUnitRepoModel> YearToDateCustomerUnits(SearchParam searchParam)
        {
            var year = this._weeklyScheduleRepository.GetYear(searchParam.YearId);
            var days = this._weeklyScheduleRepository.GetStartEndDates(searchParam.YearId, searchParam.Week);
            var sql = @"SELECT s.*, 
                        b.*,
                        r.*, 
                        p.Id as ProductId, p.Name, P.ProductCode, p.Description, p.Price, p.RecordStatus,
                        SUM(oi.ItemsOrdered) Ins, SUM(oi.ItemsReturned) Outs, SUM(oi.ItemsSold) Sold 
                        
                        FROM  orders AS o                             
                        JOIN orderitems as oi ON o.Id = oi.OrderId                           
                        JOIN stores s ON o.StoreId = s.Id                          
                        JOIN billinginformation b ON s.BillingInformationId = b.Id                          
                        JOIN deliveryroutes r ON s.RouteId = r.Id                           
                        JOIN routeweeklyschedules rws ON o.RouteWeeklyScheduleId = rws.Id
                        JOIN products p ON oi.ProductId = p.Id                         
                        WHERE o.RecordStatus = 'ACT'  
                        AND o.OrderStatus = 'INVOICE'
                        AND p.RecordStatus = 'ACT'";


            if (days != null)
            {
                sql += " AND o.InvoiceDate>='" + year.FirstSundayOfTheYear.ToShortDateString() + "' ";
                sql += " AND o.InvoiceDate<='" + days.Item2.ToShortDateString() + "' ";
            }
            if (!string.IsNullOrEmpty(searchParam.Route))
            {
                sql += " AND r.RouteNumber LIKE '" + searchParam.Route + "' ";
            }

            if (!string.IsNullOrEmpty(sql))
            {
                sql += @"   GROUP BY o.StoreId, oi.ProductId
                            ORDER BY StoreNumber, ProductCode";
            }

            using (var con = this.Connection)
            {
                var list = con.Query<Store, BillingInformation, DeliveryRoute, Product, StoreProductYtdUnitRepoModel, StoreProductYtdUnitRepoModel>(sql,
                        (store,billingInformaiton, deliveryRoute, product, productUnit) =>
                        {
                            productUnit.Store = store;
                            productUnit.Store.BillingInformation = billingInformaiton;
                            productUnit.Store.Route = deliveryRoute;
                            productUnit.Product = product;
                            return productUnit;

                        },
                        splitOn: "Id,Id,ProductId, Ins");

                return list; 
            }
        }
    }
}
