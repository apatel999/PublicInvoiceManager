import { Injectable } from "@angular/core";
import { Actions, Effect } from "@ngrx/effects";
import { Observable, of } from "rxjs";
import { Action } from "@ngrx/store";
import { map, switchMap, catchError, tap } from "rxjs/operators";
import { DeliveryRouteService } from "../../../delivery-route/delivery-route.service";
import * as fromAction from "../actions";

@Injectable()
export class DeliveryRoutesEffect {
  constructor(
    private actions: Actions,
    private routeService: DeliveryRouteService
  ) {}

  @Effect()
  loadRoutes$: Observable<Action> = this.actions
    .ofType(fromAction.LOAD_ROUTES)
    .pipe(
      map((action: fromAction.LoadDeliveryRoutes) => action.payload),
      switchMap((payload: any) => {
        return this.routeService.allRoutes.pipe(
          map(routes => new fromAction.LoadDeliveryRoutesSuccess(routes)),
          catchError(error => {
            return of(new fromAction.LoadDeliveryRoutesFail());
          })
        );
      })
    );

  @Effect({ dispatch: true })
  saveRoutes$: Observable<Action> = this.actions
    .ofType(fromAction.SAVE_ROUTES)
    .pipe(
      map((action: fromAction.SaveDeliveryRoutes) => {
        console.log(action);
        return action.payload;
      }),
      switchMap((payload: any) => {
        return this.routeService.saveAll(payload).pipe(
          map(routes => {
            if (routes.errors) return new fromAction.SaveDeliveryRoutesFail();
            return new fromAction.SaveDeliveryRoutesSuccess(routes);
          }),
          catchError(error => {
            return of(new fromAction.SaveDeliveryRoutesFail());
          })
        );
      })
    );
}
