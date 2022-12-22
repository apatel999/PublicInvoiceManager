using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace InvoiceManagerWeb.Print
{
    public class InvoiceTextReport2
    {
        private Order order;
        public InvoiceTextReport2(Order order)
        {
            this.order = order;
        }

        public string GetText()
        {
            var text = this.Header();
            text += this.InvoiceItems();
            text += this.Footer();
            return text;
        }

        private string replaceTemplateText(string template, string searchText, string replacement, bool alignRight = false)
        {
            if (alignRight)
            {
                return template.Replace(searchText, replacement.PadLeft(searchText.Length, ' '));
            }
            return template.Replace(searchText, replacement.PadRight(searchText.Length, ' '));
        }

        private string Header()
        {
            string value = "";
            string template = InvoiceTextReportTemplates2.InvoiceHeader();
            value = this.order?.InvoiceNumber > 0 ? this.order?.InvoiceNumber.ToString() : "";
            template = this.replaceTemplateText(template, "{InvoiceNo}", value, false);

            value = this.order != null ? this.order?.CreateDate.ToString("dd/MM/yyyy") : "";
            template = this.replaceTemplateText(template, "{Date    }", value, false);

            value = !string.IsNullOrEmpty(this.order?.StoreInfo?.BillingInformation?.Name) ? this.order?.StoreInfo?.BillingInformation?.Name : "";
            template = this.replaceTemplateText(template, "{CustomerName                           }", value, false);

            var address1 = "";
            if (!string.IsNullOrEmpty(this.order?.StoreInfo?.BillingInformation?.Address1)
               || !string.IsNullOrEmpty(this.order?.StoreInfo?.BillingInformation?.Address2)
               || !string.IsNullOrEmpty(this.order?.StoreInfo?.BillingInformation?.City)
             )
            {
                address1 = this.order?.StoreInfo?.BillingInformation?.Address1
                  + " " + this.order?.StoreInfo?.BillingInformation?.Address2
                  + ", " + this.order?.StoreInfo?.BillingInformation?.City;
            }
            template = this.replaceTemplateText(template, "{Address1                               }", address1, false);

            var address2 = "";
            if (!string.IsNullOrEmpty(this.order?.StoreInfo?.BillingInformation?.State)
                || !string.IsNullOrEmpty(this.order?.StoreInfo?.BillingInformation?.PostalCode))
            {
                address2 = this.order?.StoreInfo?.BillingInformation?.State + ", " + this.order?.StoreInfo?.BillingInformation?.PostalCode;
            }
            template = this.replaceTemplateText(template, "{Address2                               }", address2, false);


            var storeNumber = !string.IsNullOrEmpty(this.order?.StoreInfo?.StoreNumber)? this.order?.StoreInfo?.StoreNumber : "";
            template = this.replaceTemplateText(template, "{   StoreNumber}", storeNumber, true);

            address1 = "";
            if (!string.IsNullOrEmpty(this.order?.StoreInfo?.Address1)
               || !string.IsNullOrEmpty(this.order?.StoreInfo?.Address2)
               || !string.IsNullOrEmpty(this.order?.StoreInfo?.City)
             )
            {
                address1 = this.order?.StoreInfo?.Address1
                    + " " + this.order?.StoreInfo?.Address2
                  + ", " + this.order?.StoreInfo?.City;
            }
            template = this.replaceTemplateText(template, "{                          DAddress1}", address1, true);

            address2 = "";
            if (!string.IsNullOrEmpty(this.order?.StoreInfo?.State)
                || !string.IsNullOrEmpty(this.order?.StoreInfo?.PostalCode))
            {
                address2 = this.order?.StoreInfo?.State + ", " + this.order?.StoreInfo?.PostalCode;
            }
            template = this.replaceTemplateText(template, "{                          DAddress2}", address2, true);


            return template;
        }

        public string InvoiceItems()
        {
            string invoiceItems = "";
            var count = 0;

            if (this.order != null && this.order.OrderItems.Count<OrderItem>() > 0)
            {
                foreach (var item in this.order?.OrderItems)
                {
                    var template = InvoiceTextReportTemplates2.InvoiceItem();
                    template = this.replaceTemplateText(template, "{ProductName          }", item.ProductName);

                    var ordered = item.ItemsOrdered > 0 ? item.ItemsOrdered.ToString() : "";
                    template = this.replaceTemplateText(template, "{In }", ordered);

                    var returnValue = item.ItemsReturned > 0 ? item.ItemsReturned.ToString() : "";
                    template = this.replaceTemplateText(template, "{Out }", returnValue);

                    var sold = item.ItemsSold > 0 ? item.ItemsSold.ToString() : "";
                    template = this.replaceTemplateText(template, "{Sold  }", sold);

                    var cost = item.DiscountPrice > 0 ? string.Format("{0:0.00}", item.DiscountPrice) : "";
                    template = this.replaceTemplateText(template, "{COST  }", cost);

                    var amount = item.SubTotal > 0 ? string.Format("{0:0.00}", item.SubTotal) : "";
                    template = this.replaceTemplateText(template, "{AMOUNT}", amount, true);

                    var retail = item.ItemPrice > 0 ? string.Format("{0:0.00}", item.ItemPrice) : "";
                    template = this.replaceTemplateText(template, "{Retail }", retail);

                    invoiceItems += template;
                    count++;
                }
            }
            //add empy rows
            for (var i = count; i < 6; i++)
            {
                var template = InvoiceTextReportTemplates2.InvoiceItem();
                template = this.replaceTemplateText(template, "{ProductName          }", "");
                template = this.replaceTemplateText(template, "{In }", "");
                template = this.replaceTemplateText(template, "{Out }", "");
                template = this.replaceTemplateText(template, "{Sold  }", "");
                template = this.replaceTemplateText(template, "{COST  }", "");
                template = this.replaceTemplateText(template, "{AMOUNT}", "");
                template = this.replaceTemplateText(template, "{Retail }", "");

                invoiceItems += template;
            }

            return invoiceItems;
        }

        private string Footer()
        {
            string template = InvoiceTextReportTemplates2.InvoiceFooter();

            var value = this.order?.SubTotal > 0 ? string.Format("{0:0.00}", this.order?.SubTotal) : "";
            template = this.replaceTemplateText(template, "{Amount  }", value, true);

            value = this.order?.TaxAmount > 0 ? string.Format("{0:0.00}", this.order?.TaxAmount) : "";
            template = this.replaceTemplateText(template, "{Hst     }", value, true);

            value = this.order?.Total > 0 ? string.Format("{0:0.00}", this.order?.Total) : "";
            template = this.replaceTemplateText(template, "{Total   }", value, true);

            var routeNo = "";
            if (!string.IsNullOrEmpty(this.order?.StoreInfo.Route.RouteNumber)
               && !string.IsNullOrEmpty(this.order?.StoreInfo.RouteOrderNo))
            {
                routeNo = this.order?.StoreInfo.Route.RouteNumber + "." + this.order?.StoreInfo.RouteOrderNo;
            }
            template = this.replaceTemplateText(template, "{   RouteNo}", routeNo, true);

            return template;
        }
    }

}
