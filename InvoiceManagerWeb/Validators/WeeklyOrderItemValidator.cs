using DAL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerWeb.Validators
{
    public class WeeklyOrderItemValidator : AbstractValidator<WeeklyOrderItem>
    {
        public WeeklyOrderItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product required.");
            RuleFor(x => x.StoreId).NotEmpty().WithMessage("Store required.");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity required.");
        }
    }
}
