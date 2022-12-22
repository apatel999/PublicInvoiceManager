import { Injectable } from "@angular/core";
import { CustomHttpService } from "../custom-http.service";
import { environment } from "../../environments/environment";
import {
  BillingInformation,
  Store,
  BillingStatement
} from "./BillinInformation.model";
import { Observable } from "rxjs";
import * as _ from "lodash";
import { SearchParam } from "../shared/model/search-param";
import { map } from "rxjs/operators";

@Injectable()
export class CustomerService {
  private customerUrl = environment.api_server + "customer/";

  constructor(private http: CustomHttpService) {}

  getAllCustomers(param: SearchParam) {
    let url =
      environment.api_server + "billinginformation" + "?" + param.quryParams();

    return this.http.get(url).pipe(map(result => result.json()));
  }

  getCustomer(id: number): Observable<BillingInformation> {
    let url = environment.api_server + "billinginformation/" + id;
    return this.http.get(url).pipe(
      map(result => {
        let billingInfo = new BillingInformation();
        billingInfo = _.assign(billingInfo, result.json());
        return billingInfo;
      })
    );
  }

  public addBillingInformation(model: BillingInformation): Observable<any> {
    return this.http
      .post(environment.api_server + "billinginformation", model)
      .pipe(map(result => result.json()));
  }

  public saveBillingInformation(
    id: number,
    model: BillingInformation
  ): Observable<any> {
    return this.http
      .put(environment.api_server + "billinginformation/" + id, model)
      .pipe(map(result => result.json()));
  }

  public getWeeklyBillingStatement(
    customerId: number,
    yearId: number,
    week: number
  ): Observable<BillingStatement> {
    const url = `${environment.api_server}billinginformation/${customerId}/weekly-billing-statement/${yearId}/${week}`;
    return this.http.get(url).pipe(map(result => result.json()));
  }

  //TODO: Duplicate in store.service
  public addStore(model: Store): Observable<any> {
    return this.http
      .post(environment.api_server + "store", model)
      .pipe(map(result => result.json()));
  }

  public saveStore(id: number, model: Store): Observable<any> {
    return this.http
      .put(environment.api_server + "store/" + id, model)
      .pipe(map(result => result.json()));
  }

  /************************ Need to be commented out ******************************/
  // globalSearch(data: any) {
  //   let url = this.customerUrl + "global-search/" + data;
  //   return this.http.get(url).pipe(map(res => res.json()));
  // }

  // setStatus(data: any) {
  //   let url = this.customerUrl + "status_change/";
  //   return this.http.put(url, data).pipe(map(res => res.json()));
  // }

  getCustomerDetails(id) {
    let url = this.customerUrl + "details/" + id;
    return this.http.get(url).pipe(map(res => res.json()));
  }

  // updateCustomer(data: any) {
  //   let url = this.customerUrl + "details/update";
  //   return this.http.put(url, data).pipe(map(res => res.json()));
  // }

  // createNewCustomer(data: any) {
  //   let url = this.customerUrl + "create/";
  //   return this.http.post(url, data).pipe(map(res => res.json()));
  // }

  // searchByUsername(data: any) {
  //   let url = this.customerUrl + "search/username/";
  //   return this.http.post(url, data).pipe(map(res => res.json()));
  // }

  // searchByMobileNumber(data: any) {
  //   let url = this.customerUrl + "search/mobile_number/";
  //   return this.http.post(url, data).pipe(map(res => res.json()));
  // }

  // searchByArea(data: any) {
  //   let url = this.customerUrl + "search/area/";
  //   return this.http.post(url, data).pipe(map(res => res.json()));
  // }

  // getCustomerByArea(data: any) {
  //   let url = this.customerUrl + "search/customerByArea/";
  //   return this.http.post(url, data).pipe(map(res => res.json()));
  // }

  // setCheckGenerateInvoice(data: any) {
  //   let url = this.customerUrl + "check_change_generate_invoice_monthly";
  //   return this.http.post(url, data).pipe(map(res => res.json()));
  // }

  // uploadFileContents(data: any) {
  //   let url = this.customerUrl + "upload-file-contents";
  //   return this.http.post(url, data).pipe(map(res => res.json()));
  // }

  // getTotalCustomerCount() {
  //   let url = this.customerUrl + "customer-count";
  //   return this.http.get(url).pipe(map(res => res.json()));
  // }

  // generateAutoInvoice(id) {
  //   let url = this.customerUrl + "generate-auto-invoice/" + id;
  //   return this.http.get(url).pipe(map(res => res.json()));
  // }

  // getAutoGenerateList() {
  //   let url = this.customerUrl + "get-auto-generate-list";
  //   return this.http.get(url).pipe(map(res => res.json()));
  // }
  /************************ Need to be commented out ******************************/
}
