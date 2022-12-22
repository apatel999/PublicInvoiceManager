import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerAllComponent } from './customer-all/customer-all.component';
import { CustomerService } from './customer.service';

import { Ng2PaginationModule } from 'ng2-pagination';
import { DialogModule } from 'primeng/primeng';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomerCreateComponent } from './customer-create/customer-create.component';
import { ProductService } from '../product/product.service';

import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { StoreComponent } from './store/store.component';
import { CustomerDashboardComponent } from './customer-dashboard/customer-dashboard.component';
import { WeeklyOrdersComponent } from './weekly-orders/weekly-orders.component';
import { InvoicesComponent } from './invoices/invoices.component';
import { StoreDashboardComponent } from './store-dashboard/store-dashboard.component';
import { StoreDetailComponent } from './store-detail/store-detail.component';
import { StoreService } from "./store.service";
import { WeeklyOrdersService } from "./weekly-orders/weekly-orders.service";
import { StoreFormComponent } from './store-form/store-form.component';
import { BillingInfoFormComponent } from './billing-info-form/billing-info-form.component';
import { SharedModule } from "../shared/shared.module";
import { InvoiceModule } from "../invoice/invoice.module";
import { WeeklyBillingStatementComponent } from './weekly-billing-statement/weekly-billing-statement.component';
import { StoresAllComponent } from './stores-all/stores-all.component';


@NgModule({
  imports: [
    CommonModule,
    CustomerRoutingModule,
    Ng2PaginationModule,
    DialogModule,
    FormsModule,
    ReactiveFormsModule,
    NgxDatatableModule,
    SharedModule,
    InvoiceModule
  ],
  declarations: [
    CustomerAllComponent,
    CustomerDetailComponent,
    CustomerCreateComponent,
    StoreComponent,
    CustomerDashboardComponent,
    WeeklyOrdersComponent,
    InvoicesComponent,
    StoreDashboardComponent,
    StoreDetailComponent,
    StoreFormComponent,
    BillingInfoFormComponent,
    WeeklyBillingStatementComponent,
    StoresAllComponent
  ],
  exports: [],
  providers: [
    CustomerService,
    ProductService,
    StoreService,
    WeeklyOrdersService
  ]
})
export class CustomerModule {
}

