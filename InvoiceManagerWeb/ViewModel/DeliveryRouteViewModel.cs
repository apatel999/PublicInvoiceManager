using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerWeb.ViewModel
{
    public class DeliveryRouteViewModel
    {
        public int Id { get; set; }
        public string RouteNumber { get; set; }
        public string Description { get; set; }
        //public DateTime DeliveryDate { get; set; }
        public int ProductionDay { get; set; }
        public int DeliveryDay { get; set; }
    }
}
