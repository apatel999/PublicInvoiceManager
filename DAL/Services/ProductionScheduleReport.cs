using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DAL.Services
{
    public class ProductionScheduleService
    {
       
        private string _conString;
        private IRepository<Product> productRepo;
        private IDeliveryRouteRepository deliveryRouteRepo;
        private ReportsRepository reportRepo;
        private IWeeklyScheduleRepository weeklyScheduleRepo;
        private ILogger _log;
        public ProductionScheduleService(string conString, ILogger log)
        {
            this._log = log;
            this._conString = conString;
            this.deliveryRouteRepo = new DeliveryRoutesRepository(this._conString, this._log);
            this.productRepo = new ProductRepository(this._conString, this._log);
            this.reportRepo = new ReportsRepository(this._conString, this._log);
            this.weeklyScheduleRepo = new WeeklyScheduleRepository(this._conString, this._log);
        }

        /*
           {
                "Products": [
                    {
                        "Product": {
                            "Id": 1,
                            "Productcode": "1001",
                            "Name": "Red Rose Update",
                            "Description": "Red Rose",
                            "Price": 5.55,
                            "Status": null,
                            "Createdate": "0001-01-01T00:00:00",
                            "Updatedate": "2017-12-27T09:15:23"
                        },
                        "Orders": [
                            {
                                "ProductId": 1,
                                "RouteId": 2,
                                "Total": 8
                            },
                            {
                                "ProductId": 1,
                                "RouteId": 3,
                                "Total": 4
                            },
                            {
                                "ProductId": 1,
                                "RouteId": 4,
                                "Total": 2
                            },
                            {
                                "ProductId": 1,
                                "RouteId": 7,
                                "Total": 0
                            }
                        ],
                        OrderTotal:14
                    } ]
             "Routes": [
                    {
                        "Id": 2,
                        "RouteNumber": "200",
                        "Description": "Missisauga",
                        "ProductionDay": 3,
                        "DeliveryDay": 1,
                        "RecordCreateDate": "0001-01-01T00:00:00",
                        "RecordUpdateDate": "2018-06-10T10:05:15"
                    }]

        */

        public ProductionScheduleData GenerateReport(int yearId, int week)
        {
            var yearDate = this.weeklyScheduleRepo.GetFirstDayOfTheYear(yearId);
            var products = this.productRepo.GetAll().ToArray();
            var routes = this.deliveryRouteRepo.GetAll().ToArray();
            var productRouteOrders = this.reportRepo.GetProductionSchedule(yearId, week);
            var reportData = this.getProductionScheduleReportData(products, routes, productRouteOrders);
            reportData.YearId = yearId;
            reportData.Week = week;
            reportData.ReportDate = yearDate.AddDays(week * 7);
            return reportData;
        }

        private ProductionScheduleData getProductionScheduleReportData(IEnumerable<Product> products, IEnumerable<DeliveryRoute> routes, IEnumerable<ProductRouteOrder> productRouteOrders)
        {
            var schedule = new ProductionScheduleData();
            var scheduleProducts = new List<ScheduleProductData>();

            foreach (var product in products)
            {
                var scheduleProduct = new ScheduleProductData();
                var productOrders = new List<ProductRouteOrder>();
                var ordersTotal = 0;
                foreach (var route in routes)
                {
                    var productRouteOrder = this.getProductRouteOrder(product, route, productRouteOrders);
                    productOrders.Add(productRouteOrder);
                    ordersTotal += productRouteOrder.Total;
                }
                scheduleProduct.Product = product;
                scheduleProduct.Orders = productOrders;
                scheduleProduct.OrdersTotal = ordersTotal;
                scheduleProducts.Add(scheduleProduct);
            }

            schedule.Products = scheduleProducts;
            schedule.Routes = routes;

            return schedule;
        }

        private ProductRouteOrder getProductRouteOrder(Product product, DeliveryRoute route, IEnumerable<ProductRouteOrder> productRouteOrders)
        {
            var order = (from ps in productRouteOrders
            where (ps.ProductId == product.Id )
            where (ps.RouteId == route.Id)
            select ps).SingleOrDefault<ProductRouteOrder>();
           
            if(order != null)
            {
                return order;
            }
            return new ProductRouteOrder { ProductId = product.Id, RouteId = route.Id, Total = 0 };
        }
    }
}
