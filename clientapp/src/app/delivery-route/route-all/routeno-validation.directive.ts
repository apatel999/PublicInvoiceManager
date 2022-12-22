import { Directive, Input } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';
import * as _ from 'lodash';

@Directive({
  selector: '[routenoValidation]',
  providers: [{provide: NG_VALIDATORS, useExisting: RoutenoValidationDirective, multi: true}]
})
export class RoutenoValidationDirective  implements Validator{

@Input('routenoValidation') routes:Array<any>
  
  constructor() { }
  validate(control: AbstractControl): {[key: string]: any} {
    let idCtrl = control.parent.get("Id");
    let id = idCtrl? idCtrl.value : -1;
    let routeNumber = control.value
    let index = _.findIndex(this.routes, route=>route.RouteNumber == routeNumber && route.Id != id )
    if(index>=0)
      return {'routenoValidation': {value: control.value}}
    else  
      return null; 
  }

}
