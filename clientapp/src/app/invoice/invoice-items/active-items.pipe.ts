import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'lodash';
@Pipe({
    name: 'activeItems',
    pure: false
})
export class ActiveItemsPipe implements PipeTransform {
    transform(items: any[]): any {
        if (!items) {
            return items;
        }
        return _.filter(items, i => i.RecordStatus != 'DEL');
    }
}