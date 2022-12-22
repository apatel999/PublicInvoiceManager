using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Models;
using DAL.Services;

namespace InvoiceManagerWeb.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class SalesReportController :   BaseController
    {
        private ISalesReportService _salesService;

        public SalesReportController(ISalesReportService salesService)
        {
            this._salesService = salesService;
        }

        
       
        [HttpPost("insouts/{yearId}/{week}")]
        public IActionResult InsOutsSummary(int yearId, int week, [FromBody] SearchParam searchParam)
        {
            var report = this._salesService.GetDeliverySheetInsOut(searchParam);
            return new OkObjectResult(report);
        }

        [HttpPost("sales-summary-by-route/{yearId}/{week}")]
        public IActionResult GetSalesSummaryByRoute(int yearId, int week)
        {
            var report = this._salesService.GetSalseSummaryByRoute(new SearchParam { YearId = yearId, Week = week });
            return new OkObjectResult(report);
        }

        [HttpPost("sales-audit-by-route/{yearId}/{week}")]
        public IActionResult SalesAuditByRoute(int yearId, int week, [FromBody] SearchParam searchParam)
        {
            var report = this._salesService.GetSalesAuditByRoute(searchParam);
            return new OkObjectResult(report);
        }

        [HttpPost("customer-units/{yearId}/{week}")]
        public IActionResult YearlyCustomerUnits(int yearId, int week, [FromBody] SearchParam searchParam)
        {
            var report = this._salesService.CustomerUnitsReport(searchParam);
            return new OkObjectResult(report);
        }
    }
}