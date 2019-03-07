using System;

namespace SkillTest.WorkingDays.Services
{
    public interface IWorkingDayCalculator
    {
        int Calculate(DateTime fromDate, DateTime toDate);
    }

    public class WorkingDayCalculator : IWorkingDayCalculator
    {
        private readonly IWeekDaysCalculator _weekDaysCalculator;
        private readonly IPublicHolidayCalculator _publicHolidayCalculator;

        public WorkingDayCalculator(IWeekDaysCalculator weekDaysCalculator, IPublicHolidayCalculator publicHolidayCalculator)
        {
            _weekDaysCalculator = weekDaysCalculator;
            _publicHolidayCalculator = publicHolidayCalculator;
        }

        public int Calculate(DateTime fromDate, DateTime toDate)
        {
            var workingDays = _weekDaysCalculator.Calculate(fromDate, toDate);
            var publicHolidays = _publicHolidayCalculator.Calculate(fromDate, toDate);

            return workingDays - publicHolidays;
        }
    }
}
