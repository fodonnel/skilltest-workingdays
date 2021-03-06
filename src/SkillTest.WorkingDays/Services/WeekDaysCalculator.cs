﻿using System;

namespace SkillTest.WorkingDays.Services
{
    public interface IWeekDaysCalculator
    {
        int Calculate(DateTime fromDate, DateTime toDate);
    }

    public class WeekDaysCalculator : IWeekDaysCalculator
    {
        public int Calculate(DateTime fromDate, DateTime toDate)
        {
            if (!CrossesWeekend(fromDate, toDate))
            {
                var result = (toDate - fromDate).Days - 1;
                return result > 0 ? result : 0;
            }

            var fromEndOfWeek = SnapToEndOfWeek(fromDate);
            var toStartOfWeek = SnapToStartOfWeek(toDate);

            var total = (toStartOfWeek - fromEndOfWeek).Days;
            var weekendDays = ((total / 7)) * 2;
            var daysUntilNextWeekend = fromDate.IsWeekend() ? 0 : (fromEndOfWeek - fromDate).Days - 2;
            var daysToPrevWeekend = toDate.IsWeekend() ? 0 : (toDate - toStartOfWeek).Days - 1;

            return total - weekendDays + daysUntilNextWeekend + daysToPrevWeekend;
        }


        private bool CrossesWeekend(DateTime fromDate, DateTime toDate) =>
            (toDate - fromDate).Days > 6 || fromDate.DayOfWeek > toDate.DayOfWeek;

        private DateTime SnapToStartOfWeek(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                return  date.AddDays(1);
            }

            var diff = Math.Abs((int) DayOfWeek.Sunday - (int) date.DayOfWeek);
            return date.AddDays(diff * -1);
        }

        private DateTime SnapToEndOfWeek(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                return date.AddDays(1);
            }

            var diff = ((int)DayOfWeek.Sunday - (int)date.DayOfWeek + 7) % 7;
            return date.AddDays(diff);
        }
    }
}
