import { Component, OnInit } from "@angular/core";
import { AppMessageService } from "./app-message.service";
import { Observable, interval } from "rxjs";
import { take } from "rxjs/operators";

@Component({
  selector: "app-message",
  templateUrl: "./app-message.component.html",
  styleUrls: ["./app-message.component.css"]
})
export class AppMessageComponent implements OnInit {
  public alerts: Array<any> = [];
  constructor(private msgService: AppMessageService) {}

  ngOnInit() {
    this.msgService.message.subscribe(msg => {
      if (msg) {
        this.alerts.push(msg);
        this.setAlertClose(msg);
      }
    });
  }

  setAlertClose(alert) {
    alert.timeout = alert.timeout && alert.timeout > 0 ? alert.timeout : 5000;
    interval(alert.timeout)
      .pipe(take(1))
      .subscribe(count => {
        if (alert) alert.display = false;
      });
  }

  alertClosed(index) {
    let newArray = [];
    for (let i = 0; i < this.alerts.length; i++) {
      if (i != index) newArray.push(this.alerts[i]);
    }
    this.alerts = newArray;
  }
}
