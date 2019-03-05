using System;
using FluentAssertions;
using SkillTest.WorkingDays.Services;
using Xunit;

namespace SkillTest.WorkingDays.Test.Unit.Services
{
    public class WeekDaysCalculatorTests
    {
        private readonly WeekDaysCalculator _target;

        public WeekDaysCalculatorTests()
        {
            _target = new WeekDaysCalculator();
        }

        [Theory]
        [InlineData("2019-03-03", "2019-03-09", 5)]
        [InlineData("2019-03-02", "2019-03-09", 5)]
        [InlineData("2019-03-02", "2019-03-10", 5)]
        [InlineData("2019-03-03", "2019-03-23", 15)]
        [InlineData("2019-03-03", "2019-03-30", 20)]
        [InlineData("2019-03-03", "2019-03-31", 20)]
        public void Can_Calculate_Work_Days_Between_Weekends(DateTime fromDate, DateTime toDate, int expected)
        {
            _target.Calculate(fromDate, toDate).Should().Be(expected);
        }

        [Fact]
        public void Can_Calculate_Work_Days_Until_Weekend()
        {
            var midweek = new DateTime(2019, 03, 05);
            var weekend = new DateTime(2019, 03, 10);

            _target.Calculate(midweek, weekend).Should().Be(3);
        }

        [Fact]
        public void Can_Calculate_Work_Days_After_Weekend()
        {
            var weekend = new DateTime(2019, 03, 10);
            var midweek = new DateTime(2019, 03, 14);

            _target.Calculate(weekend, midweek).Should().Be(3);
        }

        [Theory]
        [InlineData("2019-03-04", "2019-03-08", 3)]
        [InlineData("2019-03-04", "2019-03-05", 0)]
        [InlineData("2019-03-04", "2019-03-04", 0)]
        public void Can_Calculate_Within_Same_Week(DateTime fromDate, DateTime toDate, int expected)
        {
            _target.Calculate(fromDate, toDate).Should().Be(expected);
        }

        [Theory]
        [InlineData("2014-08-7", "2014-08-11", 1)]
        [InlineData("2014-08-13", "2014-08-21", 5)]
        [InlineData("2019-03-01", "2019-03-31", 20)]
        public void Can_Calculate_Work_Days_Between_Range(DateTime fromDate, DateTime toDate, int total)
        {
            _target.Calculate(fromDate, toDate).Should().Be(total);
        }
    }
}
