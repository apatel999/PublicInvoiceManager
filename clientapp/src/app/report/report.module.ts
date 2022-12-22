import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Ng2PaginationModule } from 'ng2-pagination';
import { ProductionScheduleComponent } from './production-schedule/production-schedule.component';
import { FormsModule } from "@angular/forms";
import { ProductionScheduleService } from "./production-schedule/production-shedule.service";
import { SalesAuditComponent } from './sales-audit/sales-audit.component';
import { ReportService } from "./report.service";
import { SalesAuditByRouteComponent } from "./sales-audit-by-route/sales-audit-by-route.component";
import { InsOutsComponent } from './ins-outs/ins-outs.component';
import { CustomerUnitsComponent } from './customer-units/customer-units.component';

@NgModule({
  imports: [
    CommonModule,
    Ng2PaginationModule,
    FormsModule
  ],
  declarations: [
    ProductionScheduleComponent,
    SalesAuditComponent,
    SalesAuditByRouteComponent,
    InsOutsComponent,
    CustomerUnitsComponent
  ],
  exports: [],
  providers: [
    ProductionScheduleService,
    ReportService
  ]
})
export class ReportModule {
}

