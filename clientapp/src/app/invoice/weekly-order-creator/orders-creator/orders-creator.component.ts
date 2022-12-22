import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import { map, takeWhile, tap } from 'rxjs/operators';
import { WeeklyScheduleService } from '../weekly-schedule.service';
import * as fromDeliveryRouteStore from '../../../delivery-route/store';
import * as fromInvoice from '../../store';
import { AppMessageService } from '../../../shared/components/app-message/app-message.service';

@Component({
  selector: 'orders-creator',
  templateUrl: './orders-creator.component.html',
  styleUrls: ['./orders-creator.component.css']
})
export class OrdersCreatorComponent implements OnInit {
  private subscriptionAlive: boolean = true;

  public rows: Array<any> = [];
  public isScheduleSaved: boolean;
  private scheduleYearId: number;
  private scheduleWeek: number;
  public creatingOrders$: Observable<boolean>
  public createOrderSuccess$: Observable<boolean>;
  public createOrderFail$: Observable<boolean>;
  public showCreateOrderButton = false;
  constructor(
    private invoiceStore: Store<fromInvoice.InvoiceState>,
    private msgService: AppMessageService
  ) { }

  ngOnInit() {
    this.isScheduleSaved = false;
    this.invoiceStore.dispatch(new fromInvoice.InitCreateWeeklyInvoices());
    this.creatingOrders$ = this.invoiceStore.select(fromInvoice.isCreatingWeeklyOrders);
    this.createOrderSuccess$ = this.invoiceStore.select(fromInvoice.weeklyOrdersCreateSuccess)
      .pipe(
      tap(isOrdersCreated => {
        if (isOrdersCreated) {
          this.msgService.success("Weekly Orders created successfully");
        }
        return isOrdersCreated;
      }));


    this.createOrderFail$ = this.invoiceStore.select(fromInvoice.weeklyOrdersCreateFailed)
      .pipe(
      tap(orderCreateFailed => {
        if (orderCreateFailed) {
          this.msgService.error("Failed to create Weekly Orders.");
        }
        return orderCreateFailed;
      }));


  }

  onWeeklyScheduleSaved(event) {
    this.scheduleYearId = event.yearId;
    this.scheduleWeek = event.week;
    this.isScheduleSaved = true;
    this.showCreateOrderButton = true;
  }

  createOrders() {
    this.showCreateOrderButton = false;
    this.invoiceStore.dispatch(new fromInvoice.CreateWeeklyInvoices({ yearId: this.scheduleYearId, week: this.scheduleWeek }));

  }

  onNgDestroy() {
    this.subscriptionAlive = false;
  }
}
