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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceManagerWeb.Controllers
{
    [Route("[controller]")]
    public class StoreController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public StoreController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] SearchParam searchParam)
        {
            var values = this._unitOfWork.Stores.GetAll(searchParam);
            var count = this._unitOfWork.Stores.Count(searchParam);

            if (values != null)
            {
                IEnumerable<StoreViewModel> valuesVM = Mapper.Map<IEnumerable<StoreViewModel>>(values);
                return new OkObjectResult(new { Total = count, Records = valuesVM });
            }

            return NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var value = this._unitOfWork.Stores.Get(id);
            if (value != null)
                return new OkObjectResult(value);
            else
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Store value)
        {
            var validator = new StoreValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                var addedValue = this._unitOfWork.Stores.Add(value);
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
        public IActionResult Put(int id, [FromBody] Store value)
        {
            var validator = new StoreValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                value.Id = id;
                var updatedValue = this._unitOfWork.Stores.Update(value);
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
    }
}
