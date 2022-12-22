import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { RouteAllComponent } from './route-all/route-all.component';
import { RoutenoValidationDirective } from './route-all/routeno-validation.directive';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { StoreModule } from '@ngrx/store';
import * as fromStore from './store';
import { EffectsModule } from '@ngrx/effects';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxDatatableModule,
    NgbModule,
    StoreModule.forFeature('deliveryRoutes', fromStore.reducers),
    EffectsModule.forFeature(fromStore.effects)
  ],
  declarations: [
    RouteAllComponent,
    RoutenoValidationDirective
  ],
  providers: []
})
export class DeliveryRouteModule { }
