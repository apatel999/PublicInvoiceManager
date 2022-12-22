import { Component, OnInit, ViewChild, TemplateRef } from "@angular/core";
import { ProductModel } from "../product.model";
import { ProductService } from "../product.service";
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
import { AppVars } from "../../app-vars";
import {
  take,
  takeUntil,
  debounceTime,
  filter,
  tap,
  map,
  mergeMap
} from "rxjs/operators";

@Component({
  selector: "app-product-all",
  templateUrl: "./product-all.component.html",
  styleUrls: ["./product-all.component.css"]
})
export class ProductAllComponent implements OnInit {
  private searchSubject: Subject<string> = new Subject();

  public form: FormGroup;
  private destroy: Subject<any> = new Subject<any>();
  public editMode: boolean = false;
  public createMode: boolean = false;

  public rows: Array<any> = [];
  public cols: Array<any> = [];
  public model: ProductModel = new ProductModel();
  public showValidationErrors: boolean = false;

  @ViewChild("actionsTemplate") actionTemplate: TemplateRef<any>;
  @ViewChild("deliveryDayTemplate") deliveryDayTemplate: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private modalService: NgbModal,
    private appVars: AppVars,
    private service: ProductService,
    private msgService: AppMessageService
  ) {}

  ngOnInit() {
    this.setDataTable();
    this.initSearch();
  }

  private setDataTable() {
    this.cols = [
      { prop: "Name", name: "Name" },
      { prop: "Price", name: "Price" },
      { prop: "RecordStatus", name: "Status" },
      { prop: "Id", name: "", cellTemplate: this.actionTemplate }
    ];

    this.service.allProducts
      .pipe(takeUntil(this.destroy))
      .subscribe(products => {
        this.rows = products;
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
          return this.service.allProducts.pipe(
            map(products => {
              let filteredValues = products.filter(
                product => product.Name.toLowerCase().indexOf(searchText) >= 0
              );
              return filteredValues;
            })
          );
        }),
        takeUntil(this.destroy)
      )
      .subscribe(
        products => {
          this.rows = products;
        },
        error => console.log(error)
      );
  }

  public addRecord() {
    this.showValidationErrors = false;
    this.model = new ProductModel();
    this.createMode = true;
  }

  edit(model: ProductModel) {
    this.model = new ProductModel();
    this.model = _.assign(this.model, model);
    this.editMode = true;
  }

  public save() {
    if (this.model.Id > 0) {
      //update record
      this.service.save(this.model.Id, this.model).subscribe(
        next => {
          this.msgService.success("Record updated successfully");
          this.editMode = false;
          this.loadProducts();
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
          this.loadProducts();
        },
        error => {
          this.msgService.errorResponseHandler(error);
        }
      );
    }
  }

  loadProducts() {
    this.service.allProducts.pipe(take(1)).subscribe(products => {
      this.rows = products;
    });
  }

  public cancel() {
    this.createMode = false;
    this.editMode = false;
  }

  public searchChange(searchText: string) {
    this.searchSubject.next(searchText);
  }

  public resetSearch() {
    this.service.allProducts.pipe(takeUntil(this.destroy)).subscribe(data => {
      this.rows = data;
    });
  }
  ngOnDestroy() {
    this.destroy.next(true);
  }

  keys(obj) {
    return Object.keys(obj);
  }
}
