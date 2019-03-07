using System;
using System.Collections.Generic;

namespace SkillTest.WorkingDays.Services.RuleCalculators
{
    public interface IRuleCalculator
    {
        IEnumerable<DateTime> GetHolidays(int year);
    }
}