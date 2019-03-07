using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillTest.WorkingDays.Models;

namespace SkillTest.WorkingDays.Services.PublicHolidayRules
{
    public class DayOfMonthHolidayCalculator : IHolidayCalculator
    {
        private readonly IHolidayRuleRepository _repo;
        private OccuranceRule[] _rules;

        public DayOfMonthHolidayCalculator(IHolidayRuleRepository repo)
        {
            _repo = repo;
        }

        public async Task InitializeAsync()
        {
            _rules = (await _repo.GetAll(HolidayRuleType.Occurance))
                .Select(t => Parse(t.Rule))
                .ToArray();
        }

        public IEnumerable<DateTime> GetHolidays(int year)
        {
            return _rules.Select(t => Execute(t, year))
                .Where(t => t != null)
                .Select(t => t.Value);
        }

        private DateTime? Execute(OccuranceRule rule, int year)
        {
            var date = new DateTime(year, rule.Month, 1);
            var days = 7 * rule.Occurance;

            var diff = ((int)rule.DayOfWeek - (int)date.DayOfWeek + days) % days;

            date = date.AddDays(diff);

            if (date.Month == rule.Month)
            {
                return date;
            }

            return null;
        }

        private OccuranceRule Parse(string rule)
        {
            var split = rule.Split('-');
            if (split.Length != 3)
            {
                throw new ArgumentException("Badly formed rule");
            }

            if (!int.TryParse(split[0], out var occurance))
            {
                throw new ArgumentException("Badly formed rule");
            }

            if (!Enum.TryParse<DayOfWeek>(split[1], true, out var dayOfWeek))
            {
                throw new ArgumentException("Badly formed rule");
            }

            if (!int.TryParse(split[2], out var month))
            {
                throw new ArgumentException("Badly formed rule");
            }

            if (month < 1 || month > 12)
            {
                throw new ArgumentException("Badly formed rule");
            }

            return new OccuranceRule
            {
                DayOfWeek = dayOfWeek,
                Month = month,
                Occurance = occurance
            };
        }

        private struct OccuranceRule
        {
            public int Occurance { get; set; }
            public DayOfWeek DayOfWeek { get; set; }
            public int Month { get; set; }
        }

    }
}
