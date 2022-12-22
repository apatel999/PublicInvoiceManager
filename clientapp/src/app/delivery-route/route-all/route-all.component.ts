import { Component, OnInit, ViewChild, TemplateRef } from "@angular/core";
import { DeliveryRoute } from "../delivery-route.model";
import { DeliveryRouteService } from "../delivery-route.service";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Subject } from "rxjs";
import * as _ from "lodash";
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
  ValidatorFn
} from "@angular/forms";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import {
  takeUntil,
  debounceTime,
  tap,
  filter,
  map,
  mergeMap
} from "rxjs/operators";

@Component({
  selector: "app-route-all",
  templateUrl: "./route-all.component.html",
  styleUrls: ["./route-all.component.css"]
})
export class RouteAllComponent implements OnInit {
  private searchSubject: Subject<string> = new Subject();

  public form: FormGroup;
  private destroy: Subject<any> = new Subject<any>();
  public editMode: boolean = false;
  public createMode: boolean = false;
  public weekDays: Array<any> = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday"
  ];
  public rows: Array<any> = [];
  public cols: Array<any> = [];
  public model: DeliveryRoute = new DeliveryRoute();
  public showValidationErrors: boolean = false;

  @ViewChild("actionsTemplate") actionTemplate: TemplateRef<any>;
  @ViewChild("productionDayTemplate") productionDayTemplate: TemplateRef<any>;
  @ViewChild("deliveryDayTemplate") deliveryDayTemplate: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private modalService: NgbModal,
    private service: DeliveryRouteService,
    private msgService: AppMessageService
  ) {}

  ngOnInit() {
    this.setDataTable();
    this.initSearch();
  }

  private setDataTable() {
    this.cols = [
      { prop: "RouteNumber", name: "Route Number" },
      { prop: "Description", name: "Description" },
      {
        prop: "DeliveryDay",
        name: "Delivery Day",
        cellTemplate: this.deliveryDayTemplate
      },
      {
        prop: "ProductionDay",
        name: "Production Day",
        cellTemplate: this.productionDayTemplate
      },
      { prop: "Id", name: "", cellTemplate: this.actionTemplate }
    ];

    this.service.allRoutes.pipe(takeUntil(this.destroy)).subscribe(routes => {
      this.rows = routes;
    });
  }

  private initSearch() {
    this.searchSubject
      .pipe(
        debounceTime(500),
        tap(searchText => {
          if (!searchText || searchText.trim() == "") this.resetSearch();
          return searchText;
        }),
        filter(searchText => searchText && searchText.trim() != ""),
        map(searchText => searchText.toLowerCase()),
        mergeMap(searchText => {
          return this.service.allRoutes.pipe(
            map(routes => {
              let filteredRoutes = routes.filter(
                route =>
                  route.RouteNumber == searchText ||
                  route.Description.toLowerCase().indexOf(searchText) >= 0
              );
              return filteredRoutes;
            })
          );
        })
      )
      .subscribe(
        routes => {
          this.rows = routes;
        },
        error => console.log(error)
      );
  }

  public addRoute() {
    this.showValidationErrors = false;
    this.model = new DeliveryRoute();
    this.createMode = true;
  }

  edit(model: DeliveryRoute) {
    this.model = new DeliveryRoute();
    this.model = _.assign(this.model, model);
    this.editMode = true;
  }

  public save() {
    // this.model.DeliveryDate = this.model.DeliveryDateObject.year + "-" + this.model.DeliveryDateObject.month + "-" + this.model.DeliveryDateObject.day;
    if (this.model.Id > 0) {
      //update record
      this.service.save(this.model.Id, this.model).subscribe(
        next => {
          this.msgService.success("Record updated successfully");
          this.editMode = false;
        },
        error => {
          this.msgService.errorResponseHandler(error);
        }
      );
    } else {
      //add new record
      this.service.add(this.model).subscribe(
        next => {
          this.msgService.success("Record saved successfully");
          this.createMode = false;
        },
        error => {
          this.msgService.errorResponseHandler(error);
        }
      );
    }
  }

  public cancel() {
    this.createMode = false;
    this.editMode = false;
  }

  public searchChange(searchText: string) {
    this.searchSubject.next(searchText);
  }

  public resetSearch() {
    this.service.allRoutes.pipe(takeUntil(this.destroy)).subscribe(routes => {
      this.rows = routes;
    });
  }
  ngOnDestroy() {
    this.destroy.next(true);
  }
}
