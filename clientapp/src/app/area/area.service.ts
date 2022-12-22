import { Injectable } from "@angular/core";
import { CustomHttpService } from "../custom-http.service";
import { environment } from "../../environments/environment";
import { map } from "rxjs/operators";

@Injectable()
export class AreaService {
  private areaUrl = environment.api_server + "area/";

  constructor(private http: CustomHttpService) {}

  createArea(data: any) {
    let url = this.areaUrl + "create/";
    return this.http.post(url, data).pipe(map(res => res.json()));
  }

  getAllArea() {
    let url = this.areaUrl + "all/";
    return this.http.get(url).pipe(map(res => res.json()));
  }

  setStatus(data: any) {
    let url = this.areaUrl + "status_change/";
    return this.http.put(url, data).pipe(map(res => res.json()));
  }

  getAreaById(id: any) {
    let url = this.areaUrl + "id/" + id;
    return this.http.get(url).pipe(map(res => res.json()));
  }

  updateArea(data: any) {
    let url = this.areaUrl + "update/";
    return this.http.put(url, data).pipe(map(res => res.json()));
  }
}
