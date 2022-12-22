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


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceManagerWeb.Controllers
{
    [Route("[controller]")]
    public class DeliveryRoutesController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public DeliveryRoutesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var values = this._unitOfWork.DeliveryRoutes.GetAll();
            IEnumerable<DeliveryRouteViewModel> valuesVM = Mapper.Map<IEnumerable<DeliveryRouteViewModel>>(values);
            return new OkObjectResult(valuesVM);
        }

        
        

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var value = this._unitOfWork.DeliveryRoutes.Get(id);
            if (value != null)
                return new OkObjectResult(value);
            else
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] DeliveryRouteViewModel value)
        {
            var validator = new DeliveryRouteValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
               var addedValue = this._unitOfWork.DeliveryRoutes.Add(Mapper.Map<DeliveryRoute>(value));
                if(addedValue != null)
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
        public IActionResult Put(int id, [FromBody] DeliveryRouteViewModel value)
        {
            var validator = new DeliveryRouteValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                value.Id = id;
                var updatedValue = this._unitOfWork.DeliveryRoutes.Update(Mapper.Map<DeliveryRoute>(value));
                return new OkObjectResult(updatedValue);
            }
            else
            {
                return ValidationError(validation);
            }
        }

        // PUT api/values/5
        [Route("updateall")]
        [HttpPut]
        public IActionResult Put([FromBody] IEnumerable<DeliveryRouteViewModel> values)
        {
            var validator = new DeliveryRouteValidator();
            var validationResults = new Dictionary<string,ValidationResult>();
            var validValues = new List<DeliveryRoute>();
            foreach(var value in values)
            {
                ValidationResult validation = validator.Validate(value);
                if (!validation.IsValid)
                {
                    validationResults.Add(value.Id.ToString(),validation);
                }
                else
                {
                    validValues.Add(Mapper.Map<DeliveryRoute>(value));
                }
            }
      
            if (validationResults.Count <=0)
            {
                var updatedValue = this._unitOfWork.DeliveryRoutes.UpdateRange(validValues);
                return new OkObjectResult(updatedValue);
            }
            else
            {
                return ValidationError(validationResults);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
