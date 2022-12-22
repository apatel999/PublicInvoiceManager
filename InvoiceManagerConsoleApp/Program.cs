using System;

using System.Collections.Generic;
using DAL.Models;
using InvoiceManagerWeb.Print;

namespace InvoiceManagerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var order = GetOrder();
            var invoiceReport = new InvoiceTextReport(GetEmptyOrder());
            string report = invoiceReport.GetText();
            Console.WriteLine(report);

            var invoiceReport2 = new InvoiceTextReport(GetNewOrder());
            string report2 = invoiceReport2.GetText();
            Console.WriteLine(report2);

            var invoiceReport3 = new InvoiceTextReport(GetOrder());
            string report3 = invoiceReport3.GetText();
            Console.WriteLine(report3);
        }


        private static Order GetEmptyOrder()
        {
            return null;
        }

        //orders that does not have outs
        private static Order GetNewOrder()
        {
            return new Order
            {
                Id = 5,
                InvoiceNumber = 0,
                StoreId = 1,
                RouteWeeklyScheduleId = 34,
                CreateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                UpdateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                InvoiceDate = Convert.ToDateTime("2018-08-06T12:37:01"),
                TaxAmount = 0,
                DiscountPercent = (decimal)25.0000,
                DiscountAmount = (decimal)0.0000,
                OrderStatus = "ORDER",
                RecordStatus = "ACT",
                Note = null,
                SubTotal = 0,
                TaxPercent = 0,
                Total = 0,

                StoreInfo = new Store
                {
                    Id = 1,
                    RouteId = 2,
                    RouteOrderNo = "0006",
                    StoreNumber = "100203",
                    Status = "ACT",
                    BillingInformationId = 1,
                    Address1 = "156 Toronto Rd",
                    Address2 = null,
                    City = "Port Hope",
                    State = "Ontario",
                    Country = null,
                    PostalCode = "L1A 3S5",
                    ContactPerson = null,
                    Phone = null,
                    Fax = null,
                    BillingInformation = new BillingInformation
                    {
                        Id = 1,
                        BillingGroupId = "232323",
                        Name = "Petro Can",
                        PaymentType = "CHARG",
                        PercentDiscount = (decimal)25.0,
                        Note = null,
                        HstNumber = null,
                        PstExempt = 0,
                        Status = "ACT",
                        Stores = null,
                        Address1 = "156 Toronto Rd",
                        Address2 = null,
                        City = "Port Hope",
                        State = "Ontario",
                        Country = null,
                        PostalCode = "L1A 3S5",
                        ContactPerson = null,
                        Phone = null,
                        Fax = null,
                        Ytd = (decimal)0.0,
                        Ltd = (decimal)0.0,
                        DateCreated = Convert.ToDateTime("2018-07-21T17:49:01"),
                        DateModified = Convert.ToDateTime("2018-08-19T13:38:14")
                    },
                    Route = new DeliveryRoute
                    {
                        Id = 2,
                        RouteNumber = "300",
                        Description = "Kingston",
                        ProductionDay = 2,
                        DeliveryDay = 2,
                        RecordCreateDate = Convert.ToDateTime("2018-07-21T17:23:32"),
                        RecordUpdateDate = Convert.ToDateTime("2018-07-29T10:35:07")
                    },
                    DateCreated = Convert.ToDateTime("2018-07-21T17:52:12"),
                    DateModified = Convert.ToDateTime("2018-08-12T12:04:25")
                },
                OrderItems = new List<OrderItem>
                {
                 new OrderItem
                    {
                     Id = 13,
                    OrderId = 5,
                    ProductId = 1,
                    ItemsOrdered = 4,
                    ItemsReturned = 0,
                    ItemsSold = 0,
                    ItemPrice = (decimal)10.9900,
                    DiscountPrice = (decimal)8.2400,
                    CreateDate = Convert.ToDateTime("2018-08-06T12:37:02"),
                    UpdateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                    RecordStatus = "ACT",
                    SubTotal = 0,
                    RegularPrice = (decimal)12.99,
                    ProductName = "Single Rose Test2",
                    RouteWeeklyScheduleId = 0
                },
                 new OrderItem
                {
                                Id = 14,
                    OrderId = 5,
                    ProductId = 2,
                    ItemsOrdered = 1,
                    ItemsReturned = 0,
                    ItemsSold = 0,
                    ItemPrice = (decimal)10.9900,
                    DiscountPrice = (decimal)8.2400,
                    CreateDate = Convert.ToDateTime("2018-08-06T12:37:02"),
                    UpdateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                    RecordStatus = "ACT",
                    SubTotal = 0,
                    RegularPrice = (decimal)10.99,
                    ProductName = "Rose Bouquet",
                    RouteWeeklyScheduleId = 0
                },
                 new OrderItem
                {
                                Id = 15,
                    OrderId = 5,
                    ProductId = 5,
                    ItemsOrdered = 3,
                    ItemsReturned = 0,
                    ItemsSold = 0,
                    ItemPrice = (decimal)10.9900,
                    DiscountPrice = (decimal)8.2400,
                    CreateDate = Convert.ToDateTime("2018-08-06T12:37:02"),
                    UpdateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                    RecordStatus = "ACT",
                    SubTotal = 0,
                    RegularPrice = (decimal)10.99,
                    ProductName = "Deluxe Bunch",
                    RouteWeeklyScheduleId = 0
                }

            }


            };

        }

        private static Order GetOrder()
        {
            return new Order
            {
                Id = 5,
                InvoiceNumber = 0,
                StoreId = 1,
                RouteWeeklyScheduleId = 34,
                CreateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                UpdateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                InvoiceDate = Convert.ToDateTime("2018-08-06T12:37:01"),
                TaxAmount = (decimal)7.4984,
                DiscountPercent = (decimal)25.0000,
                DiscountAmount = (decimal)0.0000,
                OrderStatus = "ORDER",
                RecordStatus = "ACT",
                Note = null,
                SubTotal = (decimal)57.6800,
                TaxPercent = (decimal)13.0000,
                Total = (decimal)65.1784,

                StoreInfo = new Store
                {
                Id = 1,
                RouteId = 2,
                RouteOrderNo = "0006",
                StoreNumber = "100203",
                Status = "ACT",
                BillingInformationId = 1,
                Address1 = "156 Toronto Rd",
                Address2 = null,
                City = "Port Hope",
                State = "Ontario",
                Country = null,
                PostalCode = "L1A 3S5",
                ContactPerson = null,
                Phone = null,
                Fax = null,
                BillingInformation = new BillingInformation{
                    Id = 1,
                    BillingGroupId = "232323",
                    Name = "Petro Can",
                    PaymentType = "CHARG",
                    PercentDiscount = (decimal)25.0,
                    Note = null,
                    HstNumber = null,
                    PstExempt = 0,
                    Status = "ACT",
                    Stores = null,
                    Address1 = "156 Toronto Rd",
                    Address2 = null,
                    City = "Port Hope",
                    State = "Ontario",
                    Country = null,
                    PostalCode = "L1A 3S5",
                    ContactPerson = null,
                    Phone = null,
                    Fax = null,
                    Ytd = (decimal)0.0,
                    Ltd = (decimal)0.0,
                    DateCreated = Convert.ToDateTime("2018-07-21T17:49:01"),
                    DateModified = Convert.ToDateTime("2018-08-19T13:38:14")
                    },
                Route = new DeliveryRoute{
                    Id = 2,
                    RouteNumber = "300",
                    Description = "Kingston",
                    ProductionDay = 2,
                    DeliveryDay = 2,
                    RecordCreateDate = Convert.ToDateTime("2018-07-21T17:23:32"),
                    RecordUpdateDate = Convert.ToDateTime("2018-07-29T10:35:07")
                    },
                DateCreated = Convert.ToDateTime("2018-07-21T17:52:12"),
                DateModified = Convert.ToDateTime("2018-08-12T12:04:25")
                },
                OrderItems = new List<OrderItem>
                {
                 new OrderItem
                    {
                     Id = 13,
                    OrderId = 5,
                    ProductId = 1,
                    ItemsOrdered = 4,
                    ItemsReturned = 0,
                    ItemsSold = 4,
                    ItemPrice = (decimal)10.9900,
                    DiscountPrice = (decimal)8.2400,
                    CreateDate = Convert.ToDateTime("2018-08-06T12:37:02"),
                    UpdateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                    RecordStatus = "ACT",
                    SubTotal = (decimal)32.9600,
                    RegularPrice = (decimal)10.99,
                    ProductName = "Single Rose Test2",
                    RouteWeeklyScheduleId = 0
                },
                 new OrderItem
                {
                                Id = 14,
                    OrderId = 5,
                    ProductId = 2,
                    ItemsOrdered = 1,
                    ItemsReturned = 0,
                    ItemsSold = 1,
                    ItemPrice = (decimal)10.9900,
                    DiscountPrice = (decimal)8.2400,
                    CreateDate = Convert.ToDateTime("2018-08-06T12:37:02"),
                    UpdateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                    RecordStatus = "ACT",
                    SubTotal = (decimal)8.2400,
                    RegularPrice = (decimal)10.99,
                    ProductName = "Rose Bouquet",
                    RouteWeeklyScheduleId = 0
                },
                 new OrderItem
                {
                                Id = 15,
                    OrderId = 5,
                    ProductId = 5,
                    ItemsOrdered = 3,
                    ItemsReturned = 1,
                    ItemsSold = 2,
                    ItemPrice = (decimal)10.9900,
                    DiscountPrice = (decimal)8.2400,
                    CreateDate = Convert.ToDateTime("2018-08-06T12:37:02"),
                    UpdateDate = Convert.ToDateTime("2018-08-06T12:39:12"),
                    RecordStatus = "ACT",
                    SubTotal = (decimal)16.4800,
                    RegularPrice = (decimal)10.99,
                    ProductName = "Deluxe Bunch",
                    RouteWeeklyScheduleId = 0
                }

            }


            };


        }
    }
}
