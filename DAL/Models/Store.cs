using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Store
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string RouteOrderNo { get; set; }
        public string StoreNumber { get; set; }
        public string Status { get; set; }
        public int BillingInformationId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public BillingInformation BillingInformation { get; set; }
        public DeliveryRoute Route { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
