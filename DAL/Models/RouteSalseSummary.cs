using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class RouteSalseSummary
    {
        public int RouteNumber { get; set; }
        public int RouteDescription { get; set; }
        public string PaymentType { get; set; }
        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

    }
}
