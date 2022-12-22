using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceManagerWeb.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult ValidationError(ValidationResult validation)
        {
            //var errors = validation.Errors.Select(e => e.ErrorMessage);
            return new BadRequestObjectResult(new { errors = validation });
        }

        public IActionResult ValidationError(Dictionary<string,ValidationResult> validations)
        {
            return new BadRequestObjectResult(new { errors = validations});
        }
    }
}
