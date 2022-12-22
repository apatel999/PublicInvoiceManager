using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerWeb.ViewModel
{
    public class BillingInformationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BillingGroupId { get; set; }
        public string PaymentType { get; set; }
        public float PercentDiscount { get; set; }
        public string Note { get; set; }
        public string HstNumber { get; set; }
        public byte PstExempt { get; set; }
        public string Status { get; set; }
        //public IEnumerable<Store> Stores { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        //public DateTime DateCreated { get; set; }
        //public DateTime DateModified { get; set; }
    }
}
