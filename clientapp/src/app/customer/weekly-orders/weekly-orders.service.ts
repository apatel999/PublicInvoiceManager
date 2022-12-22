import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { CustomHttpService } from "../../custom-http.service";
import { map } from "rxjs/operators";

@Injectable()
export class WeeklyOrdersService {
  constructor(private http: CustomHttpService) {}

  public load(storeId: number): Observable<any> {
    let uri = environment.api_server + "stores/" + storeId + "/weeklyorder";
    return this.http.get(uri).pipe(map(res => res.json()));
  }

  public save(storeId: number, orderItem: any): Observable<any> {
    if (orderItem.Id > 0) {
      let uri =
        environment.api_server +
        "stores/" +
        storeId +
        "/weeklyorder/items/" +
        orderItem.Id;
      return this.http.put(uri, orderItem).pipe(map(res => res.json()));
    } else {
      let uri =
        environment.api_server + "stores/" + storeId + "/weeklyorder/items/";
      return this.http.post(uri, orderItem).pipe(map(res => res.json()));
    }
  }

  public remove(storeId, itemId) {
    let uri =
      environment.api_server +
      "stores/" +
      storeId +
      "/weeklyorder/items/" +
      itemId;
    return this.http.delete(uri);
  }
}
