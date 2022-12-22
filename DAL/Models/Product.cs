// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product 
    {
        public int Id { get; set; }
        public string Productcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string RecordStatus { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime Updatedate { get; set; }
    }
}
