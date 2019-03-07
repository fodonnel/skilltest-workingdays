using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillTest.WorkingDays.Core;
using SkillTest.WorkingDays.Models;

namespace SkillTest.WorkingDays.Services.RuleCalculators
{
    public class FixedRuleCalculator : IAsyncInitializer, IRuleCalculator
    {
        private readonly IRuleRepository _repo;
        private string[] _rules;

        public FixedRuleCalculator(IRuleRepository repo)
        {
            _repo = repo;
        }

        public async Task InitializeAsync()
        {
            _rules = (await _repo.GetAll(HolidayRuleType.Fixed))
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
            if (!DateTime.TryParse(val, out var date) || date.IsWeekend())
            {
                return null;
            }

            return date;
        }
    }
}
