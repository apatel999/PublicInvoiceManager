using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceManagerWeb.Controllers
{
    [Route("production-schedule")]
    public class ProductionScheduleReportController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public ProductionScheduleReportController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        // GET api/values/5
        [HttpGet("{yearId}/{week}")]
        public IActionResult Get(int yearId, int week)
        {
            var report = this._unitOfWork.ProductionSchedule.GenerateReport(yearId, week);
            return new OkObjectResult(report);
        }
        
    }
}
