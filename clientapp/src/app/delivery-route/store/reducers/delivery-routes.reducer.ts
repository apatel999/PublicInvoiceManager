import { Action } from "@ngrx/store";
import * as fromActions from "../actions";

export interface DeliveryRoutesState {
  data: any;
  loading: boolean;
  loaded: boolean;
  saving: boolean;
  saved: boolean;
}

const initialState: DeliveryRoutesState = {
  data: null,
  loading: false,
  loaded: false,
  saving: false,
  saved: false
};

export function deliveryRoutesReducer(
  state: DeliveryRoutesState = initialState,
  action: fromActions.DeliveryRouteAction
): DeliveryRoutesState {
  switch (action.type) {
    case fromActions.LOAD_ROUTES: {
      return {
        ...state,
        loading: true
      };
    }
    case fromActions.LOAD_ROUTES_SUCCESS: {
      return {
        ...state,
        loading: false,
        loaded: true,
        data: action.payload
      };
    }

    case fromActions.LOAD_ROUTES_FAIL: {
      return {
        ...state,
        loading: false,
        loaded: false
      };
    }

    case fromActions.SAVE_ROUTES: {
      return {
        ...state,
        saving: true,
        saved: false
      };
    }

    case fromActions.SAVE_ROUTES_SUCCESS: {
      return {
        ...state,
        data: action.payload.data,
        saving: false,
        saved: true
      };
    }

    case fromActions.SAVE_ROUTES_FAIL: {
      return {
        ...state,
        saving: false,
        saved: false
      };
    }
  }
}
