using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using SkillTest.WorkingDays.Services.PublicHolidayRules;
using Xunit;

namespace SkillTest.WorkingDays.Test.Unit.Services.PublicHolidayRules
{ 
    public class FixedHolidayRuleTests
    {
        [Fact]
        public void Badly_Formed_Rule_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentException>(() => new FixedHolidayRule("<year>-13-01"));
        }

        [Fact]
        public void Should_Evaluate_Rule_For_Year()
        {
            var target = new FixedHolidayRule("<year>-03-01");
            target.GetHoliday(2019).Should().Be(1.March(2019));
        }

        [Fact]
        public void Should_Return_Nothing_For_Weekends()
        {
            var target = new FixedHolidayRule("<year>-03-02");
            target.GetHoliday(2019).Should().BeNull();
        }

        [Fact]
        public void Can_Evaluate_Leap_Years()
        {
            var target = new FixedHolidayRule("<year>-02-29");
            target.GetHoliday(2000).Should().Be(29.February(2000));
        }

        [Fact]
        public void Should_Return_Nothing_For_Invalid_Dates()
        {
            var target = new FixedHolidayRule("<year>-02-29");
            target.GetHoliday(2019).Should().BeNull();
        }
    }
}
