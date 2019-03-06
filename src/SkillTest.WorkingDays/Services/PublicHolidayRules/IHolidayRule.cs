using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillTest.WorkingDays.Services.PublicHolidayRules
{
    public interface IHolidayRule
    {
        DateTime? GetHoliday(int year);
    }
}
