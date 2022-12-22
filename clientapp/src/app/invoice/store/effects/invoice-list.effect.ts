import { Injectable } from "@angular/core";
import { Actions, Effect } from "@ngrx/effects";
import { InvoiceService } from "../../invoice.service";
import * as invoiceAction from "../actions";
import { Observable, of } from "rxjs";
import { Action } from "@ngrx/store";
import { map, switchMap, catchError } from "rxjs/operators";
import { SearchParam } from "../../order.model";

@Injectable()
export class InvoiceListEffect {
  @Effect()
  public invoiceList: Observable<Action> = this.actions
    .ofType(invoiceAction.LOAD_INVOICE_LIST)
    .pipe(
      map((action: any) => action.payload),
      switchMap((payload: SearchParam) => {
        return this.invoiceService.getOrders(payload).pipe(
          map(data => new invoiceAction.LoadInvoiceListSuccess(data)),
          catchError(error => {
            return of(new invoiceAction.LoadInvoiceListFail(error));
          })
        );
      })
    );
  @Effect()
  public weeklyOrders: Observable<Action> = this.actions
    .ofType(invoiceAction.CREATE_WEEKLY_INVOICES)
    .pipe(
      map((action: any) => action.payload),
      switchMap((payload: any) => {
        return this.invoiceService
          .createWeeklyOrders(payload.yearId, payload.week)
          .pipe(
            map(data => {
              console.log(
                "public weeklyOrders: Observable<Action> : CreateWeeklyInvoicesSuccess"
              );
              return new invoiceAction.CreateWeeklyInvoicesSuccess(data);
            }),
            catchError(error => {
              console.log(
                "public weeklyOrders: Observable<Action> : CreateWeeklyInvoicesFail"
              );
              return of(new invoiceAction.CreateWeeklyInvoicesFail());
            })
          );
      })
    );

  constructor(
    private actions: Actions,
    private invoiceService: InvoiceService
  ) {}
}
