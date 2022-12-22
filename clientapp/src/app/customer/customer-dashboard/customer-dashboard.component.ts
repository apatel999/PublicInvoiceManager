import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute, Router, NavigationEnd } from "@angular/router";
import { Subject, Observable } from "rxjs";
import { distinct, takeUntil, map } from "rxjs/operators";

@Component({
  selector: "customer-dashboard",
  templateUrl: "./customer-dashboard.component.html",
  styleUrls: ["./customer-dashboard.component.css"]
})
export class CustomerDashboardComponent implements OnInit, OnDestroy {
  public selectedTab: string = "";
  private destroy: Subject<any> = new Subject<any>();
  public hasCustomerId$: Observable<boolean>;
  constructor(private router: Router, private route: ActivatedRoute) {}

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

    this.hasCustomerId$ = this.route.params.pipe(
      map(params => {
        if (params && params["customerId"] && params["customerId"] > 0)
          return true;
        return false;
      })
    );
  }

  ngOnDestroy() {
    this.destroy.next(true);
  }
}
