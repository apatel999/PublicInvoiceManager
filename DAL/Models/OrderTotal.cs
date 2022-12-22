using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class OrderTotal
    {
        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
