import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()
export class GeneralService {
  private displaySidebarSource = new Subject<boolean>();

  displaySidebarConfirmed$ = this.displaySidebarSource.asObservable();

  displaySidebar(status: boolean) {
    this.displaySidebarSource.next(status);
  }
}
