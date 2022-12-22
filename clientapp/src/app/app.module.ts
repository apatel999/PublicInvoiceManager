import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { NgbModule, NgbAlertModule } from "@ng-bootstrap/ng-bootstrap";
import { AppComponent } from "./app.component";
import { GeneralModule } from "./general/general.module";
import { AppRoutingModule } from "./app-routing.module";

import { NgxDatatableModule } from "@swimlane/ngx-datatable";

import { StoreModule } from "@ngrx/store";
import { EffectsModule } from "@ngrx/effects";
import { StoreDevtoolsModule } from "@ngrx/store-devtools";
// services
import { AuthService } from "./auth.service";
import { CustomHttpService } from "./custom-http.service";
import {
  AutoCompleteModule,
  PickListModule,
  ConfirmDialogModule,
  SharedModule
} from "primeng/primeng";
import { HttpService } from "./shared/util/http.service";
import { ReactiveFormsModule } from "@angular/forms";
import { AppMessageService } from "./shared/components/app-message/app-message.service";
import { AppVars } from "./app-vars";
import { DeliveryRouteService } from "./delivery-route/delivery-route.service";
import { environment } from "../environments/environment";
import { LocationStrategy, HashLocationStrategy } from "@angular/common";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpModule,
    NgxDatatableModule,
    //user defines modules
    GeneralModule,
    AutoCompleteModule,
    PickListModule,
    ConfirmDialogModule,
    SharedModule,
    NgbModule.forRoot(),
    StoreModule.forRoot({}, {}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument(),
    //routing module
    AppRoutingModule
  ],
  exports: [NgxDatatableModule],
  providers: [
    AppVars,
    AuthService,
    CustomHttpService,
    HttpService,
    AppMessageService,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    DeliveryRouteService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
