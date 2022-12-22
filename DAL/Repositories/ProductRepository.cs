// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class ProductRepository : Repository, IRepository<Product>
    {
        public ProductRepository(string conString, ILogger log) : base(conString, log)
        { }

        public Product Add(Product entity)
        {
            using(var con= this.Connection)
            {
               return con.Query<Product>("INSERT INTO products (Productcode, Name, Description, Price, CreateDate, UpdateDate ) VALUES (@Productcode, @Name, @Description, @Price, now(), now());" +
                   " SELECT Id, Productcode, Name, Description, Price, RecordStatus, CreateDate, UpdateDate FROM products WHERE LAST_INSERT_ID() > 0 AND Id = LAST_INSERT_ID();", entity).FirstOrDefault();
            }
        }

        public void AddRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public int Count(SearchParam searchParam)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            using (var con = this.Connection)
            {
                return con.Query<Product>("SELECT Id, Productcode, Name, Description, Price, RecordStatus, CreateDate, UpdateDate FROM products WHERE Id=@Id", new { Id = id })
                        .SingleOrDefault<Product>();
            }
        }

        public IEnumerable<Product> GetAll(SearchParam searchParam=null)
        {
            using (var con = this.Connection)
            {
                return con.Query<Product>("SELECT Id, Productcode, Name, Description, Price, RecordStatus, CreateDate, UpdateDate FROM products");
            }
        }

        public Product GetSingleOrDefault(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product entity)
        {
            using (var con = this.Connection)
            {
                con.Execute("UPDATE products SET Productcode=@Productcode, Name=@Name, Description=@Description, Price=@Price, RecordStatus= @RecordStatus, UpdateDate=now() WHERE Id=@Id;", entity) ;
                return this.Get(entity.Id);
            }
        }

        public IEnumerable<Product> UpdateRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }
    }
}
