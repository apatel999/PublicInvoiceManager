import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs";

@Injectable()
export class HttpService {
  constructor(private http: Http) {}

  public get(url: string) {
    return this.http.get(url);
  }

  public post(url: string, options: any) {
    return this.http.post(url, options);
  }

  public put(url: string, options: any) {
    return this.http.put(url, options);
  }
}
