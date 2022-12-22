using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace DAL.Services
{
    public class OrdersService
    {
        private string _conString;
        private IOrdersRepository orderRepo;
        private IOrderItemsRepository orderItemsRepo;
        private IStoresRepository storeRepo;
        private IWeeklyOrderRepository storeWeeklyOrderRepo;
        private IRepository<Product> productRepo;
        private IDeliveryRouteRepository deliveryRouteRepo;
        private IWeeklyScheduleRepository weeklyScheduleRepo;

        private const decimal TAXPERCENT = 13;
        private ILogger _log;
        public OrdersService(string conString, ILogger log)
        {
            this._log = log;
            this._conString = conString;
            this.orderRepo = new OrdersRepository(this._conString, this._log);
            this.orderItemsRepo = new OrderItemsRepository(this._conString, this._log);
            this.storeRepo = new StoresRepository(this._conString, this._log);
            this.storeWeeklyOrderRepo = new WeeklyOrderRepository(this._conString, this._log);
            this.productRepo = new ProductRepository(this._conString, this._log);
            this.weeklyScheduleRepo = new WeeklyScheduleRepository(this._conString, this._log);
        }

        //Empty Order with default settings.
        public Order GetEmptyOrder(int storeId)
        {
            Order order = null;
            var storeInfo = this.storeRepo.Get(storeId);
            if(storeInfo != null)
            {
                order = new Order();
                order.CreateDate = DateTime.Now;
                order.InvoiceDate = DateTime.Now;
                order.OrderStatus = "ORDER";
                order.StoreId = storeId;
                order.StoreInfo = storeInfo;
                order.DiscountPercent = order.StoreInfo.BillingInformation.PercentDiscount;
                order.TaxPercent = TAXPERCENT;
                order.OrderItems = new List<OrderItem>();
            }

            return order;
        }

        public Order AddOrder(Order orderData,bool skipSubtotalCalculation=false)
        {
            orderData.TaxPercent = TAXPERCENT;
            var storeInfo = this.storeRepo.Get(orderData.StoreId);
            orderData.DiscountPercent = storeInfo.BillingInformation.PercentDiscount;

            //only weekly orders does not calculate subtotal, otherwise calculate subtotal all the time.
            if (!skipSubtotalCalculation)
            {
                this.CalculateAmount(orderData);
            }

            var newOrder = this.orderRepo.Add(orderData);
            var orderItems = new List<OrderItem>();

            foreach(var orderItem in orderData.OrderItems)
            {
                if(orderItem.ItemsOrdered >0)
                {
                    orderItem.OrderId = newOrder.Id;
                    var newOrderItem = this.orderItemsRepo.Add(orderItem);
                    orderItems.Add(newOrderItem);
                }
                
            }
            newOrder.OrderItems = orderItems;

            return newOrder;
        }

        public Order UpdateOrder(Order orderData)
        {
            orderData.TaxPercent = TAXPERCENT;
            this.CalculateAmount(orderData);

            var orderItems = new List<OrderItem>();

            foreach (var orderItem in orderData.OrderItems)
            {
                if (orderItem.ItemsOrdered > 0 && orderItem.Id <=0)
                {
                    orderItem.OrderId = orderData.Id;
                    var newOrderItem = this.orderItemsRepo.Add(orderItem);
                    orderItems.Add(newOrderItem);
                }
                else if(orderItem.Id > 0)
                {
                    //if (orderItem.ItemsOrdered <= 0)
                    //    orderItem.RecordStatus = "DEL";
                    var updatedOrderItem = this.orderItemsRepo.Update(orderItem);
                    orderItems.Add(updatedOrderItem);
                }
            }

            var updatedOrder = this.orderRepo.Update(orderData);

            updatedOrder.OrderItems = orderItems;
            return updatedOrder;
        }

        private void CalculateAmount(Order order)
        {
            order.SubTotal = 0;
            foreach (var orderItem in order.OrderItems)
            {
                if(orderItem.RecordStatus == "ACT")
                {
                    orderItem.ItemsSold = orderItem.ItemsOrdered - orderItem.ItemsReturned;
                    var discountedItemPrice = orderItem.ItemPrice * (1 - (order.DiscountPercent / 100));
                    orderItem.DiscountPrice = Math.Round(discountedItemPrice, 2);
                    orderItem.SubTotal = orderItem.DiscountPrice * orderItem.ItemsSold;
                    order.SubTotal += orderItem.SubTotal;
                }
            }
            
            order.TaxAmount = (order.SubTotal) * (order.TaxPercent / 100);
            order.TaxAmount = Math.Round(order.TaxAmount, 2);
            order.Total = order.SubTotal + order.TaxAmount;
        }

        public Order Get(int orderId)
        {
            var order = this.orderRepo.Get(orderId);
            if(order !=null)
            {
                order.OrderItems = new List<OrderItem>();
                var orderItems = this.orderItemsRepo.GetAllOrderItems(orderId);
                if (orderItems != null && orderItems.Count() > 0)
                    order.OrderItems = orderItems;

            }
            return order;
        }

        public IEnumerable<Order> GetAll(SearchParam searchParam)
        {
            return this.orderRepo.GetAll(searchParam);
        }

        public int OrderCount(SearchParam searchParam)
        {
            return this.orderRepo.Count(searchParam);
        }

        public IEnumerable<int> GetAllOrderIds(SearchParam searchParam)
        {
            return this.orderRepo.GetAllOrderIds(searchParam);
        }

        public void AddAllWeeklyOrders(int startYearId, int week)
        {
            var stores = this.storeWeeklyOrderRepo.GetStoreListForWeeklyOrders(startYearId, week);
            var weeklySchedules = this.weeklyScheduleRepo.GetAll(startYearId, week);
            
            //LOG results
            foreach (var store in stores)
            {
                try
                {
                    var weeklyOrderItems = this.storeWeeklyOrderRepo.GetOrderItems(store.Id);
                    var routeWeeklySchedule = weeklySchedules.FirstOrDefault(schedule => schedule.RouteId == store.RouteId);
                    var order = this.MapWeeklyOrderToOrder(routeWeeklySchedule.Id, store, weeklyOrderItems, routeWeeklySchedule.DeliveryDate);
                    if (order != null)
                    {
                        var newOrder = this.AddOrder(order, true);
                        //log(newOrder)
                    }
                }
                catch (Exception ex)
                {
                    //log(ex)
                }

            }

        }
        
       
        private Order MapWeeklyOrderToOrder(int routeWeeklyScheduleId, Store store, IEnumerable<WeeklyOrderItem> weeklyOrderItems,DateTime deliveryDate)
        {
            var newOrder = this.GetEmptyOrder(store.Id);
            newOrder.RouteWeeklyScheduleId = routeWeeklyScheduleId;
            newOrder.InvoiceDate = deliveryDate;

            var orderItems = new List<OrderItem>();
            foreach (var item in weeklyOrderItems)
            {
                var orderItem = this.MapWeeklyOrderItemToOrderItem(item);
                var discountedItemPrice = orderItem.ItemPrice * (1 - (newOrder.DiscountPercent / 100));
                orderItem.DiscountPrice = Math.Round(discountedItemPrice, 2);
                orderItems.Add(orderItem);
            }

            if (orderItems.Count > 0)
            {
                newOrder.OrderItems = orderItems;
                return newOrder;
            }
            else
            {
                return null;
            }
        }

        private OrderItem MapWeeklyOrderItemToOrderItem(WeeklyOrderItem item)
        {
            var orderItem = new OrderItem();
            orderItem.ProductId = item.Product.Id;
            orderItem.ItemsOrdered = item.Quantity;
            orderItem.ItemPrice = item.Product.Price;
            orderItem.RecordStatus = "ACT";
            return orderItem;
        }
    }
}
