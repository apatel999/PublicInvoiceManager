import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Store } from "../BillinInformation.model";
import { StoreService } from "../store.service";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { AppVars } from "../../app-vars";
import { DeliveryRouteService } from "../../delivery-route/delivery-route.service";
import { tap } from "rxjs/operators";

@Component({
  selector: "store-form",
  templateUrl: "./store-form.component.html",
  styleUrls: ["./store-form.component.css"]
})
export class StoreFormComponent implements OnInit {
  @Input("model") storeModel: Store;
  @Input("enableCancel") enableCancel: boolean;
  @Output("onSaved") onSaved = new EventEmitter<Store>();
  @Output("onAdded") onAdded = new EventEmitter<Store>();
  @Output("cancelClicked") onCancelClicked = new EventEmitter();

  public saveDisabled: boolean;

  constructor(
    private service: StoreService,
    private appVars: AppVars,
    private deliveryRouteService: DeliveryRouteService,
    private msgService: AppMessageService
  ) {}

  ngOnInit() {}

  keys(obj) {
    return Object.keys(obj);
  }

  public saveStore() {
    if (this.storeModel.Id > 0) {
      //update record
      this.service.save(this.storeModel.Id, this.storeModel).subscribe(
        data => {
          this.msgService.success("Record updated successfully");
          this.onSaved.emit(data);
        },
        error => {
          this.msgService.errorResponseHandler(error);
        }
      );
    } else {
      //add new record
      this.saveDisabled = true;
      this.service.add(this.storeModel).subscribe(
        data => {
          this.msgService.success("Record added successfully");
          this.onAdded.emit(data);
          this.saveDisabled = false;
        },
        error => {
          this.msgService.errorResponseHandler(error);
          this.saveDisabled = false;
        }
      );
    }
  }

  cancel() {
    this.onCancelClicked.emit();
  }
}
