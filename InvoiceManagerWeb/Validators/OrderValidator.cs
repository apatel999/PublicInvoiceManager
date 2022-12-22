using DAL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerWeb.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.StoreId).NotEmpty().WithMessage("Store information required. Please select Store.");
            RuleFor(x => x.OrderItems).NotEmpty().WithMessage("Empty Order. Please select at lease one Product.");
        }
    }
}
