using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Models;
using InvoiceManagerWeb.ViewModel;
using AutoMapper;
using InvoiceManagerWeb.Validators;
using FluentValidation.Results;

namespace InvoiceManagerWeb.Controllers
{
    [Route("[controller]")]
    public class BillingInformationController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public BillingInformationController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] SearchParam searchParam)
        {
            var value = this._unitOfWork.BillingInformation.GetAll(searchParam);
            var count = this._unitOfWork.BillingInformation.Count(searchParam);
            if (value != null)
                return new OkObjectResult(new {Total = count, Records = value });
            else
                return NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var value = this._unitOfWork.BillingInformation.Get(id);
            if (value != null)
                return new OkObjectResult(value);
            else
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] BillingInformation value)
        {
            var validator = new BillingInformationValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                var addedValue = this._unitOfWork.BillingInformation.Add(value);
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BillingInformation value)
        {
            var validator = new BillingInformationValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                value.Id = id;
                var updatedValue = this._unitOfWork.BillingInformation.Update(value);
                return new OkObjectResult(updatedValue);
            }
            else
            {
                return ValidationError(validation);
            }
        }



        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("{id}/weekly-billing-statement/{yearId}/{week}")]
        public IActionResult WeeklyBillingStatement(int id, int yearId, int week)
       {
            var result = this._unitOfWork.BillingInformation.GetWeeklyBillingStatement(id, yearId, week);
            return new OkObjectResult(result);
       }
    }
}