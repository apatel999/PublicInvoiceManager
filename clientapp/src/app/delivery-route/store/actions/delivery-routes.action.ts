import { Action } from '@ngrx/store';

export const LOAD_ROUTES = '[Routes] Loading';
export const LOAD_ROUTES_SUCCESS = '[Routes] Loading Success';
export const LOAD_ROUTES_FAIL = '[Routes] Loading Fail';
export const SAVE_ROUTES = '[Routes] Save';
export const SAVE_ROUTES_SUCCESS = '[Routes] Save Success';
export const SAVE_ROUTES_FAIL = '[Roures] Save Fail'
export class LoadDeliveryRoutes implements Action {
    readonly type = LOAD_ROUTES;
    constructor(public payload?: any) { }
}

export class LoadDeliveryRoutesSuccess implements Action {
    readonly type = LOAD_ROUTES_SUCCESS;
    constructor(public payload?: any) { }
}

export class LoadDeliveryRoutesFail implements Action {
    readonly type = LOAD_ROUTES_FAIL;
    constructor(public payload?: any) { }
}

export class SaveDeliveryRoutes implements Action {
    readonly type = SAVE_ROUTES;
    constructor(public payload?: any) { }
}

export class SaveDeliveryRoutesSuccess implements Action {
    readonly type = SAVE_ROUTES_SUCCESS;
    constructor(public payload?: any) { }
}

export class SaveDeliveryRoutesFail implements Action {
    readonly type = SAVE_ROUTES_FAIL;
    constructor(public payload?: any) { }
}

export type DeliveryRouteAction = LoadDeliveryRoutes | LoadDeliveryRoutesSuccess | LoadDeliveryRoutesFail | SaveDeliveryRoutes | SaveDeliveryRoutesSuccess | SaveDeliveryRoutesFail;