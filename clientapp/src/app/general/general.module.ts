import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { GeneralRoutingModule } from "./general-routing.module";
import { AlertModule } from "ngx-bootstrap";
//own components
import { DashboardComponent } from "./dashboard/dashboard.component";
import { NavbarComponent } from "./navbar/navbar.component";
import { SidebarComponent } from "./sidebar/sidebar.component";
import { FooterComponent } from "./footer/footer.component";
// modules
import { HomeModule } from "../home/home.module";
import { CustomerModule } from "../customer/customer.module";
import { ProductModule } from "../product/product.module";
import { SettingsModule } from "../settings/settings.module";
import { ReportModule } from "../report/report.module";
import { GeneralService } from "./general.service";
import { DeliveryRouteModule } from "../delivery-route/delivery-route.module";
import { AppMessageComponent } from "../shared/components/app-message/app-message.component";
import { NgbModule, NgbDateAdapter } from "@ng-bootstrap/ng-bootstrap";
import { InvoiceModule } from "../invoice/invoice.module";
import { NgbCustomDateAdapter } from "../shared/util/ngb-custom-date-adapter";

@NgModule({
  imports: [
    CommonModule,
    HomeModule,
    GeneralRoutingModule,
    CustomerModule,
    ProductModule,
    InvoiceModule,
    SettingsModule,
    ReportModule,
    DeliveryRouteModule,
    NgbModule.forRoot()
  ],
  declarations: [
    DashboardComponent,
    NavbarComponent,
    SidebarComponent,
    FooterComponent,
    AppMessageComponent
  ],
  exports: [DashboardComponent, NgbModule],
  providers: [
    GeneralService,
    { provide: NgbDateAdapter, useClass: NgbCustomDateAdapter }
  ]
})
export class GeneralModule {}
