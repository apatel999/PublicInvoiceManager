using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerWeb.Print
{
    /**
     * Template has place holders in braces "{                               Address1}"
     * No of characters between braces is important. It defines space in which value will be replaced.
     * if values is smaller then place holder length it will padd with space.
     */

    public static class InvoiceTextReportTemplates
    {
        /**
         * Invoice is devided in 3 parts: This is Invoice Top Part
         */
        public static string InvoiceHeader()
        {
            string text =
@"
CAMLIN FLOWERS                                            INVOICE #:{InvoiceNo}
P.O. BOX 3,STATION MAIN,                                  DATE:{Date    }      
PETERBOROUGH, ON, K9J 6Y5                                 SOLD TO:             
PHONE: (800) 642 9913                 {                           CustomerName}
FAX:(705)-277-2912                    {                               Address1}
sales@camlinflowers.ca                {                               Address2}

------------------------------------------------------------------------------ 
                       |     |      |        |     WHOLESALE       | SUGGESTED  
 Item(s)               | IN  | OUT  | SALES  | COST/ea  | AMOUNT   | RETAIL/ea  
_______________________|_____|______|________|__________|__________|__________
";
            return text;
        }
        /**
         * This is part 2: Invoice items
         */
        public static string InvoiceItem()
        {
            string text =
@"
{ProductName          }|{In }|{Out }|{Sold  }| ${COST  }| ${AMOUNT}| ${Retail }     
_______________________|_____|______|________|__________|__________|__________
";
            text = text.TrimStart(Environment.NewLine.ToCharArray());
            return text;
        }

        /**
         * This is part 3: footer
         */
        public static string InvoiceFooter()
        {
            string text =
@"
                                                    NET:${Amount  }
                                                        ___________

                                         HST(134896638):${Hst     }
                                                        ___________

WATER CONDITION:                                  Total:${Total   }
                                                        ___________      
CLEAN ___ DIRTY ___
CLEAN VASES & WATER INCREASE SALES

__________________________________           __________________________________
      STORE REPRESENTATIVE                          SALES REPRESENTATIVE

                            *** THANK YOU ***                      {   RouteNo}
";
            return text;
        }
    }

}
