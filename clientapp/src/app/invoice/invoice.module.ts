import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { StoreModule } from "@ngrx/store";
import { EffectsModule } from "@ngrx/effects";
import { InvoiceDetailComponent } from "./invoice-detail/invoice-detail.component";
import { InvoiceListComponent } from "./invoice-list/invoice-list.component";
import { InvoiceItemsComponent } from "./invoice-items/invoice-items.component";
import { FormsModule } from "@angular/forms";
import { ActiveItemsPipe } from "./invoice-items/active-items.pipe";
import { NewInvoiceComponent } from "./new-invoice/new-invoice.component";
import { InvoiceService } from "./invoice.service";
import { NgxDatatableModule } from "@swimlane/ngx-datatable";

import * as fromInvoice from "./store";
import { WeeklyScheduleComponent } from "./weekly-order-creator/weekly-schedule/weekly-schedule.component";
import { OrdersCreatorComponent } from "./weekly-order-creator/orders-creator/orders-creator.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { WeeklyScheduleService } from "./weekly-order-creator/weekly-schedule.service";
import { RouterModule } from "@angular/router";
import { InvoiceSearchComponent } from "./invoice-search/invoice-search.component";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxDatatableModule,
    NgbModule,
    StoreModule.forFeature("invoiceFeature", fromInvoice.reducers),
    EffectsModule.forFeature(fromInvoice.effects),
    RouterModule
  ],

  declarations: [
    InvoiceDetailComponent,
    InvoiceListComponent,
    InvoiceItemsComponent,
    ActiveItemsPipe,
    NewInvoiceComponent,
    WeeklyScheduleComponent,
    OrdersCreatorComponent,
    InvoiceSearchComponent
  ],
  exports: [InvoiceListComponent],
  providers: [InvoiceService, WeeklyScheduleService]
})
export class InvoiceModule {}
