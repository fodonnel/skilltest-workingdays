﻿using System;
using Microsoft.AspNetCore.Mvc;
using SkillTest.WorkingDays.Services;

namespace SkillTest.WorkingDays.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingDaysController : ControllerBase
    {
        private readonly IWorkingDayCalculator _calculator;

        public WorkingDaysController(IWorkingDayCalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpGet]
        public ActionResult<int> Get([FromQuery]string fromDate, [FromQuery]string toDate)
        {
            if (string.IsNullOrEmpty(fromDate) || !DateTime.TryParse(fromDate, out var parsedFromDate))
            {
                return BadRequest("Invalid From Date");
            }

            if (string.IsNullOrEmpty(toDate) || !DateTime.TryParse(toDate, out var parsedToDate))
            {
                return BadRequest("Invalid To Date");
            }

            if (parsedToDate <= parsedFromDate)
            {
                return BadRequest("Invalid Date Range");
            }

            return _calculator.Calculate(parsedFromDate, parsedToDate);
        }
    }
}
