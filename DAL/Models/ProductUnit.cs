using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ProductUnit
    {
        public Product Product { get; set; }
        public int Ins { get; set; }
        public int Outs { get; set; }
        public int Sold { get; set; }
        public decimal Percent { get; set; }
    }
}
