import {
  ActionReducerMap,
  createFeatureSelector,
  createSelector
} from "@ngrx/store";

import * as fromInvoice from "./invoice-list.reducer";
import * as fromInvoiceDetail from "./invoice-detail.reducer";

export { InvoiceDetailState } from "./invoice-detail.reducer";
export { InvoiceListState } from "./invoice-list.reducer";

export interface InvoiceState {
  invoiceList: fromInvoice.InvoiceListState;
  invoiceDetail: fromInvoiceDetail.InvoiceDetailState;
}

export const reducers: ActionReducerMap<InvoiceState> = {
  invoiceList: fromInvoice.reducer,
  invoiceDetail: fromInvoiceDetail.reducer
};

export const invoiceFeatureState = createFeatureSelector<InvoiceState>(
  "invoiceFeature"
);

//export const invoiceDetailState = (state: InvoiceState) => state.invoiceDetail;
//export const invoiceListState = (state: InvoiceState) => state.invoiceList;
