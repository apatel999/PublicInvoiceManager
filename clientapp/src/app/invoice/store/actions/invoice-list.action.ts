import { Action } from "@ngrx/store";
import { SearchParam } from "../../../invoice/order.model";

export const INIT_CREATE_WEEKLY_INVOICES = "[Invoice List] init";
export const LOAD_INVOICE_LIST = "[Invoice list] Loading";
export const LOAD_INVOICE_LIST_SUCCESS = "[Invoice list] Loading Success";
export const LOAD_INVOICE_LIST_FAIL = "[Invoice list] Loading Fail";
export const CREATE_WEEKLY_INVOICES = "[Invoice list] create invoices";
export const CREATE_WEEKLY_INVOICES_SUCCESS =
  "[Invoice list] create invoices success";
export const CREATE_WEEKLY_INVOICES_FAIL =
  "[Invoice list] create invoices fail";

export class LoadInvoiceList implements Action {
  readonly type = LOAD_INVOICE_LIST;
  constructor(public payload: SearchParam) {}
}

export class LoadInvoiceListSuccess implements Action {
  readonly type = LOAD_INVOICE_LIST_SUCCESS;
  constructor(public payload: any) {}
}

export class LoadInvoiceListFail implements Action {
  readonly type = LOAD_INVOICE_LIST_FAIL;
  constructor(public payload: any) {}
}

export class InitCreateWeeklyInvoices implements Action {
  readonly type = INIT_CREATE_WEEKLY_INVOICES;
  constructor(public payload?: any) {}
}

export class CreateWeeklyInvoices implements Action {
  readonly type = CREATE_WEEKLY_INVOICES;
  constructor(public payload: any) {}
}

export class CreateWeeklyInvoicesSuccess implements Action {
  readonly type = CREATE_WEEKLY_INVOICES_SUCCESS;
  constructor(public payload: any) {}
}

export class CreateWeeklyInvoicesFail implements Action {
  readonly type = CREATE_WEEKLY_INVOICES_FAIL;
}

export type InvoiceListAction =
  | InitCreateWeeklyInvoices
  | LoadInvoiceList
  | LoadInvoiceListFail
  | LoadInvoiceListSuccess
  | CreateWeeklyInvoices
  | CreateWeeklyInvoicesSuccess
  | CreateWeeklyInvoicesFail;
