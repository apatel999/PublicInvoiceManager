using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class SearchParam
    {
        public int Page { get; set; }
        public string Route { get; set; }
        public int InvoiceNumber { get; set; }
        public List<string> OrderStatus { get; set; }
        public string CustomerName { get; set; }
        public int BillingInformationId { get; set; }
        public int StoreId { get; set; }
        /// <summary>
        /// Search in multiple table columns
        /// </summary>
        public string SearchText{ get; set; }
        public int YearId { get; set; }
        public int Week { get; set; }
        public bool AllRecords { get; set; }
        //Invoice date range.
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SearchParam ShallowCopy()
        {
            return (SearchParam)this.MemberwiseClone();
        }
    }
}
