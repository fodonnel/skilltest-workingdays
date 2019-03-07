using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillTest.WorkingDays.Models;

namespace SkillTest.WorkingDays.Services.PublicHolidayRules
{
    public class FixedOrNextHolidayCalculator :  IHolidayCalculator
    {
        private readonly IHolidayRuleRepository _repo;
        private string[] _rules;

        public FixedOrNextHolidayCalculator(IHolidayRuleRepository repo)
        {
            _repo = repo;
        }

        public async Task InitializeAsync()
        {
            _rules = (await _repo.GetAll(HolidayRuleType.FixedOrNext))
                .Select(t => t.Rule)
                .ToArray();
        }

        public IEnumerable<DateTime> GetHolidays(int year)
        {
            return _rules.Select(t => Execute(t, year))
                .Where(t => t != null)
                .Select(t => t.Value);
        }

        private DateTime? Execute(string rule, int year)
        {
            var val = rule.Replace("<year>", year.ToString());
            if (!DateTime.TryParse(val, out var date))
            {
                return null;
            }

            while (date.IsWeekend())
            {
                date = date.AddDays(1);
            }

            return date;
        }
    }
}
