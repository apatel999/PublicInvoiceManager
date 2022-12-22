import { Component, OnInit } from "@angular/core";
import { SearchParam } from "../../invoice/order.model";
import { ActivatedRoute } from "@angular/router";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { StoreService } from "../store.service";
import { Store } from "../BillinInformation.model";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Component({
  selector: "app-invoices",
  templateUrl: "./invoices.component.html",
  styleUrls: ["./invoices.component.css"]
})
export class InvoicesComponent implements OnInit {
  public orderType_ORDER: boolean = false;
  public orderType_INVOICE: boolean = false;
  public searchParam: SearchParam;
  public searchParam$: Observable<SearchParam>;

  constructor(
    private route: ActivatedRoute,
    private storeService: StoreService,
    private msgService: AppMessageService
  ) {}

  ngOnInit() {
    //get storeid or bilinginfoid
    this.searchParam$ = this.route.parent.params.pipe(
      map(params => {
        let searchParam = new SearchParam();
        if (params && params["customerId"]) {
          searchParam.BillingInformationId = params["customerId"];
          this.searchParam = searchParam;
          return searchParam;
        } else if (params && params["id"]) {
          searchParam.StoreId = params["id"];
          this.searchParam = searchParam;
          return searchParam;
        }

        return null;
      })
    );
  }

  addOrderType($event) {
    let param = new SearchParam();
    param = Object.assign(param, this.searchParam);
    param.OrderStatus = [];
    if (this.orderType_ORDER) param.OrderStatus.push("ORDER");
    if (this.orderType_INVOICE) param.OrderStatus.push("INVOICE");

    this.searchParam = param;
  }
}
