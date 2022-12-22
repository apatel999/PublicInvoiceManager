using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Models;
using InvoiceManagerWeb.Validators;
using FluentValidation.Results;
using RazorLight;
using InvoiceManagerWeb.Print;
using System.Net;
using Microsoft.Extensions.Logging;

namespace InvoiceManagerWeb.Controllers
{
    [Route("[controller]")]
    public class OrdersController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork, ILogger<OrdersController> log)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET api/values/5
        [HttpGet("store/{storeId}/empty-order")]
        public IActionResult GetEmptyOrder(int storeId)
        {
            var value = this._unitOfWork.Orders.GetEmptyOrder(storeId);
            if (value != null)
                return new OkObjectResult(value);
            else
                return NotFound();
        }


        // GET api/values/5
        [HttpGet]
        public IActionResult Get([FromQuery] SearchParam searchParam)
        {
            if(searchParam.YearId > 0)
            {
                Tuple<DateTime, DateTime> dates = this._unitOfWork.WeeklySchedule.GetStartEndDates(searchParam.YearId, searchParam.Week);
                searchParam.StartDate = dates.Item1;
                searchParam.EndDate = dates.Item2;
            }
            
            var value = this._unitOfWork.Orders.GetAll(searchParam);
            var count = this._unitOfWork.Orders.OrderCount(searchParam);
            var orderIds = this._unitOfWork.Orders.GetAllOrderIds(searchParam);
            if (value != null)
                return new OkObjectResult(new { Total = count, Records = value , searchParam = searchParam , OrderIds= orderIds });
            else
                return NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var value = this._unitOfWork.Orders.Get(id);
            if (value != null)
                return new OkObjectResult(value);
            else
                return NotFound();
        }

        
        [HttpGet("{id}.html")]
        public ContentResult GetDotmatrix(int id)
        {
            var value = this._unitOfWork.Orders.Get(id);
            var invoiceReport = new InvoiceTextReport2(value);
            string report = invoiceReport.GetText();
            string returnValue = "<html>" +
                "<head><style>@media print{ .no-print{display: none !important;}}</style></head>" +
                "<body>" +
                "<button class='no-print' onclick='window.close()'>Cancel</button> <button class='no-print' onclick='window.print()'>Print</button>" +
                "<pre>";
            returnValue += invoiceReport.GetText();
            returnValue += "</pre></body></html>";

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = returnValue
            };
           
        }
        [HttpGet("allSearchedDotmatrix.html")]
        public ContentResult GetAllDotMatrix([FromQuery] SearchParam searchParam)
        {
            if (searchParam.YearId > 0)
            {
                Tuple<DateTime, DateTime> dates = this._unitOfWork.WeeklySchedule.GetStartEndDates(searchParam.YearId, searchParam.Week);
                searchParam.StartDate = dates.Item1;
                searchParam.EndDate = dates.Item2;
            }

            var orders = this._unitOfWork.Orders.GetAll(searchParam);
            var reportText = "";
            foreach(var order in orders)
            {
                var orderDetail = this._unitOfWork.Orders.Get(order.Id);
                var invoiceReport = new InvoiceTextReport2(orderDetail);
                string text = invoiceReport.GetText();

                reportText += "<pre>" + text + "</pre>"
                             + "<P class='page-break'>";

            }
            
            string returnValue = "<html>" +
                "<head><style>@media print{ " +
                ".no-print{display: none !important;}" +
                ".page-break {page-break-after: always;}" +
                "}</style></head>" +
                "<body>" +
                "<button class='no-print' onclick='window.close()'>Cancel</button> <button class='no-print' onclick='window.print()'>Print</button>";
            returnValue += reportText;
            returnValue += "</body></html>";

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = returnValue
            };
        }

        // GET api/values/5
        //[HttpGet("{id}/print")]
        //public async Task<ActionResult> Print(int id)
        //{
        //    var value = this._unitOfWork.Orders.Get(id);
        //    if (value != null)
        //    {
        //        var engine = new RazorLightEngineBuilder()
        //      .UseFilesystemProject("C:/Users/Viraj/source/repos/InvoiceManager/ReportTemplate")
        //      .UseMemoryCachingProvider()
        //      .Build();

        //        string result = await engine.CompileRenderAsync("invoice.txt", value);

        //        return new OkObjectResult(result);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
                
        //}

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Order value)
        {
            var validator = new OrderValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                var addedValue = this._unitOfWork.Orders.AddOrder(value);
                if (addedValue != null)
                    return new OkObjectResult(addedValue);
                else
                    return NoContent();
            }
            else
            {
                return ValidationError(validation);
            }
        }

        // POST api/values
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Order value)
        {
            var validator = new OrderValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                var updatedValue = this._unitOfWork.Orders.UpdateOrder(value);
                if (updatedValue != null)
                    return new OkObjectResult(updatedValue);
                else
                    return NoContent();
            }
            else
            {
                return ValidationError(validation);
            }
        }


        [HttpGet("createweeklyorders/{yearId}/{week}")]
        public IActionResult CreatAllWeeklyOrders(int yearId, int week)
        {
            this._unitOfWork.Orders.AddAllWeeklyOrders(yearId,week);
                return new OkObjectResult(new {status= "SUCCESS" });
        }
    }
}