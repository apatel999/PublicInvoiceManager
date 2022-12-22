import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { StoreService } from "../../customer/store.service";
import { Order, OrderItem } from "../order.model";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { Observable, of } from "rxjs";
import { InvoiceService } from "../invoice.service";
import { catchError, map, switchMap } from "rxjs/operators";
import { environment } from "../../../environments/environment";

@Component({
  selector: "app-new-invoice",
  templateUrl: "./new-invoice.component.html",
  styleUrls: ["./new-invoice.component.css"]
})
export class NewInvoiceComponent implements OnInit {
  public order: Order;
  public dotmatrixUrl: string;
  constructor(
    private route: ActivatedRoute,
    private msgService: AppMessageService,
    private invoiceService: InvoiceService
  ) {}

  ngOnInit() {
    this.route.params
      .pipe(
        switchMap(params => {
          if (params["storeId"] > 0) {
            let observable = this.invoiceService.getEmptyOrder(
              params["storeId"]
            );
            return observable;
          } else {
            //invalid store info id.
            return Observable.throw("Invalid store id");
          }
        })
      )
      .subscribe(
        emptyOrder => {
          this.order = emptyOrder;
          console.log(this.order);
        },
        error => this.msgService.error("Error loading store information.")
      );
  }

  addOrder() {
    if (this.order.Id > 0) {
      this.invoiceService
        .saveOrder(this.order)
        .pipe(
          map(savedOrder => {
            this.order = savedOrder;
            this.msgService.success("Order saved successfully");
            return savedOrder;
          }),
          catchError(error => {
            this.msgService.error("Error saving order.");
            console.log(error);
            return of(error);
          })
        )
        .subscribe();
    } else {
      this.invoiceService
        .addOrder(this.order)
        .pipe(
          map(savedOrder => {
            this.order = savedOrder;
            this.msgService.success("Order added successfully");
            return savedOrder;
          }),
          catchError(error => {
            this.msgService.error("Error saving order.");
            console.log(error);
            return of(error);
          })
        )
        .subscribe();
    }
  }

  opendotmatrixLayout() {
    if (this.order && this.order.Id > 0) {
      this.dotmatrixUrl =
        environment.api_server + "orders/" + this.order.Id + ".html";
      window.open(this.dotmatrixUrl, "_blank", "width=800,height=800");
    }
  }
}
