import { Component, Input, OnChanges, OnInit } from "@angular/core";
import { OrderItem, Order } from "../order.model";
import { ProductService } from "../../product/product.service";
import * as _ from "lodash";
import { Subject } from "rxjs";
import { debounceTime, takeUntil } from "rxjs/operators";

@Component({
  selector: "invoice-items",
  templateUrl: "./invoice-items.component.html",
  styleUrls: ["./invoice-items.component.css"]
})
export class InvoiceItemsComponent implements OnInit, OnChanges {
  @Input("order") order: Order;
  @Input("isEditMode") isEditMode: boolean;
  @Input("isNewMode") isNewMode: boolean;

  private itemCount: number = 1;
  private destroy: Subject<any> = new Subject();
  public recalculateOrder: Subject<any> = new Subject();
  allProducts: any;
  constructor(public productService: ProductService) {}

  ngOnChanges() {
    if (this.order) this.assignItemCount();
  }

  ngOnInit() {
    this.productService.allProducts.subscribe(data => {
      this.allProducts = data;
    });

    this.recalculateOrder
      .pipe(debounceTime(500), takeUntil(this.destroy))
      .subscribe(shouldRecalculate => {
        this.calculateOrder();
      });
  }

  onProductSelect(productId, orderItem) {
    let selectedProduct = _.find(this.allProducts, p => p.Id == productId);
    if (selectedProduct) {
      orderItem.RegularPrice = selectedProduct.Price;
      orderItem.ItemPrice = selectedProduct.Price;
      orderItem.ProductId = selectedProduct.Id;
      this.recalculateOrder.next(true);
    }
    // this.productService.allProducts
    //   .take(1)
    //   .takeUntil(this.destroy)
    //   .subscribe(products => {
    //     let selectedProduct = _.find(products, p => p.Id == productId);
    //     if (selectedProduct) {
    //       orderItem.RegularPrice = selectedProduct.Price;
    //       orderItem.ItemPrice = selectedProduct.Price;
    //       orderItem.ProductId = selectedProduct.Id;
    //       this.recalculateOrder.next(true);
    //     }
    //   });
  }

  itemChanged($e) {
    this.recalculateOrder.next(true);
  }

  addItem() {
    let item = new OrderItem();
    item.RecordStatus = "ACT";
    this.order.OrderItems.push(item);
    this.assignItemCount();
  }

  delete(item) {
    item.RecordStatus = "DEL";
    this.recalculateOrder.next(true);
  }

  assignItemCount() {
    if (this.order.OrderItems) {
      _.forEach(this.order.OrderItems, i => {
        if (!i.ItemCount || i.ItemCount <= 0) i.ItemCount = this.itemCount++;
      });
    }
  }

  public calculateOrder() {
    if (!this.order) return;

    this.order.SubTotal = 0;
    _.forEach(this.order.OrderItems, orderItem => {
      if (orderItem.RecordStatus == "ACT") {
        orderItem.ItemsSold = orderItem.ItemsOrdered - orderItem.ItemsReturned;
        let discountedItemPrice =
          orderItem.ItemPrice * (1 - this.order.DiscountPercent / 100);
        orderItem.DiscountPrice = this.round2Decimal(discountedItemPrice);
        orderItem.SubTotal = orderItem.DiscountPrice * orderItem.ItemsSold;
        this.order.SubTotal += orderItem.SubTotal;
      }
    });

    // let discountAmount = 0;
    // if (this.order.DiscountAmount > 0)
    //   discountAmount = this.order.DiscountAmount;
    // else if (this.order.DiscountPercent > 0)
    //   discountAmount = this.order.SubTotal * (this.order.DiscountPercent / 100);

    this.order.TaxAmount = this.order.SubTotal * (this.order.TaxPercent / 100);
    this.order.Total = this.order.SubTotal + this.order.TaxAmount;
  }

  round2Decimal(number: number) {
    return Math.round(number * 100) / 100;
  }

  ngOnDestroy() {
    this.destroy.next(true);
  }
}
