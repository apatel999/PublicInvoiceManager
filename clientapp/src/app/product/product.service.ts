import { Injectable } from "@angular/core";
import { Response } from "@angular/http";
import { Observable, BehaviorSubject } from "rxjs";
import { HttpService } from "../shared/util/http.service";
import { environment } from "../../environments/environment";
import * as _ from "lodash";
import { ProductModel } from "./product.model";
import { map } from "rxjs/operators";

@Injectable()
export class ProductService {
  private _allProducts: BehaviorSubject<Array<any>> = new BehaviorSubject([]);

  constructor(private http: HttpService) {
    this.load();
  }

  public get allProducts(): Observable<Array<any>> {
    //return this._allProducts.asObservable();
    return this.http.get(environment.api_server + "products").pipe(
      map(result => {
        var data = result.json();
        //this._allProducts.next(data);
        return data;
      })
    );
  }

  public get allActiveProducts(): Observable<Array<any>> {
    return this.allProducts.pipe(
      map(products => {
        console.log("products: ", products);
        return products.filter(item => item.RecordStatus == "ACT");
      })
    );
  }

  public load() {
    return this.http
      .get(environment.api_server + "products")
      .pipe(
        map(result => {
          var data = result.json();
          this._allProducts.next(data);
        })
      )
      .subscribe(
        next => {},
        error => {
          console.log(error);
        }
      );
  }

  public add(model: ProductModel): Observable<any> {
    return this.http.post(environment.api_server + "products", model).pipe(
      map(result => {
        let resultData = result.json();
        let data = this._allProducts.getValue();
        data.push(resultData);
        this._allProducts.next(data);
      })
    );
  }

  public save(id: number, model: ProductModel): Observable<any> {
    return this.http.put(environment.api_server + "products/" + id, model).pipe(
      map(result => {
        let resultData = result.json();
      })
    );
  }

  private productUrl = environment.api_server + "product/";

  getProductById(id: any) {
    let url = this.productUrl + "id/" + id;
    return this.http.get(url).pipe(map(res => res.json()));
  }
}
