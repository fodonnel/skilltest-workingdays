using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillTest.WorkingDays.Services.PublicHolidayRules
{
    public class FixedHolidayRule : IHolidayRule
    {
        private readonly string _rule;

        public FixedHolidayRule(string rule)
        {
            if (GetDateForYear(rule, 2000) == null)
            {
                throw new ArgumentException("Badly formed rule");
            }

            _rule = rule;
        }

        public DateTime? GetHoliday(int year)
        {
            var date = GetDateForYear(_rule, year);
            if (date == null || date.Value.IsWeekend())
            {
                return null;
            }

            return date;
        }

        private DateTime? GetDateForYear(string rule, int year)
        {
            var val = rule.Replace("<year>", year.ToString());
            return DateTime.TryParse(val, out var date) ? date : (DateTime?)null;
        }
    }
}
