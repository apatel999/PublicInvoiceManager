using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DAL.Services
{
    public interface ISalesReportService
    {
        InsOutReport GetDeliverySheetInsOut(SearchParam searchParam);
        RouteSalesSummaryAuditModel GetSalseSummaryByRoute(SearchParam searchParam);
        RouteSalesAuditModel GetSalesAuditByRoute(SearchParam searchParam);
        IEnumerable<ProductYtdCustomerUnitsReport> CustomerUnitsReport(SearchParam searchParam);
    }

    public class SalesReportService : ISalesReportService
    {
        private ReportsRepository _reportRepo;
        private ILogger _log;
        private IUnitOfWork _unitOfWork;

        public SalesReportService(IDatabaseOptions dbOptions, ILogger<UnitOfWork> log, IUnitOfWork unitOfWork)
        {
            this._log = log;
            this._reportRepo = new ReportsRepository(dbOptions.ConnectionString, this._log);
            this._unitOfWork = unitOfWork;
        }

        public RouteSalesSummaryAuditModel GetSalseSummaryByRoute(SearchParam searchParam)
        {
            var orders = this._reportRepo.GetOrders(searchParam);
            var startEndDate = this._unitOfWork.WeeklySchedule.GetStartEndDates(searchParam.YearId, searchParam.Week);
            return new RouteSalesSummaryAuditModel
            {
                RouteSummary = CalculateRoutesSalesSummary(orders),
                CashSale = CalculateTotal(orders, "CASH"),
                ChargeSale = CalculateTotal(orders,"CHARG"),
                AllSale = CalculateTotal(orders),
                ReportDate = startEndDate.Item1,
                Week = searchParam.Week
            };
            
        }

        private IEnumerable<RouteSalesAuditModel> CalculateRoutesSalesSummary(IEnumerable<Order> orders)
        {
            var routeSummaryList = new List<RouteSalesAuditModel>();
            var routes = (from o in orders select o.StoreInfo.Route)
                        .GroupBy(route => route.Id, (key, group) => group.First<DeliveryRoute>());
            foreach (var route in routes)
            {
                var routeOrders = from o in orders where o.StoreInfo.RouteId == route.Id select o;

                var routeSalesSummary = new RouteSalesAuditModel
                {
                    Route = route,
                    CashSale = CalculateTotal(routeOrders, "CASH"),
                    ChargeSale = CalculateTotal(routeOrders, "CHARG"),
                    AllSale = CalculateTotal(routeOrders)
                };
                routeSummaryList.Add(routeSalesSummary);
            }
            return routeSummaryList;
        }

        public RouteSalesAuditModel GetSalesAuditByRoute(SearchParam searchParam)
        {

            var orders = this._reportRepo.GetOrders(searchParam);
            var startEndDate = this._unitOfWork.WeeklySchedule.GetStartEndDates(searchParam.YearId, searchParam.Week);
            return new RouteSalesAuditModel
            {
                Orders = orders,
                CashSale = CalculateTotal(orders, "CASH"),
                ChargeSale = CalculateTotal(orders, "CHARG"),
                AllSale = CalculateTotal(orders),
                ReportDate = startEndDate.Item1,
                Week = searchParam.Week
            };

        }

        private OrderTotal CalculateTotal(IEnumerable<Order> orders, string paymentType = null)
        {
            var filteredOrders = orders;
            if(!string.IsNullOrEmpty(paymentType))
            {
                filteredOrders = from o in orders
                                 where string.Compare(o.StoreInfo.BillingInformation.PaymentType, paymentType, true) == 0
                                 select o;
            }

            return new OrderTotal
            {
                Tax = (from order in filteredOrders select order.TaxAmount).Sum(),
                SubTotal = (from order in filteredOrders select order.SubTotal).Sum(),
                Total = (from order in filteredOrders select order.Total).Sum(),
            };
        }


        //TODO: More columns required. Report data incomplete. need to understand Years <- Weeks columns, recommendation columns. 
        public IEnumerable<ProductYtdCustomerUnitsReport> CustomerUnitsReport(SearchParam searchParam)
        {
            var currentYearOrders = _reportRepo.YearToDateCustomerUnits(searchParam);
            
            var groupbyStore = currentYearOrders.GroupBy(order => order.Store.Id,
                                    order => order,
                                    (storeId, groupedOrders) =>
                                    {
                                        var store = groupedOrders.First().Store;
                                        return new ProductYtdCustomerUnitsReport
                                        {
                                            Store = store,
                                            YtdUnits = ComposeYtdProductUnitList(groupedOrders, searchParam.Week)
                                        };
                                    });

            /***************** NOTE ************************/
            // There are 4 holiday weeks, things sold in those weeks are subtracted from current yeartodate sale to find previous yeartodate sale.
            // same way those 4 weeks also subtracted
            // Check with john how holiday weeks are managed. Once I know how holiday is managed, will calculate Recommendations.
            /**********************************************/
            return groupbyStore;

        }

        private IEnumerable<YtdProductUnit> ComposeYtdProductUnitList(IEnumerable<StoreProductYtdUnitRepoModel> data, int weeks)
        {
            return data.Select(ytdUnit => new YtdProductUnit
            {
                Product = ytdUnit.Product,
                YtdIns = ytdUnit.Ins,
                YtdSold = ytdUnit.Sold,
                YtdPercent = CalculateSoldPercent(ytdUnit), 
                YtdMinusHolidayIns = ytdUnit.Ins, // until decided how 4 holidays managed it is same
                YtdMinusHolidaySold = ytdUnit.Sold, // until decided how 4 holidays managed it is same
                YtdMinusHolidayPercent = CalculateSoldPercent(ytdUnit),// until decided how 4 holidays managed it is same

                Check = CalculateChek(ytdUnit,weeks),
                Recommended = CalculateRecommended(ytdUnit, weeks)

            });
        }

        private decimal CalculateSoldPercent(StoreProductYtdUnitRepoModel ytdUnit)
        {
            return ytdUnit.Ins > 0 ? Math.Round(((decimal)ytdUnit.Sold * 100 / ytdUnit.Ins), 2) : 0;
        }

        private int CalculateRecommended(StoreProductYtdUnitRepoModel ytdUnit, int weeks)
        {
            var ytdSoldPercent = CalculateSoldPercent(ytdUnit);
            var dSold = (decimal)ytdUnit.Sold * 2;
            var dWeeks = (decimal)weeks;
            var recommended = 0;
            if (ytdSoldPercent < 50)
                recommended = (int)Math.Floor(dSold/ dWeeks);
            else
                recommended = (int)Math.Round(dSold/ dWeeks);
            return recommended;
        }

        //TODO: Verify is it Configured INS or total-Ins/weeks
        private int CalculateChek(StoreProductYtdUnitRepoModel ytdUnit, int weeks)
        {
            return (int)Math.Ceiling((decimal)ytdUnit.Ins / (decimal)weeks);
        }

        private decimal YtdPercent(int sold, int ins)
        {
            return ins > 0 ? Math.Round(((decimal)sold * 100 / ins), 2) : 0;
        }

        public InsOutReport GetDeliverySheetInsOut(SearchParam searchParam)
        {
            var report = GenerateDeliverySheetInsOutsReport(searchParam);
            var startEndDate = this._unitOfWork.WeeklySchedule.GetStartEndDates(searchParam.YearId, searchParam.Week);
            return new InsOutReport
            {
                InsOuts = report?.Item1,
                InsOutsTotal = report?.Item2,
                ReportDate = startEndDate.Item1,
                Week = searchParam.Week
            };

        }

        private Tuple<IList<CustomerProductUnits>, IDictionary<int, ProductUnit>> GenerateDeliverySheetInsOutsReport(SearchParam searchParam)
        {
            IDictionary<int, Product> productDictionary = GetProducts();
            IDictionary<int, ProductUnit> insOutsTotal = GetProductUnits();
            var orders = this._reportRepo.GetOrders(searchParam);

            var groupbyStore = orders.GroupBy(order => order.StoreId,
                                    order => order,
                                    (storeId, groupedOrders) =>
                                    {
                                        return groupedOrders;
                                    });

            IList<CustomerProductUnits> result = new List<CustomerProductUnits>();

            foreach (var groupedOrders in groupbyStore)
            {
                var customerProductUnits = new CustomerProductUnits();
                customerProductUnits.Store = groupedOrders.First().StoreInfo;
                customerProductUnits.Products =
                    groupedOrders.SelectMany(order => order.OrderItems)
                    .GroupBy(orderItem => orderItem.ProductId,
                            orderItem => orderItem,
                            (productId, orderItems) =>
                            {
                                //grand total
                                var ins = orderItems.Sum(orderItem => orderItem.ItemsOrdered);
                                var outs = orderItems.Sum(orderItem => orderItem.ItemsReturned);
                                insOutsTotal[productId].Ins += ins;
                                insOutsTotal[productId].Outs += outs;

                                var productUnit = new ProductUnit
                                {
                                    Product = productDictionary[productId],
                                    Ins = ins,
                                    Outs = outs,
                                    Sold = orderItems.Sum(orderItem => orderItem.ItemsSold)
                                };
                                return productUnit;
                            });


                result.Add(customerProductUnits);
            }

            return Tuple.Create<IList<CustomerProductUnits>, IDictionary<int, ProductUnit>>( result, insOutsTotal );
        }

        private IDictionary<int, Product> GetProducts()
        {
            var productDictionary = new Dictionary<int, Product>();
            var products = _unitOfWork.Products.GetAll();
            foreach (var p in products)
            {
                productDictionary.Add(p.Id, p);
            }

            return productDictionary;
        }

        private IDictionary<int, ProductUnit> GetProductUnits()
        {
            var dictionary = new Dictionary<int, ProductUnit>();
            var products = _unitOfWork.Products.GetAll();
            foreach (var p in products)
            {
                dictionary.Add(p.Id, new ProductUnit {Ins = 0, Outs = 0 });
            }

            return dictionary;
        }
    }

    public class InsOutReport
    {
        public DateTime ReportDate;
        public int Week;

        public IEnumerable<CustomerProductUnits> InsOuts { get; set; }
        public IDictionary<int, ProductUnit> InsOutsTotal { get; set; }
    }

    public class CustomerProductUnits
    {
        public Store Store { get; set; }
        public IEnumerable<ProductUnit> Products { get; set; }

    }
}
