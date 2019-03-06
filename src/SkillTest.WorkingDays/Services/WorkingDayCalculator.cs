using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillTest.WorkingDays.Services
{
    public class WorkingDayCalculator
    {
        private readonly IWeekDaysCalculator _weekDaysCalculator;

        public WorkingDayCalculator(IWeekDaysCalculator weekDaysCalculator)
        {
            _weekDaysCalculator = weekDaysCalculator;
        }

        public int Calculate(DateTime fromDate, DateTime toDate)
        {
            var total = _weekDaysCalculator.Calculate(fromDate, toDate);

            return total;
        }
    }
}
