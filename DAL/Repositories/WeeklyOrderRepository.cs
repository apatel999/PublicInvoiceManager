using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class WeeklyOrderRepository : Repository, IWeeklyOrderRepository
    {
        public WeeklyOrderRepository(string conString, ILogger log) : base(conString, log)
        {
        }
        public WeeklyOrderItem Add(WeeklyOrderItem entity)
        {
            using (var con = this.Connection)
            {
                return con.Query<WeeklyOrderItem, Product, WeeklyOrderItem>("INSERT INTO storeweeklyorders (StoreId, ProductId, Quantity, RecordStatus ) VALUES (@StoreId, @ProductId, @Quantity,'ACT');" +
                    "SELECT o.*, p.* FROM storeweeklyorders o JOIN products p ON o.ProductId = p.Id" +
                    " WHERE LAST_INSERT_ID() > 0 AND o.Id = LAST_INSERT_ID();",
                    (orderItem, product) =>
                    {
                        orderItem.Product = product;
                        return orderItem;
                    }, entity).FirstOrDefault();
            }
        }

        public void AddRange(IEnumerable<WeeklyOrderItem> entities)
        {
            throw new NotImplementedException();
        }

        public int Count(SearchParam searchParam)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<WeeklyOrderItem> Find(Expression<Func<WeeklyOrderItem, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public WeeklyOrderItem Get(int id)
        {
            using (var con = this.Connection)
            {
                return con.Query<WeeklyOrderItem, Product, WeeklyOrderItem>("SELECT o.*, p.* FROM storeweeklyorders o JOIN products p ON o.ProductId = p.Id" +
                    " WHERE o.RecordStatus = 'ACT' AND p.RecordStatus = 'ACT' AND  o.Id=@Id",
                    (orderItem, product) =>
                    {
                        orderItem.Product = product;
                        return orderItem;
                    },new { Id = id }).SingleOrDefault();
            }
        }

        public IEnumerable<WeeklyOrderItem> GetAll(SearchParam searchParam=null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Store> GetStoreListForWeeklyOrders(int yearId, int week)
        {
            using (var con = this.Connection)
            {
                var sql = @"SELECT s.* FROM storeweeklyorders wo 
                JOIN products p ON wo.ProductId = p.Id 
                JOIN stores s ON wo.StoreId = s.Id 
                JOIN deliveryroutes d ON s.RouteId = d.Id 
                JOIN routeweeklyschedules ws ON ws.RouteId = s.RouteId 
                WHERE wo.RecordStatus = 'ACT'  AND p.RecordStatus = 'ACT'  AND s.Status = 'ACT' 
                AND ws.SundayOfTheYearId = @SundayOfTheYearId AND ws.Week = @Week 
                AND s.Id NOT IN(SELECT StoreId FROM orders o WHERE o.StoreId = s.Id and o.routeweeklyscheduleId = ws.Id) 
                GROUP BY s.Id 
                ORDER BY d.RouteNumber, s.RouteOrderNo";

                return con.Query<Store>(sql, new { SundayOfTheYearId= yearId, Week = week });

                ;
            }
        }


        public IEnumerable<WeeklyOrderItem> GetOrderItems(int storeId)
        {
            using (var con = this.Connection)
            {
                return con.Query<WeeklyOrderItem, Product, WeeklyOrderItem>("SELECT o.*,p.* FROM storeweeklyorders o JOIN products p ON o.ProductId = p.Id" +
                    " WHERE o.RecordStatus = 'ACT'  AND p.RecordStatus = 'ACT'  AND o.StoreId=@StoreId",
                    (orderItem, product) =>
                    {
                        orderItem.Product = product;
                        return orderItem;
                    },new { StoreId = storeId } );
            }
        }
        

        public WeeklyOrderItem GetSingleOrDefault(Expression<Func<WeeklyOrderItem, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(WeeklyOrderItem entity)
        {
            using (var con = this.Connection)
            {
                con.Execute("UPDATE storeweeklyorders SET RecordStatus='DEL' WHERE StoreId=@StoreId AND Id=@Id;", entity);
            }
        }

        public void RemoveRange(IEnumerable<WeeklyOrderItem> entities)
        {
            throw new NotImplementedException();
        }

        public WeeklyOrderItem Update(WeeklyOrderItem entity)
        {
            using (var con = this.Connection)
            {
                con.Execute("UPDATE storeweeklyorders SET Quantity=@Quantity WHERE Id=@Id;", entity);
                return this.Get(entity.Id);
            }
        }

        public IEnumerable<WeeklyOrderItem> UpdateRange(IEnumerable<WeeklyOrderItem> entities)
        {
            throw new NotImplementedException();
        }
   
    }


}
