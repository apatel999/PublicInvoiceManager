using DAL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerWeb.Validators
{
    public class WeeklyScheduleValidator : AbstractValidator<WeeklySchedule>
    {
        public WeeklyScheduleValidator()
        {
            RuleFor(x => x.SundayOfTheYearId).NotEmpty().WithMessage("Weekly Schedule year required.");
            RuleFor(x => x.Week).NotEmpty().WithMessage("Weekly schedule week required.");
            RuleFor(x => x.DeliveryDate).NotEmpty().WithMessage("Weekly schedule delivery date required.");
            RuleFor(x => x.RouteId).NotEmpty().WithMessage("Weekly schedule route required.");
        }
    }
}
