import { createSelector } from "@ngrx/store";
import * as fromReducer from "../reducers";

export const invoice = createSelector(
  fromReducer.invoiceFeatureState,
  (state: fromReducer.InvoiceState) => state.invoiceDetail
);
export const invoiceDetail = createSelector(invoice, invoice => invoice.data);
export const invoiceLoaded = createSelector(invoice, invoice => invoice.loaded);
export const invoiceLoading = createSelector(
  invoice,
  invoice => invoice.loading
);
