using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using InvoiceManagerWeb.ViewModel;
using InvoiceManagerWeb.Validators;
using FluentValidation.Results;
using AutoMapper;
using DAL.Models;

namespace InvoiceManagerWeb.Controllers
{
    //Presently only one preorder supported, which is weekly
    [Route("stores/{storeId}/weeklyorder")]
    public class StoreWeeklyOrdersController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public StoreWeeklyOrdersController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET: api/values
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    //return order with all items

        //    var values = this._unitOfWork.StoreWeeklyOrders.GetAll();
        //    return new OkObjectResult(values);
        //}

        // GET: api/values
        [HttpGet]
        public IActionResult Get(int storeId)
        {
            var items = this._unitOfWork.StoreWeeklyOrders.GetOrderItems(storeId);
            var weeklyOrder = new { StoreId=storeId, Items=items };
            if (items != null)
                return new OkObjectResult(weeklyOrder);
            else
                return NotFound();
        }

        // POST api/values
        [Route("items")]
        [HttpPost]
        public IActionResult Post(int storeId, [FromBody] WeeklyOrderItem value)
        {
            value.StoreId = storeId;
            var validator = new WeeklyOrderItemValidator();

            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                var addedValue = this._unitOfWork.StoreWeeklyOrders.Add(value);
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

        // PUT api/values
        [Route("items/{itemId}")]
        [HttpPut]
        public IActionResult Put(int storeId, int itemId,  [FromBody] WeeklyOrderItem value)
        {
            value.StoreId = storeId;
            var validator = new WeeklyOrderItemValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                var updatedValue = this._unitOfWork.StoreWeeklyOrders.Update(value);
                return new OkObjectResult(updatedValue);
            }
            else
            {
                return ValidationError(validation);
            }
        }

        [Route("items/{itemId}")]
        [HttpDelete]
        public IActionResult Delete(int storeId, int itemId)
        {
            if (storeId > 0 && itemId > 0)
            {
                this._unitOfWork.StoreWeeklyOrders.Remove(new WeeklyOrderItem { StoreId = storeId, Id = itemId });
                return new OkResult();
            }
            else
            {
                return NotFound();
            }
        }

    }
}