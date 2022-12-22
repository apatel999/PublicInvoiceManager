import { DeliveryRoute } from '../delivery-route/delivery-route.model';

// export class SearchParam {
//     constructor(public text: string, public page: number) { }
// }

export class BillingInformation {
    public Id: number;
    public Name: string;
    public BillingGroupId: string;
    public PaymentType: string;
    public PercentDiscount: number;
    public Note: string;
    public HstNumber: string;
    public PstExempt: number;
    public Status: string;
    public Stores: Array<Store>;
    public Address1: string;
    public Address2: string;
    public City: string;
    public State: string;
    public Country: string;
    public PostalCode: string;
    public ContactPerson: string;
    public Phone: string;
    public Fax: string;
    public DateCreated: Date;
    public DateModified: Date;
}

export class Store {
    public Id: number;
    public RouteId: number;
    public RouteOrderNo: string;
    public StoreNumber: string;
    public Status: string;
    public BillingInformationId: number;
    public Address1: string;
    public Address2: string;
    public City: string;
    public State: string;
    public Country: string;
    public PostalCode: string;
    public ContactPerson: string;
    public Phone: string;
    public Fax: string;
    public BillingInformation: BillingInformation;
    public Route: DeliveryRoute;
    public DateCreated: Date;
    public DateModified: Date;
}

export class BillingStatement {
    public StatementItems: Array<BillingStatementItem>;
    public Year: Date;
    public Week: number;
}
export class BillingStatementItem {
    public StoreNumber: string;
    public InvoiceNumber: string;
    public InvoiceDate: Date;
    public SubTotal: number;
    public TaxAmount: number;
    public Total: number;
}