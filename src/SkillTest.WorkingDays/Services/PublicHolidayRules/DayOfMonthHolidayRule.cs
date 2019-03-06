using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillTest.WorkingDays.Services.PublicHolidayRules
{
    public class DayOfMonthHolidayRule : IHolidayRule
    {
        private readonly int _occurance;
        private readonly DayOfWeek _dayOfWeek;
        private readonly int _month;

        public DayOfMonthHolidayRule(string rule)
        {
            (_occurance, _dayOfWeek, _month) = Parse(rule);
        }


        public DateTime? GetHoliday(int year)
        {
            var date = new DateTime(year, _month, 1);
            var days = 7 * _occurance;

            var diff = ((int)_dayOfWeek - (int)date.DayOfWeek + days) % days;

            date = date.AddDays(diff);

            if (date.Month == _month)
            {
                return date;
            }

            return null;
        }

        private (int, DayOfWeek, int) Parse(string rule)
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

            return (occurance, dayOfWeek, month);
        }
           
    }
}
