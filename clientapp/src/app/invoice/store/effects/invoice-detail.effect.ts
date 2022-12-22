import { Injectable } from "@angular/core";
import { Actions, Effect } from "@ngrx/effects";
import { InvoiceService } from "../../invoice.service";
import * as invoiceAction from "../actions";
import { Observable, of } from "rxjs";
import { Action } from "@ngrx/store";
import { map, switchMap, catchError, tap } from "rxjs/operators";
import { AppMessageService } from "../../../shared/components/app-message/app-message.service";

@Injectable()
export class InvoiceDetialEffect {
  @Effect()
  public invoiceDetailLoad: Observable<Action> = this.actions
    .ofType(invoiceAction.INVOICE_DETAIL_LOAD)
    .pipe(
      map((action: invoiceAction.InvoiceDetailLoad) => action.payload),
      switchMap(payload => {
        return this.invoiceService.getOrderById(payload.Id).pipe(
          map(data => new invoiceAction.InvoiceDetailLoadSuccess(data)),
          catchError(error => {
            return of(new invoiceAction.InvoiceDetailLoadFail(error));
          })
        );
      })
    );

  @Effect()
  public invoiceDetailSave: Observable<Action> = this.actions
    .ofType(invoiceAction.INVOICE_DETAIL_SAVE)
    .pipe(
      map((action: invoiceAction.InvoiceDetailSave) => action.payload),
      switchMap(payload => {
        return this.invoiceService.saveOrder(payload).pipe(
          map(data => new invoiceAction.InvoiceDetailSaveSuccess(data)),
          catchError(error => {
            return of(new invoiceAction.InvoiceDetailSaveFail(error));
          })
        );
      })
    );

  @Effect({ dispatch: false })
  public invoiceDetailSaveSuccess: Observable<Action> = this.actions
    .ofType(invoiceAction.INVOICE_DETAIL_SAVE_SUCCESS)
    .pipe(
      tap((action: any) => {
        this.msgService.success("Invoice saved successfully.");
      })
    );

  @Effect({ dispatch: false })
  public invoiceDetailSaveFail: Observable<Action> = this.actions
    .ofType(invoiceAction.INVOICE_DETAIL_SAVE_FAIL)
    .pipe(
      tap((action: any) => {
        this.msgService.error("Save Invoice fail.");
      })
    );

  constructor(
    private actions: Actions,
    private invoiceService: InvoiceService,
    private msgService: AppMessageService
  ) {}
}
