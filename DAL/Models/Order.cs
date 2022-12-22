// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order 
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public int StoreId { get; set; }
        public int RouteWeeklyScheduleId { get; set; }
        public int Week { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public string OrderStatus { get; set; }
        public string RecordStatus { get; set; }
        public string Note { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal Total { get; set; }

        public Store StoreInfo { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }

    }
}
