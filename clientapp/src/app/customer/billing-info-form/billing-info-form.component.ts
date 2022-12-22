import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BillingInformation } from '../BillinInformation.model';
import { AppVars } from '../../app-vars';
import { CustomerService } from '../customer.service';
import { AppMessageService } from '../../shared/components/app-message/app-message.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'billing-info-form',
  templateUrl: './billing-info-form.component.html',
  styleUrls: ['./billing-info-form.component.css']
})
export class BillingInfoFormComponent implements OnInit {

  @Input('editableForm') editableForm: boolean = false;
  @Input('model') model: BillingInformation;
  @Output('onSaved') onSaved = new EventEmitter<BillingInformation>();
  @Output('onAdded') onAdded = new EventEmitter<BillingInformation>();

  constructor(private appVars: AppVars,
    private service: CustomerService,
    private msgService: AppMessageService,
    private route: ActivatedRoute) { }

  ngOnInit() {
  }

  customerTypeChanged(value) {
    this.model.PaymentType = value;
  }

  pstExemptChanged(value) {
    this.model.PstExempt = value;
  }
  keys(obj) {
    return Object.keys(obj)
  }

  public saveBillingInformation() {
    if (this.model.Id > 0) {
      //update record
      this.service.saveBillingInformation(this.model.Id, this.model)
        .subscribe(
        data => {
          this.msgService.success("Record updated successfully");
          this.onSaved.emit(data);
        },
        error => {
          this.msgService.errorResponseHandler(error);
        });
    } else {
      //add new record
      this.service.addBillingInformation(this.model)
        .subscribe(data => {
          this.msgService.success("Record saved successfully");
          this.onAdded.emit(data);
        },
        error => {
          this.msgService.errorResponseHandler(error);
        });
    }
  }
}
