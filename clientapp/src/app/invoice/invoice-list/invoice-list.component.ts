import {
  Component,
  OnInit,
  SimpleChanges,
  OnChanges,
  Input
} from "@angular/core";
import { InvoiceService } from "../invoice.service";
import { Subject, BehaviorSubject } from "rxjs";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { SearchParam } from "../order.model";
import { withLatestFrom, filter, map } from "rxjs/operators";
import { Store } from "@ngrx/store";
import * as invoiceActoins from "../store/actions";
import * as selectors from "../store/selectors";
@Component({
  selector: "invoice-list",
  templateUrl: "./invoice-list.component.html",
  styleUrls: ["./invoice-list.component.css"]
})
export class InvoiceListComponent implements OnInit, OnChanges {
  public searchText: string;
  public rows: any;
  public cols: any;
  @Input("search-param") searchParam: SearchParam;

  private page: Subject<number> = new Subject();
  private search: BehaviorSubject<SearchParam> = new BehaviorSubject(
    new SearchParam()
  );
  private loader: Subject<any> = new Subject();
  private pageNumber: number = 0;
  private pageOffSet: number = 0;
  private totalRows: number = 0;

  constructor(
    private invoiceService: InvoiceService,
    private msgService: AppMessageService,
    private store: Store<any>
  ) {}

  ngOnInit() {
    this.search
      .pipe(
        filter(param => param != null),
        map(param => {
          param.Page = 0;
          //return this.invoiceService.getOrders(param);
          this.store.dispatch(new invoiceActoins.LoadInvoiceList(param));
          return param;
        })
      )
      .subscribe(
        data => {
          // this.setTableData(data);
        },
        error => {
          this.msgService.error("Error loading data");
        }
      );

    this.page
      .pipe(
        withLatestFrom(this.search, (pageNumber, param) => {
          let searchParam = param ? param : new SearchParam();
          searchParam.Page = pageNumber;
          this.store.dispatch(new invoiceActoins.LoadInvoiceList(searchParam));
          return searchParam;
        })
      )
      .subscribe(
        data => {
          //this.setTableData(data);
        },
        error => {
          this.msgService.error("Error loading data");
        }
      );

    this.store
      .select(selectors.invoiceList)
      .pipe(filter(data => data != null))
      .subscribe(data => {
        this.setTableData(data);
      });
  }

  ngOnChanges(changes: SimpleChanges) {
    let change = changes["searchParam"];
    if (change && change.currentValue) {
      this.search.next(change.currentValue);
    }
  }

  setTableData(data) {
    this.totalRows = data.Total;
    this.rows = data.Records;
  }

  pageChange(pageInfo) {
    this.pageNumber = pageInfo.offset;
    this.page.next(this.pageNumber);
  }
}
