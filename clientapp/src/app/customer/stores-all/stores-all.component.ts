import { Component, OnInit, OnDestroy } from "@angular/core";
import { Subject } from "rxjs";
import { SearchParam } from "../../shared/model/search-param";
import { StoreService } from "../store.service";
import { AppMessageService } from "../../shared/components/app-message/app-message.service";
import {
  debounceTime,
  filter,
  switchMap,
  map,
  takeWhile
} from "rxjs/operators";

@Component({
  selector: "stores",
  templateUrl: "./stores-all.component.html",
  styleUrls: ["./stores-all.component.css"]
})
export class StoresAllComponent implements OnInit, OnDestroy {
  private totalRows: number;
  public rows: Array<any> = [];

  public searchParam: SearchParam;
  public search: Subject<SearchParam> = new Subject();
  private delayedSearch = this.search.pipe(debounceTime(300));
  private page: Subject<number> = new Subject();
  private subscriptionAlive: boolean = true;

  constructor(
    private msgService: AppMessageService,
    private storeService: StoreService
  ) {}

  ngOnInit() {
    this.init();
  }

  init() {
    this.searchParam = new SearchParam();

    this.getAllStores(this.searchParam)
      .pipe(takeWhile(data => this.subscriptionAlive))
      .subscribe(
        data => {},
        error => {
          this.msgService.error("Error loading data");
        }
      );

    this.delayedSearch
      .pipe(
        filter(searchParam => searchParam != null),
        switchMap(searchParam => {
          searchParam.Page = 0;
          this.searchParam = searchParam;
          return this.getAllStores(searchParam);
        })
      )
      .subscribe(
        data => {},
        error => {
          this.msgService.error("Error loading data");
        }
      );

    //.withLatestFrom(this.search, (pageNumber, searchText) => { return new SearchParam(searchText, pageNumber) })
    this.page
      .pipe(
        map(p => {
          this.searchParam.Page = p;
          return this.searchParam;
        }),
        switchMap(param => {
          return this.getAllStores(param);
        })
      )
      .subscribe(
        data => {},
        error => {
          this.msgService.error("Error loading data");
        }
      );
  }

  getAllStores(param: SearchParam) {
    return this.storeService.getAll(param).pipe(
      map(data => {
        this.totalRows = data.Total;
        this.rows = data.Records;
        return data;
      })
    );
  }

  onSearchChange(searchParam) {
    this.search.next(searchParam);
  }

  pageChange(pageInfo) {
    this.page.next(pageInfo.offset);
  }

  onSearchReset() {
    this.search.next(new SearchParam());
  }

  ngOnDestroy() {
    this.subscriptionAlive = false;
  }
}
