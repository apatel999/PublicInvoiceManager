import { Component, OnInit, OnDestroy } from "@angular/core";
import { ProductionScheduleService } from "./production-shedule.service";
import { Subject, Observable } from "rxjs";
import { WeeklyScheduleService } from "../../invoice/weekly-order-creator/weekly-schedule.service";
import { takeWhile, map, switchMap } from "rxjs/operators";

@Component({
  selector: "app-production-schedule",
  templateUrl: "./production-schedule.component.html",
  styleUrls: ["./production-schedule.component.css"]
})
export class ProductionScheduleComponent implements OnInit, OnDestroy {
  public yearList$: Observable<any>;
  public scheduleYear: any;
  public scheduleWeek: any;
  public productionSchedule$: Observable<any>;
  public year: Subject<number> = new Subject();
  private subscriptionAlive: boolean = true;

  constructor(
    private weeklyScheduleService: WeeklyScheduleService,
    private productionScheduleService: ProductionScheduleService
  ) {}

  ngOnInit() {
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

  loadSchedules() {
    if (!this.scheduleYear || !this.scheduleWeek) return;

    this.productionSchedule$ = this.productionScheduleService
      .loadProductionSchedule(this.scheduleYear.Id, this.scheduleWeek)
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
