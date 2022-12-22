import {
  Component,
  OnInit,
  ViewChild,
  TemplateRef,
  Output,
  EventEmitter,
  Input
} from "@angular/core";
import * as _ from "lodash";
import { WeeklyScheduleService } from "../weekly-schedule.service";
import { Observable, Subject } from "rxjs";
import { takeWhile, map, switchMap } from "rxjs/operators";
import { AppMessageService } from "../../../shared/components/app-message/app-message.service";

@Component({
  selector: "weekly-schedule",
  templateUrl: "./weekly-schedule.component.html",
  styleUrls: ["./weekly-schedule.component.css"]
})
export class WeeklyScheduleComponent implements OnInit {
  @Output() saved: EventEmitter<any> = new EventEmitter();

  @ViewChild("productionDayTemplate") productionDayTemplate: TemplateRef<any>;
  @ViewChild("deliveryDateTemplate") deliveryDateTemplate: TemplateRef<any>;
  @ViewChild("deliveryDateTextTemplate")
  deliveryDateTextTemplate: TemplateRef<any>;

  private subscriptionAlive: boolean = true;
  private scheduleYear: any;
  private scheduleWeek: any;
  public rows: Array<any> = [];
  public weekDays: Array<any> = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday"
  ];
  public cols: Array<any> = [];
  public yearList$: Observable<any>;
  public year: Subject<number> = new Subject();
  public showConfirmationPage: boolean;

  constructor(
    private weeklyScheduleService: WeeklyScheduleService,
    private msgService: AppMessageService
  ) {}

  ngOnInit() {
    this.yearList$ = this.weeklyScheduleService.loadYearList().pipe(
      map(yearList => {
        this.scheduleYear = yearList.find(year => year.IsCurrentYear);
        this.year.next(this.scheduleYear.Id);
        return yearList;
      })
    );

    this.year
      .pipe(
        switchMap(yearId => this.weeklyScheduleService.lastSavedWeek(yearId)),
        takeWhile(data => this.subscriptionAlive)
      )
      .subscribe(lastSavedWeek => (this.scheduleWeek = lastSavedWeek + 1));

    this.showConfirmationPage = false;
  }

  loadSchedules() {
    if (!this.scheduleYear || !this.scheduleWeek) return;

    this.weeklyScheduleService
      .loadWeeklySchedule(this.scheduleYear.Id, this.scheduleWeek)
      .pipe(takeWhile(data => this.subscriptionAlive))
      .subscribe(data => {
        this.setDataTable(data);
      });
  }

  private setDataTable(data) {
    this.cols = [
      { prop: "RouteNumber", name: "Route Number" },
      { prop: "Description", name: "Description" },
      {
        prop: "DeliveryDateObject",
        name: "Delivery Day",
        cellTemplate: this.deliveryDateTemplate
      },
      {
        prop: "ProductionDay",
        name: "Production Day",
        cellTemplate: this.productionDayTemplate
      }
    ];

    this.rows = [...data];
  }

  public continueClicked() {
    this.showConfirmationPage = true;
  }

  saveClick() {
    this.weeklyScheduleService
      .saveWeeklySchedule(this.rows)
      .pipe(takeWhile(data => this.subscriptionAlive))
      .subscribe(data => {
        this.saved.emit({
          yearId: this.scheduleYear.Id,
          week: this.scheduleWeek
        });
        this.msgService.success("Schedule saved successfully");
      });
  }

  backClick() {
    this.showConfirmationPage = false;
  }

  yearChanged() {
    this.year.next(this.scheduleYear.Id);
  }

  compareWithYearId(year1, year2) {
    return year1 && year2 && year1.Id === year2.Id;
  }
  ngOnDestroy() {
    this.subscriptionAlive = false;
  }
}
