import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from "rxjs";
import { WeeklyScheduleService } from "../../invoice/weekly-order-creator/weekly-schedule.service";
import { switchMap, takeWhile, map } from "rxjs/operators";
import { ReportService } from "../report.service";
import { SearchParam } from "../../invoice/order.model";


@Component({
  selector: 'sales-audit-by-route',
  templateUrl: './sales-audit-by-route.component.html',
  styleUrls: ['./sales-audit-by-route.component.css']
})
export class SalesAuditByRouteComponent implements OnInit {
  public route: any;
  public customerName: string;
  public yearList$: Observable<any>;
  public scheduleYear: any;
  public scheduleWeek: any;
  public report$: Observable<any>;
  public year: Subject<number> = new Subject();
  public searchParam: SearchParam;
  private subscriptionAlive: boolean = true;
  
  constructor(
    private weeklyScheduleService: WeeklyScheduleService,
    private reportService: ReportService
  ) {}

  ngOnInit() {
    this.searchParam = new SearchParam();
    this.yearList$ = this.weeklyScheduleService.loadYearList();
    this.year
      .pipe(
        switchMap(yearId => this.weeklyScheduleService.lastSavedWeek(yearId)),
        takeWhile(data => this.subscriptionAlive)
      )
      .subscribe(lastSavedWeek => (this.scheduleWeek = lastSavedWeek));
  }

  compareWithYearId(year1, year2) {
    return year1 && year2 && year1.Id === year2.Id;
  }

  yearChanged() {
    this.year.next(this.scheduleYear.Id);
  }

  loadReport() {
    if (!this.scheduleYear || !this.scheduleWeek || !this.route) return;

    let param = new SearchParam();
    param.Route = this.route;
    param.CustomerName = this.customerName;
    param.YearId = this.scheduleYear.Id;
    param.Week = this.scheduleWeek;
   
    this.report$ = this.reportService
      .loadSalesAuditByRoute(param)
      .pipe(
        map(data => {
          console.log(data);
          return data;
        })
      );
  }

  ngOnDestroy() {
    this.subscriptionAlive = false;
  }

}
