using System;

namespace SkillTest.WorkingDays
{
    public static class Extensions
    {
        public static bool IsWeekend(this DateTime date) =>
            date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }
}
