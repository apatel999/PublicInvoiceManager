import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { map } from "rxjs/operators";
import { HttpService } from "../shared/util/http.service";
import { Observable } from "rxjs";
import { SearchParam } from "../invoice/order.model";
@Injectable()
export class ReportService {
  private reportUrl = environment.api_server + "report/";

  constructor(private http: HttpService) {}

  loadSalesSummaryAudit(searchParam: SearchParam): Observable<any> {
    const url = `${environment.api_server}salesreport/sales-summary-by-route/${searchParam.YearId}/${searchParam.Week}`;
    return this.http.post(url, searchParam).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  loadSalesAuditByRoute(searchParam: SearchParam): Observable<any> {
    const url = `${environment.api_server}salesreport/sales-audit-by-route/${searchParam.YearId}/${searchParam.Week}`;
    return this.http.post(url, searchParam).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  loadSalesInsOuts(searchParam: SearchParam): Observable<any> {
    const url = `${environment.api_server}salesreport/insouts/${searchParam.YearId}/${searchParam.Week}`;
    return this.http.post(url, searchParam).pipe(
      map(result => {
        return result.json();
      })
    );
  }

  loadCustomerUnitsReport(searchParam: SearchParam){
    const url = `${environment.api_server}salesreport/customer-units/${searchParam.YearId}/${searchParam.Week}`;
    return this.http.post(url, searchParam).pipe(
      map(result => {
        return result.json();
      })
    );
  }

}
