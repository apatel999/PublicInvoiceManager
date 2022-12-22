import { ProductModel } from "../../product/product.model";

export class StoreWeeklyOrder {
    public Id: number;
    public StoreId: number;
    public ProductId: number;
    public Quantity: number;
    public RecordStatus: string;
    //public Product: ProductModel;
}