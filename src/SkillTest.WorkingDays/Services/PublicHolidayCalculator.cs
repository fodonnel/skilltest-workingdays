using System;
using System.Collections.Generic;
using System.Linq;
using SkillTest.WorkingDays.Services.HolidayRuleCalculators;

namespace SkillTest.WorkingDays.Services
{
    public interface IPublicHolidayCalculator
    {
        int Calculate(DateTime fromDate, DateTime toDate);
    }

    public class PublicHolidayCalculator : IPublicHolidayCalculator
    {
        private readonly List<IHolidayRuleCalculator> _calculators;

        public PublicHolidayCalculator(IEnumerable<IHolidayRuleCalculator> calculators)
        {
            _calculators = calculators.ToList();
        }

        public int Calculate(DateTime fromDate, DateTime toDate)
        {
            var dates = new List<DateTime>();
            for (var year = fromDate.Year; year <= toDate.Year; year++)
            {
                foreach (var calculator in _calculators)
                {
                    dates.AddRange(calculator.GetHolidays(year));
                }
            }

            return dates.Distinct()
                .OrderBy(t => t)
                .Count(t => t > fromDate && t < toDate);
        }
    }
}
