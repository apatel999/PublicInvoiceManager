import { Component, OnInit, ElementRef } from "@angular/core";
import { BillingInformation, Store } from "../BillinInformation.model";
import { AppVars } from "../../app-vars";
import { CustomerService } from "../customer.service";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { ActivatedRoute } from "@angular/router";
import * as _ from "lodash";
import { switchMap, filter } from "rxjs/operators";

@Component({
  selector: "customer-create",
  templateUrl: "./customer-create.component.html",
  styleUrls: ["./customer-create.component.css"]
})
export class CustomerCreateComponent implements OnInit {
  private billingInfoId: number;

  public model: BillingInformation;
  public billingInfoEditMode: boolean;
  public billingInfoCreateMode: boolean;

  constructor(
    private appVars: AppVars,
    private service: CustomerService,
    private msgService: AppMessageService,
    private route: ActivatedRoute
  ) {
    this.model = new BillingInformation();
    this.model.Status = "ACT";
  }

  ngOnInit() {
    this.route.params
      .pipe(
        filter(params => +params["id"] > 0),
        switchMap(params => {
          return this.service.getCustomer(+params["id"]);
        })
      )
      .subscribe(
        billingInfo => {
          this.model = billingInfo;
        },
        error => this.msgService.error("Error loading data.")
      );
  }

  customerTypeChanged(value) {
    this.model.PaymentType = value;
  }

  pstExemptChanged(value) {
    this.model.PstExempt = value;
  }
  keys(obj) {
    return Object.keys(obj);
  }

  public saveBillingInformation() {
    if (this.model.Id > 0) {
      //update record
      this.service.saveBillingInformation(this.model.Id, this.model).subscribe(
        next => {
          this.msgService.success("Record updated successfully");
          this.billingInfoEditMode = false;
        },
        error => {
          this.msgService.errorResponseHandler(error);
        }
      );
    } else {
      //add new record
      this.service.addBillingInformation(this.model).subscribe(
        next => {
          this.msgService.success("Record saved successfully");
          this.billingInfoCreateMode = false;
        },
        error => {
          this.msgService.errorResponseHandler(error);
        }
      );
    }
  }
}
