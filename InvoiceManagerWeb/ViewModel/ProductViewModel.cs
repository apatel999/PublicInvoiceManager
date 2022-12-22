using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManagerWeb.ViewModel
{
    public class ProductViewModel
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
