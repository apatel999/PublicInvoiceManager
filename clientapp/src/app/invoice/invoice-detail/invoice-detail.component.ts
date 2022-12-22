import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Store } from "@ngrx/store";
import * as fromInvoice from "../store";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { Observable, of } from "rxjs";
import { Order } from "../order.model";
import { map, filter, tap, switchMap, toArray } from "rxjs/operators";
import { environment } from "../../../environments/environment";

@Component({
  selector: "app-invoice-detail",
  templateUrl: "./invoice-detail.component.html",
  styleUrls: ["./invoice-detail.component.css"]
})
export class InvoiceDetailComponent implements OnInit {
  public orderStatus: string = "";

  public orderDetail$: Observable<Order>;
  public navigationIds$: any;
  public orderLoaded$: Observable<any>;
  public dotmatrixUrl: string = "";
  public isEditMode: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private store: Store<fromInvoice.InvoiceState>,
    private msgService: AppMessageService
  ) {}

  ngOnInit() {
    this.orderDetail$ = this.store.select(fromInvoice.invoiceDetail).pipe(
      filter(invoiceDetail => !!invoiceDetail),
      map(invoiceDetail => {
        this.orderStatus = invoiceDetail.OrderStatus;
        return invoiceDetail;
      })
    );

    this.orderLoaded$ = this.store.select(fromInvoice.invoiceLoaded);

    //load store data
    this.route.params
      .pipe(
        tap(params => {
          if (params["action"] == "new" && params["storeId"] > 0) {
            //load store data
          } else if (params["invoiceId"] > 0) {
            //load invoice
            this.store.dispatch(
              new fromInvoice.InvoiceDetailLoad({ Id: params["invoiceId"] })
            );
            this.dotmatrixUrl =
              environment.api_server +
              "orders/" +
              params["invoiceId"] +
              ".html";

            this.navigationIds$ = this.store.select(
              fromInvoice.navigationOrderIds,
              {
                invoiceId: params["invoiceId"]
              }
            );
          } else {
            //invalid invoice id.
          }
        })
      )
      .subscribe(
        order => {},
        error => error => this.msgService.error("Error loading invoice.")
      );
  }

  opendotmatrixLayout() {
    window.open(this.dotmatrixUrl, "_blank", "width=800,height=800");
  }

  editOrder() {
    this.isEditMode = true;
  }

  saveOrder(order: Order) {
    order.OrderStatus = this.orderStatus;
    this.store.dispatch(new fromInvoice.InvoiceDetailSave(order));
  }
}
