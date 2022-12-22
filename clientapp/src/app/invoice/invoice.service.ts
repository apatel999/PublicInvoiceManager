import { Injectable } from "@angular/core";
import { CustomHttpService } from "../custom-http.service";
import { environment } from "../../environments/environment";
import { Order, SearchParam } from "./order.model";
import * as _ from "lodash";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable()
export class InvoiceService {
  constructor(private http: CustomHttpService) {}

  public getOrders(params: SearchParam): Observable<any> {
    let url = environment.api_server + "orders" + "?" + params.queryParams();
    return this.http.get(url).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  public getOrderById(orderId: number): Observable<any> {
    let url = environment.api_server + "orders/" + orderId;
    return this.http.get(url).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  public getEmptyOrder(storeId: number): Observable<Order> {
    let url =
      environment.api_server + "orders/store/" + storeId + "/empty-order";
    return this.http.get(url).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  public addOrder(order: Order): Observable<Order> {
    let url = environment.api_server + "orders";
    return this.http.post(url, order).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  public saveOrder(order: Order): Observable<Order> {
    let url = environment.api_server + "orders/" + order.Id;
    return this.http.put(url, order).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  public createWeeklyOrders(yearId: number, week: number): Observable<Order[]> {
    let url = `${environment.api_server}orders/createweeklyorders/${yearId}/${week}`;
    return this.http.get(url).pipe(
      map(result => {
        return result.json();
      })
    );
  }
}
