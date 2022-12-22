import * as _ from 'lodash';

export class SearchParam {
    public Page: number;
    public Route: number;
    public InvoiceNumber: number;
    public OrderStatus: Array<string>;
    public CustomerName: string;
    constructor({
        Page,
        Route,
        InvoiceNumber,
        OrderStatus,
        CustomerName }: { Page?: number, Route?: number, InvoiceNumber?: number, OrderStatus?: Array<string>, CustomerName?: string } = {}) {
        this.Page = Page;
        this.Route = Route;
        this.InvoiceNumber = InvoiceNumber;
        this.OrderStatus = OrderStatus;
        this.CustomerName = CustomerName;
    }

    public quryParams() {
        let str = '';
        str += 'Page=' + this.Page;
        str += this.Route > 0 ? '&Route=' + this.Route : '';
        str += this.InvoiceNumber > 0 ? '&InvoiceNumber=' + this.InvoiceNumber : '';
        str += this.CustomerName ? '&CustomerName=' + this.CustomerName : '';

        if (this.OrderStatus && this.OrderStatus.length > 0) {
            _.forEach(this.OrderStatus, status => {
                str += '&OrderStatus=' + status;
            })
        }

        return str;
    }
}