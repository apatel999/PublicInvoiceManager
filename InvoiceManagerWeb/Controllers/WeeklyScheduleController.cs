using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Models;
using InvoiceManagerWeb.Validators;
using FluentValidation.Results;

namespace InvoiceManagerWeb.Controllers
{
    [Route("weekly-schedule")]
    public class WeeklyScheduleController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public WeeklyScheduleController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("{sundayOfTheYearId}/{week}")]
        public IActionResult Get(int sundayOfTheYearId = 0, int week = 0)
        {
            var values = this._unitOfWork.WeeklySchedule.GetDefaultSchedules(sundayOfTheYearId, week); // week start with 0
            return new OkObjectResult(values);
        }

        [HttpGet("year-list")]
        public IActionResult GetYearList()
        {
            var years = this._unitOfWork.WeeklySchedule.GetYearList();
            return new OkObjectResult(years);
        }

        [HttpGet("{sundayOfTheYearId}/last-saved-week")]
        public IActionResult GetLastSavedWeek(int sundayOfTheYearId)
        {
            int latestWeek = this._unitOfWork.WeeklySchedule.GetLatestWeek(sundayOfTheYearId);
            return new OkObjectResult(new { week = latestWeek});
        }

        [HttpPut]
        public IActionResult Put([FromBody] IEnumerable<WeeklySchedule> values)
        {

            var validator = new WeeklyScheduleValidator();
            var validationResults = new Dictionary<string, ValidationResult>();
            var validValues = new List<WeeklySchedule>();
            foreach (var value in values)
            {
                ValidationResult validation = validator.Validate(value);
                if (!validation.IsValid)
                {
                    validationResults.Add(value.Id.ToString(), validation);
                }
                else
                {
                    validValues.Add(value);
                }
            }

            if (validationResults.Count <= 0)
            {
                var updatedValue = this._unitOfWork.WeeklySchedule.AddUpdateRange(validValues);
                return new OkObjectResult(updatedValue);
            }
            else
            {
                return ValidationError(validationResults);
            }
            
        }
    }
}