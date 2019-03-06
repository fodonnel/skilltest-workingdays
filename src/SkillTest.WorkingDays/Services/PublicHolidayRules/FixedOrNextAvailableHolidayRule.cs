using System;

namespace SkillTest.WorkingDays.Services.PublicHolidayRules
{
    public class FixedOrNextAvailableHolidayRule : IHolidayRule
    {
        private readonly string _rule;

        public FixedOrNextAvailableHolidayRule(string rule)
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
            if (date == null)
            {
                return null;
            }

            while (date.Value.IsWeekend())
            {
                date = date.Value.AddDays(1);
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
