import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
// components
import { HomeBodyComponent } from "../home/home-body/home-body.component";

import { CustomerAllComponent } from "../customer/customer-all/customer-all.component";
import { CustomerDetailComponent } from "../customer/customer-detail/customer-detail.component";
import { ProductAllComponent } from "../product/product-all/product-all.component";
import { CustomerCreateComponent } from "../customer/customer-create/customer-create.component";
import { AreaCreateComponent } from "../area/area-create/area-create.component";

import { RouteAllComponent } from "../delivery-route/route-all/route-all.component";
import { CustomerDashboardComponent } from "../customer/customer-dashboard/customer-dashboard.component";
import { WeeklyOrdersComponent } from "../customer/weekly-orders/weekly-orders.component";
import { InvoicesComponent } from "../customer/invoices/invoices.component";
import { StoreDashboardComponent } from "../customer/store-dashboard/store-dashboard.component";
import { StoreDetailComponent } from "../customer/store-detail/store-detail.component";
import { InvoiceDetailComponent } from "../invoice/invoice-detail/invoice-detail.component";
import { NewInvoiceComponent } from "../invoice/new-invoice/new-invoice.component";
import { OrdersCreatorComponent } from "../invoice/weekly-order-creator/orders-creator/orders-creator.component";
import { InvoiceSearchComponent } from "../invoice/invoice-search/invoice-search.component";
import { ProductionScheduleComponent } from "../report/production-schedule/production-schedule.component";
import { SalesAuditComponent } from "../report/sales-audit/sales-audit.component";
import { WeeklyBillingStatementComponent } from "../customer/weekly-billing-statement/weekly-billing-statement.component";
import { StoresAllComponent } from "../customer/stores-all/stores-all.component";
import { SalesAuditByRouteComponent } from "../report/sales-audit-by-route/sales-audit-by-route.component";
import { InsOutsComponent } from "../report/ins-outs/ins-outs.component";
import { CustomerUnitsComponent } from "../report/customer-units/customer-units.component";


const generalRoutes = [
  {
    path: "home",
    component: HomeBodyComponent
  },
  {
    path: "customers/:customerId",
    component: CustomerDashboardComponent,
    children: [
      { path: "", redirectTo: "customer-detail", pathMatch: "full" },
      { path: "customer-detail", component: CustomerDetailComponent },
      { path: "invoices", component: InvoicesComponent },
      {
        path: "weekly-billing-statement",
        component: WeeklyBillingStatementComponent
      }
    ]
  },
  {
    path: "customers",
    component: CustomerAllComponent
  },
  {
    path: "stores/:storeId/invoices/new",
    component: NewInvoiceComponent
  },
  {
    path: "stores/:id",
    component: StoreDashboardComponent,
    children: [
      { path: "", redirectTo: "store-detail", pathMatch: "full" },
      { path: "store-detail", component: StoreDetailComponent },
      { path: "weekly-orders", component: WeeklyOrdersComponent },
      { path: "invoices", component: InvoicesComponent }
    ]
  },
  {
    path: "stores",
    component: StoresAllComponent
  },
  {
    path: "product",
    component: ProductAllComponent
  },
  {
    path: "invoices/:invoiceId",
    component: InvoiceDetailComponent
  },
  {
    path: "invoices",
    component: InvoiceSearchComponent
  },
  {
    path: "create-weekly-orders/schedule/new",
    component: OrdersCreatorComponent
  },
  {
    path: "delivery-route",
    component: RouteAllComponent
  },
  {
    path: "reports/production-schedule",
    component: ProductionScheduleComponent
  },
  {
    path: "reports/sales-audit",
    component: SalesAuditComponent
  },
  {
    path:"reports/sales-audit-by-route",
    component: SalesAuditByRouteComponent
  },
  {
    path:"reports/ins-outs",
    component: InsOutsComponent
  },
  {
    path:"reports/customer-units",
    component: CustomerUnitsComponent
  },
  {
    path: "",
    redirectTo: "home",
    pathMatch: "full"
  },
  {
    path: "**",
    redirectTo: "home",
    pathMatch: "full"
  }
];
@NgModule({
  imports: [RouterModule.forChild(generalRoutes)],
  exports: [RouterModule]
})
export class GeneralRoutingModule {}
