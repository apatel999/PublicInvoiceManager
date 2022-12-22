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
    public class DeliveryRoutesRepository : Repository, IDeliveryRouteRepository
    {
        public DeliveryRoutesRepository(string conString, ILogger log) : base(conString, log)
        {

        }
        public DeliveryRoute Add(DeliveryRoute entity)
        {
            using (var con = this.Connection)
            {
                return con.Query<DeliveryRoute>("INSERT INTO deliveryroutes (RouteNumber, Description, DeliveryDay, ProductionDay, RecordCreateDate, RecordUpdateDate) VALUES (@RouteNumber, @Description, @DeliveryDay, @ProductionDay, now(),now());" +
                    " SELECT * FROM deliveryroutes WHERE LAST_INSERT_ID() > 0 AND Id = LAST_INSERT_ID();", entity).FirstOrDefault();
            }
        }

        public void AddRange(IEnumerable<DeliveryRoute> entities)
        {
            throw new NotImplementedException();
        }

        public int Count(SearchParam searchParam)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveryRoute> Find(Expression<Func<DeliveryRoute, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public DeliveryRoute Get(int id)
        {
            using (var con = this.Connection)
            {
                return con.Query<DeliveryRoute>("SELECT * FROM deliveryroutes WHERE Id = @Id", new {Id=id})
                            .SingleOrDefault<DeliveryRoute>();
            }
        }

        public IEnumerable<DeliveryRoute> GetAll(SearchParam searchParam)
        {
            using (var con = this.Connection)
            {
                return con.Query<DeliveryRoute>("SELECT * FROM deliveryroutes order by RouteNumber");
            }
        }
        
        public DeliveryRoute GetSingleOrDefault(Expression<Func<DeliveryRoute, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(DeliveryRoute entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<DeliveryRoute> entities)
        {
            throw new NotImplementedException();
        }

        public DeliveryRoute Update(DeliveryRoute entity)
        {
            using (var con = this.Connection)
            {
                return con.Query<DeliveryRoute>("UPDATE deliveryroutes SET RouteNumber=@RouteNumber, Description=@Description, DeliveryDay=@DeliveryDay, ProductionDay=@ProductionDay, RecordUpdateDate = now() WHERE Id=@Id;" +
                     " SELECT * FROM deliveryroutes WHERE Id=@Id;", entity).FirstOrDefault(); ;
            }
        }

        public IEnumerable<DeliveryRoute> UpdateRange(IEnumerable<DeliveryRoute> entities)
        {
            var routes = new List<DeliveryRoute>();
            using (var con = this.Connection)
            {
                foreach(var entity in entities)
                {
                    var route = con.Query<DeliveryRoute>("UPDATE deliveryroutes SET RouteNumber=@RouteNumber, Description=@Description, DeliveryDay=@DeliveryDay, ProductionDay=@ProductionDay, RecordUpdateDate = now() WHERE Id=@Id;" +
                     " SELECT * FROM deliveryroutes WHERE Id=@Id;", entity).FirstOrDefault();

                    routes.Add(route);
                }
            }

            return routes;
        }
    }
}
