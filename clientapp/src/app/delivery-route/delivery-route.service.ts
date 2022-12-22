import { Injectable } from "@angular/core";
import { Response } from "@angular/http";
import { Observable, BehaviorSubject } from "rxjs";
import { DeliveryRoute } from "./delivery-route.model";
import { HttpService } from "../shared/util/http.service";
import { environment } from "../../environments/environment";
import * as _ from "lodash";
import { map } from "rxjs/operators";

@Injectable()
export class DeliveryRouteService {
  private _allRoutes: BehaviorSubject<Array<any>> = new BehaviorSubject([]);

  constructor(private http: HttpService) {
    this.load();
  }

  public get allRoutes(): Observable<Array<any>> {
    return this._allRoutes.asObservable();
  }
  public load() {
    return this.http
      .get(environment.api_server + "deliveryroutes")
      .pipe(
        map(result => {
          var data = result.json();
          _.forEach(data, route => {
            let temp = new Date(route.DeliveryDate);
            route.DeliveryDate = temp;
            route.DeliveryDateObject = {
              year: temp.getFullYear(),
              month: temp.getMonth() + 1,
              day: temp.getDate()
            };
          });
          this._allRoutes.next(data);
        })
      )
      .subscribe(
        next => {},
        error => {
          console.log(error);
        }
      );
  }

  public add(model: DeliveryRoute): Observable<any> {
    return this.http
      .post(environment.api_server + "deliveryroutes", model)
      .pipe(
        map(result => {
          let resultData = result.json();
          let data = this._allRoutes.getValue();
          data.push(resultData);
          this._allRoutes.next(data);
        })
      );
  }

  public save(id: number, model: DeliveryRoute): Observable<any> {
    return this.http
      .put(environment.api_server + "deliveryroutes/" + id, model)
      .pipe(
        map(result => {
          let resultData = result.json();
          let data = this._allRoutes.getValue();
          let index = _.findIndex(data, obj => obj.Id == id);
          if (index >= 0) {
            _.assign(data[index], model);
            this._allRoutes.next(data);
          }
        })
      );
  }

  public saveAll(model: Array<DeliveryRoute>): Observable<any> {
    return this.http
      .put(environment.api_server + "deliveryroutes/updateall", model)
      .pipe(
        map(result => {
          let resultData = result.json();
          return resultData;
        })
      );
  }

  public saveWeeklySchedule(model: Array<DeliveryRoute>): Observable<any> {
    return this.http
      .put(environment.api_server + "deliveryroutes/", model)
      .pipe(
        map(result => {
          let resultData = result.json();
          return resultData;
        })
      );
  }
}
