// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Models;
using DAL.Repositories.Interfaces;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        IRepository<Product> Products { get; }
        IDeliveryRouteRepository DeliveryRoutes { get; }
        IBillingInformationRepository BillingInformation { get; }
        IStoresRepository Stores { get; }
        IWeeklyOrderRepository StoreWeeklyOrders { get; }
        IWeeklyScheduleRepository WeeklySchedule { get; }
        OrdersService Orders { get; }
        ProductionScheduleService ProductionSchedule {get;}
        int SaveChanges();

    }
}
