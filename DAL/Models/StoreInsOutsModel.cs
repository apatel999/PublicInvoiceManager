using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class StoreInsOutsModel
    {
        public int OrderId { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal Total { get; set; }
        public string PaymentType { get; set; }
        public int StoreId { get; set; }
        public string RouteNumber { get; set; }
        public string RouteOrderNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ProductId { get; set; }
        public int ItemOrdered { get; set; }
        public int ItemReturned { get; set; }
        public int ItemSold { get; set; }
    }
}
