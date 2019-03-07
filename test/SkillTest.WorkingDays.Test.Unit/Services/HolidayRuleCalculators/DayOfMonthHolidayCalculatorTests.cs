using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using NSubstitute;
using SkillTest.WorkingDays.Models;
using SkillTest.WorkingDays.Services;
using SkillTest.WorkingDays.Services.HolidayRuleCalculators;
using Xunit;

namespace SkillTest.WorkingDays.Test.Unit.Services.HolidayRuleCalculators
{
    public class DayOfMonthHolidayCalculatorTests
    {
        [Fact]
        public async  Task Can_Calculate_Occurance()
        {
            var target = await CreateTarget("2-Monday-3");

            var result = target.GetHolidays(2019);

            result.Should().ContainSingle().Which.Should().Be(11.March(2019));
        }

        [Fact]
        public async Task Should_Rule_Nothing_If_No_Occurance()
        {
            var target = await CreateTarget("5-Monday-3");

            var result = target.GetHolidays(2019);

            result.Should().BeEmpty();
        }

        private static async Task<DayOfMonthHolidayCalculator> CreateTarget(params string[] rules)
        {
            var repo = Substitute.For<IHolidayRuleRepository>();
            repo.GetAll(HolidayRuleType.Occurance)
                .Returns(rules.Select(t => new HolidayRule { Rule = t }));

            var target = new DayOfMonthHolidayCalculator(repo);
            await target.InitializeAsync();
            return target;
        }
    }
}
