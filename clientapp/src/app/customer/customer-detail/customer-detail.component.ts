import { Component, OnInit, ElementRef } from "@angular/core";
import { BillingInformation, Store } from "../BillinInformation.model";
import { AppVars } from "../../app-vars";
import { CustomerService } from "../customer.service";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { ActivatedRoute } from "@angular/router";
import * as _ from "lodash";
import { switchMap, filter, tap } from "rxjs/operators";

@Component({
  selector: "customer-detail",
  templateUrl: "./customer-detail.component.html",
  styleUrls: ["./customer-detail.component.css"]
})
export class CustomerDetailComponent implements OnInit {
  private billingInfoId: number;

  public model: BillingInformation;
  public billingInfoEditMode: boolean;
  public billingInfoCreateMode: boolean;
  public saveDisabled: boolean;

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
    this.route.parent.params
      .pipe(
        filter(params => +params["customerId"] > 0),
        switchMap(params => {
          return this.service.getCustomer(+params["customerId"]);
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
        result => {
          this.msgService.success("Record updated successfully");
          this.billingInfoEditMode = false;
        },
        error => {
          this.msgService.errorResponseHandler(error);
        }
      );
    } else {
      //add new record
      this.saveDisabled = true;
      this.service.addBillingInformation(this.model).subscribe(
        result => {
          this.msgService.success("Record saved successfully");
          this.billingInfoCreateMode = false;
          this.model = result;
          this.saveDisabled = false;
        },
        error => {
          this.msgService.errorResponseHandler(error);
          this.saveDisabled = false;
        }
      );
    }
  }
}
