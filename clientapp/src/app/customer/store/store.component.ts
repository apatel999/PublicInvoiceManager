import { Component, OnInit, Input, OnChanges } from "@angular/core";
import { Store, BillingInformation } from "../BillinInformation.model";
import { ActivatedRoute, Router } from "@angular/router";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { CustomerService } from "../customer.service";
import { AppVars } from "../../app-vars";
import * as _ from "lodash";
import { DeliveryRouteService } from "../../delivery-route/delivery-route.service";
import { filter } from "rxjs/operators";
import { SimpleChanges } from "@angular/core";

@Component({
  selector: "store",
  templateUrl: "./store.component.html"
})
export class StoreComponent implements OnInit, OnChanges {
  @Input("billingId") billingId: number;
  @Input("records") inputRecords: Array<any>;
  @Input("billingInformation") billingInformation: BillingInformation;
  public records: Array<any>;
  public storeModel: Store;
  public storeEditMode: boolean;
  public storeCreateMode: boolean;

  constructor(
    private appVars: AppVars,
    private service: CustomerService,
    private msgService: AppMessageService,
    private route: ActivatedRoute,
    private router: Router,
    private deliveryRouteService: DeliveryRouteService
  ) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes["inputRecords"]) {
      this.records = changes["inputRecords"].currentValue;
    }
  }

  ngOnInit() {
    this.route.params.pipe(filter(params => +params["storeId"] > 0)).subscribe(
      params => {
        if (!this.edit(+params["storeId"]))
          this.msgService.error("Record could not found.");
      },
      error => this.msgService.error("Error loading data.")
    );
  }

  keys(obj) {
    return Object.keys(obj);
  }

  add() {
    this.storeModel = new Store();
    this.storeModel.BillingInformationId = this.billingId;
    this.storeModel.Status = "ACT";
    this.storeEditMode = true;

    //Copy billing information address
    this.storeModel.Address1 = this.billingInformation.Address1;
    this.storeModel.Address2 = this.billingInformation.Address2;
    this.storeModel.City = this.billingInformation.City;
    this.storeModel.State = this.billingInformation.State;
    this.storeModel.PostalCode = this.billingInformation.PostalCode;
    this.storeModel.Country = this.billingInformation.Country;
    this.storeModel.Phone = this.billingInformation.Phone;
    this.storeModel.Fax = this.billingInformation.Fax;
  }

  edit(id: number): boolean {
    let index = _.findIndex(this.records, s => s.Id == id);
    if (index >= 0) {
      this.storeEditMode = true;
      this.storeModel = this.records[index];
      return true;
    } else {
      return false;
    }
  }

  // public saveStore() {
  //   if (this.storeModel.Id > 0) {
  //     //update record
  //     this.service.saveStore(this.storeModel.Id, this.storeModel).subscribe(
  //       data => {
  //         this.msgService.success("Record updated successfully");
  //         this.storeEditMode = false;
  //       },
  //       error => {
  //         this.msgService.errorResponseHandler(error);
  //       }
  //     );
  //   } else {
  //     //add new record
  //     this.service.addStore(this.storeModel).subscribe(
  //       data => {

  //         this.msgService.success("Record saved successfully");
  //         this.storeEditMode = false;
  //       },
  //       error => {
  //         this.msgService.errorResponseHandler(error);
  //       }
  //     );
  //   }
  // }

  cancelAddEdit() {
    this.storeEditMode = false;
  }

  addEdit(store) {
    if (!this.records) this.records = [];

    const index = this.records.findIndex(r => r.Id == store.Id);
    if (index >= 0) {
      this.records[index] = store;
    } else {
      this.records.push(store);
    }
    this.records = this.records.slice(0, this.records.length);
    this.cancelAddEdit();
  }
}
