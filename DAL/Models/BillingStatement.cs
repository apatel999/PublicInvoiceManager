using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class BillingStatement
    {
        public IEnumerable<BillingStatementItem> StatementItems { get; set; }
        public DateTime Year { get; set; }
        public int Week { get; set; }
    }

    public class BillingStatementItem
    {   
        public string StoreNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
    }
}
