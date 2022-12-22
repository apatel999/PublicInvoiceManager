using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using InvoiceManagerWeb.ViewModel;

namespace InvoiceManagerWeb.Validators
{
    public class DeliveryRouteValidator: AbstractValidator<DeliveryRouteViewModel>
    {
        public DeliveryRouteValidator()
        {
            RuleFor(x => x.RouteNumber).NotEmpty().WithMessage("Route number required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description required");
            RuleFor(x => x.DeliveryDay).NotEmpty().WithMessage("Delivery day required");
            RuleFor(x => x.ProductionDay).NotEmpty().WithMessage("Production Day required");
        }
    }
}
