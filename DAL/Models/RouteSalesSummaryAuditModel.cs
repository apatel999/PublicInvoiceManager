using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class RouteSalesSummaryAuditModel
    {
        public IEnumerable<RouteSalesAuditModel> RouteSummary { get; set; }
        public DateTime ReportDate; 
        public OrderTotal CashSale;
        public OrderTotal ChargeSale;
        public OrderTotal AllSale;
        public int Week;
    }
}
