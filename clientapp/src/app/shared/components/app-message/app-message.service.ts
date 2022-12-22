import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class AppMessageService {
  public _message: BehaviorSubject<any> = new BehaviorSubject(null);

  constructor() {}

  public get message() {
    return this._message.asObservable();
  }

  public success(message: string) {
    let msgObj = { type: "success", msg: message };
    this._message.next({
      type: "success",
      msg: message,
      timeout: 7000,
      display: true
    });
  }

  public error(message: string) {
    let msgObj = { type: "danger", msg: message, timeout: 7000, display: true };
    this._message.next(msgObj);
  }

  errorResponseHandler(errorResp) {
    let error = errorResp.json();
    let str = "Operation failed: " + error.message;
    this.error(str);
  }
}
