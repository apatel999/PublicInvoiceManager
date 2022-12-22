import { Injectable } from "@angular/core";
import { CustomHttpService } from "../custom-http.service";
import { environment } from "../../environments/environment";
import { map } from "rxjs/operators";

@Injectable()
export class HomeService {
  private homeUrl = environment.api_server + "home/";

  constructor(private http: CustomHttpService) {}

  buildPayDateCounter() {
    let url = this.homeUrl + "pay-date-counter/clean-build";
    return this.http.get(url).pipe(map(res => res.json()));
  }
}
