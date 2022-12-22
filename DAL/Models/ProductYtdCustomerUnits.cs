using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class StoreProductYtdUnits
    {
        public Store Store { get; set; }
        public int ProductId { get; set; }
        public ProductUnit YtdUnits { get; set; }
    }

    public class YtdProductUnit
    {
        public Product Product { get; set; }
        public int YtdIns { get; set; }
        public int YtdSold { get; set; }
        public decimal YtdPercent { get; set; }

        public int YtdMinusHolidayIns { get; set; }
        public int YtdMinusHolidaySold { get; set; }
        public decimal YtdMinusHolidayPercent { get; set; }

        public int Check { get; set; }
        public int Recommended { get; set; }
    }

    public class ProductYtdCustomerUnitsReport
    {
        public Store Store { get; set; }
        public IEnumerable<YtdProductUnit> YtdUnits { get; set; }
    }

}
