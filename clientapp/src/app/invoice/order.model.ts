import { Store } from "../customer/BillinInformation.model";
import * as _ from "lodash";

export class SearchParam {
  public Page: number;
  public Route: number;
  public InvoiceNumber: number;
  public OrderStatus: Array<string>;
  public CustomerName: string;
  public StoreId: number;
  public BillingInformationId: number;
  public YearId: number;
  public Week: number;

  constructor(
    {
      Page,
      Route,
      InvoiceNumber,
      OrderStatus,
      CustomerName,
      StoreId,
      BillingInformationId
    }: {
      Page?: number;
      Route?: number;
      InvoiceNumber?: number;
      OrderStatus?: Array<string>;
      CustomerName?: string;
      StoreId?: number;
      BillingInformationId?: number;
    } = {}
  ) {
    this.Page = Page;
    this.Route = Route;
    this.InvoiceNumber = InvoiceNumber;
    this.OrderStatus = OrderStatus;
    this.CustomerName = CustomerName;
    this.StoreId = StoreId;
    this.BillingInformationId = BillingInformationId;
  }

  public queryParams() {
    let str = "";
    str += "Page=" + this.Page;
    str += this.Route > 0 ? "&Route=" + this.Route : "";
    str += this.InvoiceNumber > 0 ? "&InvoiceNumber=" + this.InvoiceNumber : "";
    str += this.CustomerName ? "&CustomerName=" + this.CustomerName : "";
    str += this.StoreId > 0 ? "&StoreId=" + this.StoreId : "";
    str +=
      this.BillingInformationId > 0
        ? "&BillingInformationId=" + this.BillingInformationId
        : "";
    str += this.YearId > 0 ? "&YearId=" + this.YearId : "";
    str += this.Week > 0 ? "&Week=" + this.Week : "";

    if (this.OrderStatus && this.OrderStatus.length > 0) {
      _.forEach(this.OrderStatus, status => {
        str += "&OrderStatus=" + status;
      });
    }

    return str;
  }
}

export class Order {
  public Id: number;
  public InvoiceNumber: number;
  public StoreId: number;
  public CreateDate: string;
  public UpdateDate: string;
  public InvoiceDate: string;
  public TaxAmount: number;
  public DiscountPercent: number;
  public DiscountAmount: number;
  public OrderStatus: string;
  public RecordStatus: string;
  public Note: string;

  public OrderItems: Array<OrderItem>;
  public SubTotal: number;
  public TaxPercent: number;
  public Total: number;
  public StoreInfo: Store;
}

export class OrderItem {
  public Id: number = 0;
  public OrderId: number;
  public ProductId: number;
  public ItemsOrdered: number = 0;
  public ItemsReturned: number = 0;
  public ItemsSold: number = 0;
  public ItemPrice: number;
  public CreateDate: string;
  public UpdateDate: string;
  public RecordStatus: string;

  public SubTotal: number;
  public RegularPrice: number;
  public ProductName: string;
}
