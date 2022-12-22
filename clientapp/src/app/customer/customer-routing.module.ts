import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CustomerDetailComponent } from "./customer-detail/customer-detail.component";

const customerRoutes = [
    {
        path: '',
        component: CustomerDetailComponent
    }
];
@NgModule({
    imports: [
        RouterModule.forChild(customerRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class CustomerRoutingModule { }
