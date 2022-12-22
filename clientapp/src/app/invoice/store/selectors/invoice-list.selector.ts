import { createSelector } from "@ngrx/store";
import * as fromInvoiceList from "../reducers/invoice-list.reducer";
import * as fromReducer from "../reducers";

export const invoiceState = createSelector(
  fromReducer.invoiceFeatureState,
  (state: fromReducer.InvoiceState) => state.invoiceList
);
export const invoiceList = createSelector(invoiceState, state => state.data);

export const navigationOrderIds = createSelector(
  invoiceList,
  (state, props) => {
    let previous = -1;
    let next = -1;
    if (props.invoiceId > 0 && state.OrderIds && state.OrderIds.length > 0) {
      const index = state.OrderIds.findIndex(id => id == props.invoiceId);
      previous = state.OrderIds[index - 1] ? state.OrderIds[index - 1] : -1;
      next = state.OrderIds[index + 1] ? state.OrderIds[index + 1] : -1;
    }
    return { previous, next };
  }
);

export const isCreatingWeeklyOrders = createSelector(
  invoiceState,
  state => state.creating
);
export const weeklyOrdersCreateFailed = createSelector(
  invoiceState,
  state => state.createFailed
);
export const weeklyOrdersCreateSuccess = createSelector(
  invoiceState,
  state => state.created
);
