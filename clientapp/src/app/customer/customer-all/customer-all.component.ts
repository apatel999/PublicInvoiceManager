import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { CustomerService } from "../customer.service";
import { Customer } from "../customer";
import { ProductService } from "../../product/product.service";
import { Area } from "../../area/area";
import { Subject, Observable, interval } from "rxjs";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import { SearchParam } from "../../shared/model/search-param";
import { filter, switchMap, take, map } from "rxjs/operators";

@Component({
  selector: "app-customer-all",
  templateUrl: "./customer-all.component.html",
  styleUrls: ["./customer-all.component.css"]
})
export class CustomerAllComponent implements OnInit {
  @ViewChild("customersTable") customersTable;
  @ViewChild("chkExpandAll") chkExpandAll: ElementRef;
  totalCustomerCount: number = 0;
  public paginator = 1;

  public rows: Array<any> = [];
  public cols: Array<any> = [];
  public expandRows: any;
  public searchParam: SearchParam;
  private page: Subject<number> = new Subject();
  private search: Subject<SearchParam> = new Subject();
  private loader: Subject<any> = new Subject();
  private pageNumber: number;
  private pageOffSet: number;
  private totalRows: number;

  constructor(
    private msgService: AppMessageService,
    private customerService: CustomerService,
    private productService: ProductService
  ) {}

  ngOnInit() {
    this.cols = [{ prop: "Name", name: "Customer Name" }];

    this.init();
  }

  init() {
    this.searchParam = new SearchParam();

    this.getAllCustomer(this.searchParam).subscribe(
      data => {},
      error => {
        this.msgService.error("Error loading data");
      }
    );

    this.search
      .pipe(
        filter(searchParam => searchParam != null),
        switchMap(searchParam => {
          interval(300)
            .pipe(take(1))
            .subscribe(t => this.expandAllRows(!!searchParam)); //bad practice refactor.
          searchParam.Page = 0;
          return this.getAllCustomer(searchParam);
        })
      )
      .subscribe(
        data => {},
        error => {
          this.msgService.error("Error loading data");
        }
      );

    this.page
      //.withLatestFrom(this.search, (pageNumber, searchText) => { return new SearchParam(searchText, pageNumber) })
      .pipe(
        map(p => {
          this.searchParam.Page = p;
          return this.searchParam;
        }),
        switchMap(param => {
          return this.getAllCustomer(param);
        })
      )
      .subscribe(
        data => {},
        error => {
          this.msgService.error("Error loading data");
        }
      );
  }

  getAllCustomer(param: SearchParam) {
    return this.customerService.getAllCustomers(param).pipe(
      map(data => {
        this.totalRows = data.Total;
        this.rows = data.Records;
        return data;
      })
    );
  }

  getCustomerDetails(id) {
    this.customerService.getCustomerDetails(id).subscribe(
      res => {},
      err => {
        console.log(err);
      }
    );
  }

  pageChange(pageInfo) {
    this.pageNumber = pageInfo.offset;
    this.page.next(this.pageNumber);
  }

  expandAllRows(expand: boolean) {
    if (expand) {
      this.chkExpandAll.nativeElement.checked = expand;
      this.customersTable.rowDetail.expandAllRows();
    } else {
      this.chkExpandAll.nativeElement.checked = expand;
      this.customersTable.rowDetail.colopseAllRows();
    }
  }

  toggleExpandRow(row) {
    this.customersTable.rowDetail.toggleExpandRow(row);
  }

  getRowHeight(row) {
    if (row.Stores && row.Stores.length > 0) {
      return row.Stores.length * 50;
    }
  }

  onSearchChange(searchParam) {
    this.search.next(searchParam);
  }
  onSearchReset() {
    this.searchParam = new SearchParam();
    this.search.next(this.searchParam);
  }
}
