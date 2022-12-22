// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;

namespace DAL.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ItemsOrdered { get; set; }
        public int ItemsReturned { get; set; }
        public int ItemsSold { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string RecordStatus { get; set; }
        public decimal SubTotal { get; set; }
        public decimal RegularPrice { get; set; }
        public string ProductName { get; set; }
        public int RouteWeeklyScheduleId { get; set; }
    }
}