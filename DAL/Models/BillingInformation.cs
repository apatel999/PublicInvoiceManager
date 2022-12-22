// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BillingInformation 
    {
        public BillingInformation()
        {
            this.Stores = new List<Store>();                
        }

        public int Id { get; set; }
        private string _billingGrouId;
        public string BillingGroupId
        {
            get
            {
                return String.IsNullOrEmpty(_billingGrouId) ? null : _billingGrouId;
            }
            set
            {
                _billingGrouId = value?.Trim();
            }
        }
        
        public string Name { get; set; }
        public string PaymentType { get; set; }
        public decimal PercentDiscount { get; set; }
        public string Note { get; set; }
        public string HstNumber { get; set; }
        public byte PstExempt { get; set; }
        public string Status { get; set; }
        public List<Store> Stores { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public decimal Ytd { get; set; }
        public decimal Ltd { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}