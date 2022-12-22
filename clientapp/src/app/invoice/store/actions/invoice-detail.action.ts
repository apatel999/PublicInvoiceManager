import { Order } from "../../order.model";
import { Action } from "@ngrx/store";


export const INVOICE_DETAIL_LOAD = '[invoice] invoice detail load';
export const INVOICE_DETAIL_LOAD_SUCCESS = '[invoice] invoice detail load success';
export const INVOICE_DETAIL_LOAD_FAIL = '[invoice] invoice detail load fail';
export const INVOICE_DETAIL_SAVE = '[invoice] invoice detail save';
export const INVOICE_DETAIL_SAVE_SUCCESS = '[invoice] invoice detail save success';
export const INVOICE_DETAIL_SAVE_FAIL = '[invoice] invoice detail save fail';

export class InvoiceDetailLoad implements Action {
    public type = INVOICE_DETAIL_LOAD
    constructor(public payload?: any) { }
}

export class InvoiceDetailLoadSuccess implements Action {
    public type = INVOICE_DETAIL_LOAD_SUCCESS
    constructor(public payload?: Order) { }
}

export class InvoiceDetailLoadFail implements Action {
    public type = INVOICE_DETAIL_LOAD_FAIL
    constructor(public payload?: Order) { }
}

export class InvoiceDetailSave implements Action {
    public type = INVOICE_DETAIL_SAVE
    constructor(public payload?: any) { }
}

export class InvoiceDetailSaveSuccess implements Action {
    public type = INVOICE_DETAIL_SAVE_SUCCESS
    constructor(public payload?: Order) { }
}

export class InvoiceDetailSaveFail implements Action {
    public type = INVOICE_DETAIL_SAVE_FAIL
    constructor(public payload?: Order) { }
}


export type InvoiceDetailAction = InvoiceDetailLoad | InvoiceDetailLoadSuccess | InvoiceDetailLoadFail | InvoiceDetailSave | InvoiceDetailSaveSuccess | InvoiceDetailSaveFail;
