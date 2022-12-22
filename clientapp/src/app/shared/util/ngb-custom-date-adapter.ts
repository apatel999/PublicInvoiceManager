import { Injectable } from "@angular/core";
import { NgbDateAdapter, NgbDateStruct } from "@ng-bootstrap/ng-bootstrap";

@Injectable()
export class NgbCustomDateAdapter extends NgbDateAdapter<Date> {

  fromModel(date: Date): NgbDateStruct {
    console.log("fromModel" + date);
    return (date && date.getFullYear) ? { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() } : null;
  }

  toModel(date: NgbDateStruct): Date {
    console.log("toModel" + date);
    return date ? new Date(date.year, date.month - 1, date.day) : null;
  }
}