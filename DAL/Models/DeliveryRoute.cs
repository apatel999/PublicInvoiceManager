using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class DeliveryRoute: AuditData
    {
        public int Id { get; set; }
        public string RouteNumber { get; set; }
        public string Description { get; set; }
        //public DateTime DeliveryDate { get; set; }
        public int ProductionDay { get; set; }
        public int DeliveryDay { get; set; }
    }
}
