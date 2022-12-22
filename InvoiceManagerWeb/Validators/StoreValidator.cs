using DAL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerWeb.Validators
{
    public class StoreValidator : AbstractValidator<Store>
    {
        public StoreValidator()
        {
            RuleFor(x => x.RouteId).NotEmpty().WithMessage("Route number required.");
            RuleFor(x => x.BillingInformationId).NotEmpty().WithMessage("Billing information Id required.");
            RuleFor(x => x.Address1).NotEmpty().WithMessage("Address 1 required.");
        }
    }
}
