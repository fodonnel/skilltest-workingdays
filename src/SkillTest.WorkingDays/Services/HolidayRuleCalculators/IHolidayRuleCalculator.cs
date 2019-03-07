using System;
using System.Collections.Generic;

namespace SkillTest.WorkingDays.Services.PublicHolidayRules
{
    public interface IHolidayCalculator
    {
        IEnumerable<DateTime> GetHolidays(int year);
    }
}