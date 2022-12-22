//using DAL.Models;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;

//namespace InvoiceManagerConsoleApp
//{

//    /**
//     * Template has place holders in braces "{                               Address1}"
//     * No of characters between braces is important. It defines space in which value will be replaced.
//     * if values is smaller then place holder length it will padd with space.
//     */

//    public static class InvoiceTextReportTemplates
//    {
//        /**
//         * Invoice is devided in 3 parts: This is Invoice Top Part
//         */
//        public static string InvoiceHeader()
//        {
//            string text =
//@"
//CAMLIN FLOWERS                                            INVOICE #:{InvoiceNo}
//P.O. BOX 3,STATION MAIN,                                  DATE:{Date    }
//PETERBOROUGH, ON, K9J 6Y5                                 SOLD TO:
//PHONE: (800) 642 9913                 {                               Address1}
//<sales@camlinflowers.ca>              {                               Address2}
//FAX:(705)-277-2912                    {                               Address3}

//------------------------------------------------------------------------------ 
//                       |     |      |        |     WHOLESALE       | SUGGESTED  
// Item(s)               | IN  | OUT  | SALES  | COST/ea  | AMOUNT   | RETAIL/ea  
//_______________________|_____|______|________|__________|__________|__________
//";
//            return text;
//        }
//        /**
//         * This is part 2: Invoice items
//         */
//        public static string InvoiceItem()
//        {
//            string text =
//@"
//{ProductName          }|{In }|{Out }|{Sold  }|${COST   }|${AMOUNT }|${Retail  }     
//_______________________|_____|______|________|__________|__________|__________
//";
//            text = text.TrimStart(Environment.NewLine.ToCharArray());
//            return text;
//        }

//        /**
//         * This is part 3: footer
//         */
//        public static string InvoiceFooter()
//        {
//            string text =
//@"
//                                                    NET:${Amount  }
//                                                        ___________

//                                         HST(134896638):${Hst     }
//                                                        ___________

//Water Condition                                   Total:${Total   }
//                                                        ___________      
//CLEAN ___ DIRTY ___
//CLEAN VASES & WATER INCREASE SALES

//__________________________________           __________________________________
//      STORE REPRESENTATIVE                          SALES REPRESENTATIVE

//                            *** THANK YOU ***                      {   RouteNo}
//";
//            return text;
//        }
//    }


//public class InvoiceTextReport
//    {
//        private Order order;
//        public InvoiceTextReport(Order order)
//        {
//            this.order = order;
//        }

//        public string GetText()
//        {
//            var text = this.Header();
//            text += this.InvoiceItems();
//            text += this.Footer();
//            return text;
//        }

//        private string replaceTemplateText(string template, string searchText, string replacement, bool alignRight = false)
//        {
//            if (alignRight)
//            {
//                return template.Replace(searchText, replacement.PadLeft(searchText.Length, ' '));
//            }
//            return template.Replace(searchText, replacement.PadRight(searchText.Length, ' '));
//        }

//        private string Header()
//        {
//            string template = InvoiceTextReportTemplates.InvoiceHeader();
//            template = this.replaceTemplateText(template, "{InvoiceNo}", this.order.InvoiceNumber.ToString(), true);
//            template = this.replaceTemplateText(template, "{Date    }", this.order.InvoiceDate.ToString("dd/MM/yyyy"), true);
//            var address1 = this.order.StoreInfo.Address1;
//            template = this.replaceTemplateText(template, "{                               Address1}", address1, true);
//            var address2 = this.order.StoreInfo.City + ", "+ this.order.StoreInfo.State;
//            template = this.replaceTemplateText(template, "{                               Address2}", address2, true);
//            var address3 = this.order.StoreInfo.City;
//            template = this.replaceTemplateText(template, "{                               Address3}", address3,  true);

//            return template;
//        }

//        public string InvoiceItems()
//        {
//            string invoiceItems = "";
//            var count = 0;
//            foreach(var item in this.order.OrderItems)
//            {
//                var template = InvoiceTextReportTemplates.InvoiceItem();
//                template = this.replaceTemplateText(template, "{ProductName          }", item.ProductName);
//                template = this.replaceTemplateText(template, "{In }", item.ItemsOrdered.ToString());
//                template = this.replaceTemplateText(template, "{Out }", item.ItemsReturned.ToString());
//                template = this.replaceTemplateText(template, "{Sold  }", item.ItemsSold.ToString());
//                template = this.replaceTemplateText(template, "{COST   }", item.DiscountPrice.ToString());
//                template = this.replaceTemplateText(template, "{AMOUNT }", item.SubTotal.ToString(),true);
//                template = this.replaceTemplateText(template, "{Retail  }", item.RegularPrice.ToString());

//                invoiceItems += template;
//                count++;
//            }

//            //add empy rows
//            for(var i = count; i< 10; i++)
//            {
//                var template = InvoiceTextReportTemplates.InvoiceItem();
//                template = this.replaceTemplateText(template, "{ProductName          }", "");
//                template = this.replaceTemplateText(template, "{In }", "");
//                template = this.replaceTemplateText(template, "{Out }", "");
//                template = this.replaceTemplateText(template, "{Sold  }", "");
//                template = this.replaceTemplateText(template, "{COST   }", "");
//                template = this.replaceTemplateText(template, "{AMOUNT }", "");
//                template = this.replaceTemplateText(template, "{Retail  }", "");

//                invoiceItems += template;
//            }

//            return invoiceItems;
//        }
        
//        private string Footer()
//        {
//            string template = InvoiceTextReportTemplates.InvoiceFooter();
//            template = this.replaceTemplateText(template, "{Amount  }", string.Format("{0:0.00}", this.order.SubTotal), true);
//            template = this.replaceTemplateText(template, "{Hst     }", string.Format("{0:0.00}",this.order.TaxAmount), true);
//            template = this.replaceTemplateText(template, "{Total   }", string.Format("{0:0.00}",this.order.Total), true);
//            var routeNo = this.order.StoreInfo.Route.RouteNumber + "." + this.order.StoreInfo.RouteOrderNo;
//            template = this.replaceTemplateText(template, "{   RouteNo}", routeNo, true);

//            return template;
//        }
//    }
//}
