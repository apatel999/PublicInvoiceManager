using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IWeeklyOrderRepository:IRepository<WeeklyOrderItem>
    {
       IEnumerable<WeeklyOrderItem> GetOrderItems(int storeId);
       IEnumerable<Store> GetStoreListForWeeklyOrders(int yearId, int week);
    }
}
