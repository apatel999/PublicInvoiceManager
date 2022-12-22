using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ProductionScheduleData
    {
        public int YearId { get; set; }
        public int Week { get; set; }
        public DateTime ReportDate { get; set; }
        public IEnumerable<ScheduleProductData> Products { get; set; }
        public IEnumerable<DeliveryRoute> Routes { get; set; }
    }

    public class ScheduleProductData
    {
        public Product Product { get; set; }
        public IEnumerable<ProductRouteOrder> Orders { get; set; }
        public int OrdersTotal { get; set; }
    }

    public class ProductRouteOrder
    {
        public int ProductId { get; set; }
        public int RouteId { get; set; }
        public int Total { get; set; }
    }
}
