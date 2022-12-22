import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subject } from "rxjs";
import { SearchParam } from "../../invoice/order.model";
import { WeeklyScheduleService } from "../../invoice/weekly-order-creator/weekly-schedule.service";
import { ReportService } from "../report.service";
import { switchMap, takeWhile, map } from "rxjs/operators";
import { ProductService } from "../../product/product.service";

@Component({
  selector: 'ins-outs',
  templateUrl: './ins-outs.component.html',
  styleUrls: ['./ins-outs.component.css']
})
export class InsOutsComponent implements OnInit, OnDestroy  {
  public route: any;
  public customerName: string;
  public yearList$: Observable<any>;
  public scheduleYear: any;
  public scheduleWeek: any;
  public report$: Observable<any>;
  public year: Subject<number> = new Subject();
  public searchParam: SearchParam;
  private subscriptionAlive: boolean = true;
  public products:any;

  constructor(
    private weeklyScheduleService: WeeklyScheduleService,
    private reportService: ReportService,
    private service: ProductService
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

     this.service.allProducts
    .pipe(
      takeWhile(data => this.subscriptionAlive),
      map(products => {
        return products.filter(p => p.RecordStatus == 'ACT')
      })
    ).subscribe(data => this.products = data);

  }
  compareWithYearId(year1, year2) {
    return year1 && year2 && year1.Id === year2.Id;
  }

  yearChanged() {
    this.year.next(this.scheduleYear.Id);
  }

  loadReport() {
    if (!this.scheduleYear && !this.scheduleWeek) return;

    let param = new SearchParam();
    param.Route = this.route;
    param.CustomerName = this.customerName;
    param.YearId = this.scheduleYear.Id;
    param.Week = this.scheduleWeek;

    this.report$ = this.reportService
      .loadSalesInsOuts(param)
      .pipe(
        map(data => {
          console.log(data);
          return data;
        })
      );
  }

  getProduct(orderdProducts, productId){
    if(orderdProducts && orderdProducts.length > 0)
    {
      return orderdProducts.find( item => item.Product.Id == productId)
    }
  }

  ngOnDestroy() {
    this.subscriptionAlive = false;
  }

}
