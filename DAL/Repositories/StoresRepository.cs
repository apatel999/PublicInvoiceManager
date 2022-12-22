using DAL.Models;
using DAL.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class StoresRepository : Repository, IStoresRepository
    {
        public StoresRepository(string conString, ILogger log) : base(conString, log)
        {
        }
        public Store Add(Store entity)
        {
            using (var con = this.Connection)
            {
                var store = con.Query<Store>("INSERT INTO stores(RouteId, RouteOrderNo, StoreNumber, BillingInformationId, Status, Address1, Address2, City, State, Country, PostalCode, ContactPerson, Phone, Fax, DateCreated, DateModified)" +
                "VALUES(@RouteId, @RouteOrderNo, @StoreNumber, @BillingInformationId, @Status, @Address1, @Address2, @City, @State, @Country, @PostalCode, @ContactPerson, @Phone, @Fax, now(), now() ); " +
                "SELECT Id, RouteId, RouteOrderNo, StoreNumber, BillingInformationId, Status, Address1, Address2, City, State, Country, PostalCode, ContactPerson, Phone, Fax, DateCreated, DateModified FROM stores " +
                " WHERE LAST_INSERT_ID() > 0 AND Id = LAST_INSERT_ID(); ", entity).FirstOrDefault();

                return this.Get(store.Id);
            }
        }

    
        public Store Get(int id)
        {
            using (var con = this.Connection)
            {
                var sql = "SELECT s.*, b.*, d.* FROM stores s " +
                    " JOIN billinginformation b ON s.BillingInformationId = b.Id " +
                    " LEFT JOIN deliveryroutes d  ON s.RouteId = d.Id " +
                    " WHERE s.Id=@Id ;";

                return con.Query<Store, BillingInformation, DeliveryRoute, Store>(sql,
                    (store, billingInfo, deliveryRoute) => {
                        store.BillingInformation = billingInfo;
                        store.Route = deliveryRoute;
                        return store;
                    },    
                    new { Id = id })
                    .SingleOrDefault<Store>();

            }
        }

        public int Count(SearchParam searchParam)
        {
            using (var con = this.Connection)
            {
                var whereClause = this.whereClause(searchParam);
                var where = !String.IsNullOrEmpty(whereClause) ? " WHERE " + whereClause : "";

                var sql = "SELECT count(*) FROM stores s " +
                    " JOIN billinginformation b ON s.BillingInformationId = b.Id " +
                    " JOIN deliveryroutes d ON s.RouteId = d.Id " +
                    " " + where 
                    ;

                return con.Query<int>(sql).SingleOrDefault<int>();

            }
        }

        public IEnumerable<Store> GetAll(SearchParam searchParam=null)
        {
            using (var con = this.Connection)
            {
                var whereClause = this.whereClause(searchParam);
                var where = !String.IsNullOrEmpty(whereClause) ? " WHERE " + whereClause : "";
                
                return con.Query<Store, BillingInformation, DeliveryRoute, Store>("SELECT s.*, b.* , d.* FROM stores s " +
                    " JOIN billinginformation b ON s.BillingInformationId = b.Id " +
                    " JOIN deliveryroutes d ON s.RouteId = d.Id " +
                    " " + where +
                    " ORDER BY d.RouteNumber, s.RouteOrderNo, b.Id ",

                    (store, billingInfo, deliveryRoute) => {
                        store.BillingInformation = billingInfo;
                        store.Route = deliveryRoute;
                        return store;
                    });

            }

        }

        private string whereClause(SearchParam searchParam = null)
        {
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
                where += " CONCAT(d.RouteNumber ,'.',COALESCE(s.RouteOrderNo,'')) LIKE '" + searchParam.Route + "%' ";
            }
            return where;
        }

        

        public Store Update(Store entity)
        {
            using (var con = this.Connection)
            {
                con.Execute("UPDATE stores SET RouteId=@RouteId,  " +
                " RouteOrderNo = @RouteOrderNo, StoreNumber = @StoreNumber, Status=@Status, Address1=@Address1, Address2=@Address2, City=@City, State=@State, Country=@Country, PostalCode=@PostalCode, ContactPerson=@ContactPerson, Phone=@Phone, Fax=@Fax, " +
                " DateModified = now() WHERE Id=@Id;", entity);

                return this.Get(entity.Id);
            }
        }

      
    }
}
