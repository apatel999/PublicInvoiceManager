import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { DialogModule } from 'primeng/primeng';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductRoutingModule } from './product-routing.module';
import { ProductService } from './product.service';
import { ProductAllComponent } from './product-all/product-all.component';
import { NgxDatatableModule } from "@swimlane/ngx-datatable";


@NgModule({
    imports: [
        CommonModule,
        ProductRoutingModule,
        NgxDatatableModule,
        DialogModule,
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [
        ProductAllComponent],
    exports: [

    ],
    providers: [
        ProductService
    ]
})
export class ProductModule { }

