import { Component, OnInit } from "@angular/core";
import { Subject } from "rxjs";
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";
import { StoreService } from "../store.service";
import { Store } from "../BillinInformation.model";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { distinct, takeUntil, filter, switchMap } from "rxjs/operators";

@Component({
  selector: "app-store-dashboard",
  templateUrl: "./store-dashboard.component.html",
  styleUrls: ["./store-dashboard.component.css"]
})
export class StoreDashboardComponent implements OnInit {
  public selectedTab: string = "";
  private destroy: Subject<any> = new Subject<any>();
  public model: Store;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private msgService: AppMessageService,
    private service: StoreService
  ) {}

  ngOnInit() {
    this.selectedTab = this.route.snapshot.firstChild.url[0].path;
    this.router.events
      .pipe(distinct(), takeUntil(this.destroy))
      .subscribe((e: NavigationEnd) => {
        this.selectedTab = e.url
          ? e.url.substring(e.url.lastIndexOf("/") + 1)
          : "";
        this.selectedTab = this.selectedTab.toLowerCase();
      });

    //load store data
    this.route.params
      .pipe(
        filter(params => +params["id"] > 0),
        switchMap(params => {
          return this.service.cacheInMemeory(+params["id"]);
        })
      )
      .subscribe(
        storeInfo => {
          this.model = storeInfo;
        },
        error => this.msgService.error("Error loading data.")
      );
  }

  ngOnDestroy() {
    this.destroy.next(true);
  }
}
