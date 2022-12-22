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
    public class ProductsController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var values = this._unitOfWork.Products.GetAll();
            IEnumerable<ProductViewModel> valuesVM = Mapper.Map<IEnumerable<ProductViewModel>>(values);
            return new OkObjectResult(valuesVM);
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var value = this._unitOfWork.Products.Get(id);
            if (value != null)
                return new OkObjectResult(value);
            else
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] ProductViewModel value)
        {
            var validator = new ProductValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                var addedValue = this._unitOfWork.Products.Add(Mapper.Map<Product>(value));
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
        public IActionResult Put(int id, [FromBody] ProductViewModel value)
        {
            var validator = new ProductValidator();
            ValidationResult validation = validator.Validate(value);

            if (validation.IsValid)
            {
                value.Id = id;
                var updatedValue = this._unitOfWork.Products.Update(Mapper.Map<Product>(value));
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
