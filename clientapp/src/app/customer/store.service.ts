import { Injectable } from "@angular/core";
import { Observable, BehaviorSubject } from "rxjs";
import { environment } from "../../environments/environment";
import { BillingInformation, Store } from "./BillinInformation.model";
import { CustomHttpService } from "../custom-http.service";
import * as _ from "lodash";
import { StoreWeeklyOrder } from "./weekly-orders/store-weekly-order.model";
import { SearchParam } from "../shared/model/search-param";
import { skipWhile, map } from "rxjs/operators";

@Injectable()
export class StoreService {
  public _storeInfo: BehaviorSubject<Store> = new BehaviorSubject(null);
  public _weeklyOrder: BehaviorSubject<any> = new BehaviorSubject(null);

  constructor(private http: CustomHttpService) {}

  public get StoreInfo(): Observable<Store> {
    return this._storeInfo.pipe(skipWhile(value => value == null));
  }

  public get WeeklyOrder(): Observable<Store> {
    return this._weeklyOrder.pipe(skipWhile(value => value == null));
  }

  cacheInMemeory(id: number): Observable<Store> {
    return this.load(id).pipe(
      map(store => {
        this._storeInfo.next(store);
        return store;
      })
    );
  }

  load(id: number): Observable<Store> {
    let url = environment.api_server + "store/" + id;
    return this.http.get(url).pipe(
      map(result => {
        let store = new Store();
        store = _.assign(store, result.json());
        return store;
      })
    );
  }

  getAll(param: SearchParam): Observable<any> {
    let url = environment.api_server + "store" + "?" + param.quryParams();
    return this.http.get(url).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  public add(model: Store): Observable<any> {
    return this.http
      .post(environment.api_server + "store", model)
      .pipe(map(result => result.json()));
  }

  public save(id: number, model: Store): Observable<any> {
    return this.http
      .put(environment.api_server + "store/" + id, model)
      .pipe(map(result => {}));
  }
}
