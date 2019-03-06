using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FluentAssertions.Extensions;
using SkillTest.WorkingDays.Services.PublicHolidayRules;
using Xunit;

namespace SkillTest.WorkingDays.Test.Unit.Services.PublicHolidayRules
{
    public class DayOfMonthHolidayRuleTests
    {
        [Fact]
        public void Can_Calculate_Occurance()
        {
            var target = new DayOfMonthHolidayRule("2-Monday-3");
            target.GetHoliday(2019).Should().Be(11.March(2019));
        }

        [Fact]
        public void Should_Rule_Nothing_If_No_Occurance()
        {
            var target = new DayOfMonthHolidayRule("5-Monday-3");
            target.GetHoliday(2019).Should().BeNull();
        }
    }
}
