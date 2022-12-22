import {
  ActionReducerMap,
  createFeatureSelector,
  createSelector
} from "@ngrx/store";

import * as fromRoutes from "./delivery-routes.reducer";

export interface RoutesState {
  deliveryRoutes: fromRoutes.DeliveryRoutesState;
}

export const reducers: ActionReducerMap<RoutesState> = {
  deliveryRoutes: fromRoutes.deliveryRoutesReducer
};

const deliveryReouteState = createFeatureSelector("deliveryRoutes");

export const getDeliveryRoutes = createSelector(
  deliveryReouteState,
  (state: RoutesState) =>
    state && state.deliveryRoutes ? state.deliveryRoutes.data : null
);
export const getDeliveryRoutesLoaded = createSelector(
  deliveryReouteState,
  (state: RoutesState) => state.deliveryRoutes.loaded
);
export const getDeliveryRoutesLoading = createSelector(
  deliveryReouteState,
  (state: RoutesState) => state.deliveryRoutes.loading
);
export const getDeliveryRoutesSaving = createSelector(
  deliveryReouteState,
  (state: RoutesState) => state.deliveryRoutes.saving
);
export const getDeliveryRoutesSaved = createSelector(
  deliveryReouteState,
  (state: RoutesState) =>
    state && state.deliveryRoutes ? state.deliveryRoutes.saved : false
);
