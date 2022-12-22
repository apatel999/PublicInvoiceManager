import { Injectable } from "@angular/core";
import { Response } from "@angular/http";
import { Observable, BehaviorSubject } from "rxjs";
import { HttpService } from "../../shared/util/http.service";
import { environment } from "../../../environments/environment";
import * as _ from "lodash";
import { WeeklySchedule } from "./weekly-schedule.model";
import { map } from "rxjs/operators";

@Injectable()
export class WeeklyScheduleService {
  private _allSchedules: BehaviorSubject<Array<any>> = new BehaviorSubject([]);

  constructor(private http: HttpService) {}

  public loadYearList() {
    return this.http
      .get(environment.api_server + "weekly-schedule/year-list")
      .pipe(
        map(result => {
          return result.json();
        })
      );
  }

  public lastSavedWeek(yearId: number) {
    const url = `${environment.api_server}weekly-schedule/${yearId}/last-saved-week`;
    return this.http.get(url).pipe(
      map(result => {
        return result.json().week;
      })
    );
  }

  public loadWeeklySchedule(yearId: number, week: number) {
    const url = `${environment.api_server}weekly-schedule/${yearId}/${week}`;
    return this.http.get(url).pipe(
      map(result => {
        var data = result.json();
        _.forEach(data, route => {
          let temp = new Date(route.DeliveryDate);
          route.DeliveryDate = temp;
          route.DeliveryDateObject = {
            year: temp.getFullYear(),
            month: temp.getMonth() + 1,
            day: temp.getDate()
          };
        });
        return data;
      })
    );
  }

  public saveWeeklySchedule(model: Array<WeeklySchedule>): Observable<any> {
    return this.http
      .put(`${environment.api_server}weekly-schedule`, model)
      .pipe(
        map(result => {
          let resultData = result.json();
          return resultData;
        })
      );
  }
}
