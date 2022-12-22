using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IOrderItemsRepository: IRepository<OrderItem>
    {
        IEnumerable<OrderItem> GetAllOrderItems(int orderId);
    }
}
