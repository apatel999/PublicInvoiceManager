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
    public class OrderItemsRepository : Repository, IOrderItemsRepository
    {
        public OrderItemsRepository(string conString, ILogger log) : base(conString, log)
        {
            
        }
        public OrderItem Add(OrderItem entity)
        {
            
            using (var con = this.Connection)
            {
                string sql = "INSERT INTO orderitems (OrderId, ProductId, ItemsOrdered, ItemsReturned, ItemsSold, ItemPrice, DiscountPrice, CreateDate, UpdateDate, RecordStatus, SubTotal)" +
                "VALUES (@OrderId, @ProductId, @ItemsOrdered, @ItemsReturned, @ItemsSold, @ItemPrice, @DiscountPrice, NOW(), NOW(), 'ACT', @SubTotal);" +
                "SELECT LAST_INSERT_ID(); ";
                int insertId = con.Query<int>(sql, entity).FirstOrDefault();

                if (insertId > 0)
                    return this.Get(insertId);

                return null;
            }
            
        }

        public void AddRange(IEnumerable<OrderItem> entities)
        {
            throw new NotImplementedException();
        }

        public int Count(SearchParam searchParam)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> Find(Expression<Func<OrderItem, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public OrderItem Get(int id)
        {
            using (var con = this.Connection)
            {
                return con.Query<OrderItem>("SELECT oi.*, p.Price as RegularPrice, p.Name as ProductName FROM orderitems oi JOIN products p ON oi.ProductId = p.Id WHERE oi.Id =@Id ", 
                         new { Id = id }).SingleOrDefault();
            }
        }

        public IEnumerable<OrderItem> GetAll(SearchParam searchParam=null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAllOrderItems(int orderId)
        {
            using (var con = this.Connection)
            {
                return con.Query<OrderItem>("SELECT oi.*, p.Price as RegularPrice, p.Name as ProductName FROM orderitems oi JOIN products p ON oi.ProductId = p.Id " +
                    " WHERE oi.RecordStatus = 'ACT' AND oi.OrderId = @OrderId ", new { OrderId = orderId }).AsList(); ;
            }
        }

        public OrderItem GetSingleOrDefault(Expression<Func<OrderItem, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<OrderItem> entities)
        {
            throw new NotImplementedException();
        }

        public OrderItem Update(OrderItem entity)
        {
            using (var con = this.Connection)
            {
                string sql = "UPDATE orderitems SET ProductId=@ProductId, ItemsOrdered = @ItemsOrdered, ItemsReturned = @ItemsReturned, ItemsSold=@ItemsSold, UpdateDate= now() " +
                    ", RecordStatus = @RecordStatus, SubTotal = @SubTotal  WHERE OrderId=@OrderId AND Id=@Id" ;
                con.Execute(sql, entity);
                return this.Get(entity.Id);
            }
        }

        public IEnumerable<OrderItem> UpdateRange(IEnumerable<OrderItem> entities)
        {
            throw new NotImplementedException();
        }
    }
}
