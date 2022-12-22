import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { switchMap, takeWhile, map } from "rxjs/operators";
import { Subject } from "rxjs";
import { WeeklyScheduleService } from "../../invoice/weekly-order-creator/weekly-schedule.service";
import { ProductionScheduleService } from "../../report/production-schedule/production-shedule.service";
import { CustomerService } from "../customer.service";
import { ActivatedRoute } from "@angular/router";
import { saveAs } from "file-saver";

@Component({
  selector: "app-weekly-billing-statement",
  templateUrl: "./weekly-billing-statement.component.html",
  styleUrls: ["./weekly-billing-statement.component.css"]
})
export class WeeklyBillingStatementComponent implements OnInit {
  public yearList$: Observable<any>;
  public statement$: Observable<any>;
  public selectedYear: any;
  public selectedWeek: any;
  public year: Subject<number> = new Subject();
  public grandSubTotal: number;
  public grandTaxAmount: number;
  public grandTotal: number;

  private billingInformationId: number;
  private subscriptionAlive: boolean = true;

  constructor(
    private weeklyScheduleService: WeeklyScheduleService,
    private productionScheduleService: ProductionScheduleService,
    private customerService: CustomerService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.parent.params
      .pipe(
        map(params => {
          if (params && params["customerId"]) {
            this.billingInformationId = params["customerId"];
          }
        }),
        takeWhile(data => this.subscriptionAlive)
      )
      .subscribe();

    this.yearList$ = this.weeklyScheduleService.loadYearList();

    this.year
      .pipe(
        switchMap(yearId => this.weeklyScheduleService.lastSavedWeek(yearId)),
        takeWhile(data => this.subscriptionAlive)
      )
      .subscribe(lastSavedWeek => (this.selectedWeek = lastSavedWeek));
  }

  compareWithYearId(year1, year2) {
    return year1 && year2 && year1.Id === year2.Id;
  }
  yearChanged() {
    this.year.next(this.selectedYear.Id);
  }

  loadStatement() {
    this.grandSubTotal = 0;
    this.grandTaxAmount = 0;
    this.grandTotal = 0;

    this.statement$ = this.customerService
      .getWeeklyBillingStatement(
        this.billingInformationId,
        this.selectedYear.Id,
        this.selectedWeek
      )
      .pipe(
        map(statement => {
          if (statement && statement.StatementItems) {
            let grandSubTotal = 0;
            let grandTaxAmount = 0;
            let grandTotal = 0;
            statement.StatementItems.forEach(item => {
              grandSubTotal += item.SubTotal;
              grandTaxAmount += item.TaxAmount;
              grandTotal += item.Total;
            });
            this.grandSubTotal = grandSubTotal;
            this.grandTaxAmount = grandTaxAmount;
            this.grandTotal = grandTotal;
          }
          return statement;
        })
      );
  }

  downloadCSV(statement) {
    let data: string = this.generateCSV(statement);
    const blob = new Blob([data], { type: "text/csv" });

    let date = this.formatDate(statement.Year, "-");
    let fileName =
      this.billingInformationId + "-" + date + "-" + statement.Week + ".csv";
    saveAs(blob, fileName);
    // const url = window.URL.createObjectURL(blob);
    // window.open(url);
  }

  generateCSV(statement): string {
    let csv = "Store Number,Invoice,Date,NET,HST,Total\r\n";
    statement.StatementItems.map(item => {
      let csvLine =
        item.StoreNumber +
        "," +
        item.InvoiceNumber +
        "," +
        this.formatDate(item.CreateDate) +
        "," +
        this.formatCurrency(item.SubTotal) +
        "," +
        this.formatCurrency(item.TaxAmount) +
        "," +
        this.formatCurrency(item.Total);
      csv += csvLine + "\r\n";
    });

    return csv;
  }

  formatDate(strDate, seperator?) {
    seperator = seperator ? seperator : "/";
    let date = new Date(strDate);

    var year = date.getFullYear();

    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : "0" + month;

    var day = date.getDate().toString();
    day = day.length > 1 ? day : "0" + day;

    return day + seperator + month + seperator + year;
  }

  formatCurrency(amount) {
    return "$" + amount;
  }
}
