import { Component, OnInit } from "@angular/core";
import { StoreWeeklyOrder } from "./store-weekly-order.model";
import { ProductService } from "../../product/product.service";
import { Subject, Observable, Subscription } from "rxjs";
import * as _ from "lodash";
import {
  NgbModal,
  ModalDismissReasons,
  NgbModalRef
} from "@ng-bootstrap/ng-bootstrap";
import { StoreService } from "../store.service";
import { WeeklyOrdersService } from "./weekly-orders.service";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { switchMap, takeUntil, take } from "rxjs/operators";

@Component({
  selector: "weekly-orders",
  templateUrl: "./weekly-orders.component.html",
  styleUrls: ["./weekly-orders.component.css"]
})
export class WeeklyOrdersComponent implements OnInit {
  private weeklyOrder: any;
  public records: Array<any> = [];
  private destroy: Subject<any> = new Subject();
  private modalFormRef: NgbModalRef;
  private formModel: any;
  public allProducts: any;
  productSubscription: Subscription;
  constructor(
    private productService: ProductService,
    private storeService: StoreService,
    private weeklyOrderService: WeeklyOrdersService,
    private msgService: AppMessageService,
    private modalService: NgbModal
  ) {}

  ngOnInit() {
     this.productService.allProducts
      .pipe(takeUntil(this.destroy))
      .subscribe(products => {
        this.allProducts = products;
      });

    this.loadWeeklyOrder();
  }

  loadWeeklyOrder() {
    this.storeService.StoreInfo
      .pipe(
        takeUntil(this.destroy),
        switchMap(store => {
          return this.weeklyOrderService.load(store.Id);
        })
      )
      .subscribe(order => {
        this.weeklyOrder = order;
        this.weeklyOrder.Items =
          this.weeklyOrder.Items && this.weeklyOrder.Items.length >= 0
            ? this.weeklyOrder.Items
            : [];
      });
  }

  add(template) {
    this.formModel = new StoreWeeklyOrder();
    this.modalFormRef = this.modalService.open(template);
  }

  edit(template, item) {
    this.formModel = item;
    this.modalFormRef = this.modalService.open(template);
  }

  remove(item) {
    this.storeService.StoreInfo
      .pipe(
        take(1),
        switchMap(store => {
          return this.weeklyOrderService.remove(store.Id, item.Id);
        })
      )
      .subscribe(
        result => {
          this.weeklyOrder.Items = _.filter(
            this.weeklyOrder.Items,
            i => i.Id != item.Id
          );
          this.msgService.success("Record deleted successfully");
        },
        error => this.msgService.errorResponseHandler(error)
      );
  }

  save() {
    this.storeService.StoreInfo
      .pipe(
        take(1),
        switchMap(store => {
          return this.weeklyOrderService.save(store.Id, this.formModel);
        })
      )
      .subscribe(
        result => {
          this.mergeItem(result);
          this.msgService.success("Record updated successfully");
        },
        error => this.msgService.errorResponseHandler(error)
      );

    this.modalFormRef.close();
  }

  mergeItem(mergeObj) {
    var index = _.findIndex(
      this.weeklyOrder.Items,
      elem => elem.Id && elem.Id === mergeObj.Id
    );
    if (index >= 0) {
      this.weeklyOrder.Items[index] = mergeObj;
    } else {
      this.weeklyOrder.Items.push(mergeObj);
    }
  }
}
