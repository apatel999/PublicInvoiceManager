using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using InvoiceManagerWeb.ViewModel;

namespace InvoiceManagerWeb.Validators
{
    public class ProductValidator: AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Productcode).NotEmpty().WithMessage("Product Code required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price Code required");
        }
        
    }
}
