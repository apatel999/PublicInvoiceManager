import { Injectable } from "@angular/core";
import { HttpService } from "../../shared/util/http.service";
import { environment } from "../../../environments/environment";
import { map } from "rxjs/operators";

@Injectable()
export class ProductionScheduleService {
  constructor(private http: HttpService) {}

  public loadYearList() {
    return this.http
      .get(`${environment.api_server}weekly-schedule/year-list`)
      .pipe(
        map(result => {
          return result.json();
        })
      );
  }

  public loadProductionSchedule(yearId: number, week: number) {
    const url = `${environment.api_server}production-schedule/${yearId}/${week}`;
    return this.http.get(url).pipe(
      map(result => {
        let data = result.json();
        return this.mapRouteOrderEntity(data);
      })
    );
  }

  private mapRouteOrderEntity(productionSchedules) {
    let mappedProducts = [];
    productionSchedules.Products.forEach(product => {
      let orderEntities = product.Orders.reduce((entities, order) => {
        entities[order.RouteId] = order;
        return entities;
      }, {});
      mappedProducts.push({ ...product, OrderEntities: orderEntities });
    });

    return {
      ...productionSchedules,
      Products: mappedProducts
    };
  }
}
