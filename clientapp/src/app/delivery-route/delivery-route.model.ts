export class DeliveryRoute {
    public Id: number;
    public RouteNumber: number;
    public Description: string;
    public DeliveryDate: Date;
    public ProductionDay: number;
    public RecordCreateDate: Date;
    public RecordUpdateDate: Date;
    public DeliveryDateObject: any; // store object specifict to date picker {year: 2018, month: 2, day: 23}
}
