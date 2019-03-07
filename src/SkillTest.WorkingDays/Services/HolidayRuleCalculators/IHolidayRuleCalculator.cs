using System;
using System.Collections.Generic;

namespace SkillTest.WorkingDays.Services.HolidayRuleCalculators
{
    public interface IHolidayRuleCalculator
    {
        IEnumerable<DateTime> GetHolidays(int year);
    }
}