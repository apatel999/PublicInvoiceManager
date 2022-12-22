using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class WeeklyOrderItem
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string RecordStatus { get; set; }

        public Product Product { get; set; }

    }
    
}
