using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class RouteSalesAuditModel
    {
        public DeliveryRoute Route { get; set; }
        public IEnumerable<Order> Orders;
        public OrderTotal CashSale;
        public OrderTotal ChargeSale;
        public OrderTotal AllSale;
        public DateTime ReportDate;
        public int Week;
    }
}
