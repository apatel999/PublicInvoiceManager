using DAL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerWeb.Validators
{
    public class BillingInformationValidator:  AbstractValidator<BillingInformation>
    {
        public BillingInformationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name required.");
            RuleFor(x => x.PaymentType).NotEmpty().WithMessage("Payment Type required.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status required.");
            //RuleFor(x => x.Address1).NotEmpty().WithMessage("Address 1  required.");
        }
        
    }
}
