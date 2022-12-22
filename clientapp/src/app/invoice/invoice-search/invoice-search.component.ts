import { Component, OnInit } from "@angular/core";
import { InvoiceService } from "../invoice.service";
import { Subject, BehaviorSubject } from "rxjs";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { SearchParam } from "../order.model";
import { Observable } from "rxjs";
import { WeeklyScheduleService } from "../weekly-order-creator/weekly-schedule.service";
import { tap, withLatestFrom, map, switchMap, takeWhile } from "rxjs/operators";
import { environment } from "../../../environments/environment";

@Component({
  selector: "invoice-search",
  templateUrl: "./invoice-search.component.html",
  styleUrls: ["./invoice-search.component.css"]
})
export class InvoiceSearchComponent implements OnInit {
  public route: any;
  public customerName: string;
  public orderType_ORDER: boolean = false;
  public orderType_INVOICE: boolean = false;
  public searchParam: SearchParam;
  public yearList: Array<any>;
  public selectedYear: any;
  public selectedWeek: number;
  public subscriptionAlive: boolean = true;

  constructor(
    private invoiceService: InvoiceService,
    private msgService: AppMessageService,
    private weeklyScheduleService: WeeklyScheduleService
  ) {}

  ngOnInit() {
    this.searchParam = null;
    this.weeklyScheduleService
      .loadYearList()
      .pipe(
        switchMap(yearList => {
          this.selectedYear = yearList.find(year => year.IsCurrentYear);
          this.yearList = yearList;
          return this.weeklyScheduleService.lastSavedWeek(this.selectedYear.Id);
        }),
        map(week => {
          this.selectedWeek = week;
          return week;
        }),
        takeWhile(data => this.subscriptionAlive)
      )
      .subscribe(data => {
        let param = new SearchParam();
        param.YearId = this.selectedYear.Id;
        param.Week = this.selectedWeek;
        this.searchParam = param;
      });
  }

  searchChange() {
    let param = new SearchParam();
    param.Route = this.route;
    param.CustomerName = this.customerName;
    param.YearId = this.selectedYear.Id;
    param.Week = this.selectedWeek;
    this.searchParam = param;
  }

  compareWithYearId(year1, year2) {
    return year1 && year2 && year1.Id === year2.Id;
  }

  yearChanged() {
    //this.year.next(this.selectedYear.Id);
  }

  addOrderType($event) {
    let param = new SearchParam();
    param = Object.assign(param, this.searchParam);
    param.OrderStatus = [];
    if (this.orderType_ORDER) param.OrderStatus.push("ORDER");
    if (this.orderType_INVOICE) param.OrderStatus.push("INVOICE");

    this.searchParam = param;
  }

  resetSearch() {
    this.route = null;
    this.customerName = null;
    this.orderType_ORDER = false;
    this.orderType_INVOICE = false;

    this.searchParam = new SearchParam();
  }

  donwloadDotmatrixOrders() {
    const url =
      environment.api_server +
      "orders/allSearchedDotmatrix.html?" +
      this.searchParam.queryParams();
    window.open(url, "_blank", "width=800,height=800");
  }
}
