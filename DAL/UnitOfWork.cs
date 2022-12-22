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
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.Models;
using DAL.Services;
using Microsoft.Extensions.Logging;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private IDatabaseOptions dbOptions;

        public IBillingInformationRepository _billingInformation;
        public IRepository<Product> _products;
        public IDeliveryRouteRepository _deliveryRoutes;
        public IOrdersRepository _orders;
        public IStoresRepository _stores;
        public IWeeklyOrderRepository _storeWeeklyOrders ;
        public OrdersService _orderService;
        public IWeeklyScheduleRepository _weeklySchedule;
        public ProductionScheduleService _productionSchedule;
        public ILogger _log;
        public UnitOfWork(IDatabaseOptions options, ILogger<UnitOfWork> log  )
        {
            this.dbOptions = options;
            this._log = log;
            this._log.LogInformation("UnitOfWork Object created");
        }
        
        public IBillingInformationRepository BillingInformation
        {
            get
            {
                if (_billingInformation == null)
                    _billingInformation = new BillingInformationRepository(dbOptions.ConnectionString, this._log);

                return _billingInformation;
            }
        }


        public IRepository<Product> Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(dbOptions.ConnectionString, this._log);
                return _products;
            }
        }

        public IDeliveryRouteRepository DeliveryRoutes
        {
            get
            {
                if (_deliveryRoutes == null)
                    _deliveryRoutes = new DeliveryRoutesRepository(dbOptions.ConnectionString, this._log);
                return _deliveryRoutes;
            }
        }


        public OrdersService Orders
        {
            get
            {
                if (_orderService == null)
                    _orderService = new OrdersService(dbOptions.ConnectionString, this._log);

                return _orderService;
            }
        }

        public IStoresRepository Stores{
            get
            {
                if (_stores == null)
                    _stores = new StoresRepository(dbOptions.ConnectionString, this._log);
                return _stores;
            }
        }

        public IWeeklyOrderRepository StoreWeeklyOrders
        {
            get
            {
                if (_storeWeeklyOrders == null)
                    _storeWeeklyOrders = new WeeklyOrderRepository(dbOptions.ConnectionString, this._log);
                return _storeWeeklyOrders;
            }
        }

        public IWeeklyScheduleRepository WeeklySchedule
        {
            get
            {
                if (_weeklySchedule == null)
                    _weeklySchedule = new WeeklyScheduleRepository(dbOptions.ConnectionString, this._log);
                return _weeklySchedule;
            }
        }

        public ProductionScheduleService ProductionSchedule
        {
            get
            {
                if (_productionSchedule == null)
                    _productionSchedule = new ProductionScheduleService(dbOptions.ConnectionString, this._log);

                return _productionSchedule;
            }
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
