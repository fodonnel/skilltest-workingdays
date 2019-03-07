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
    public class FixedOrNextHolidayCalculatorTests
    {
        [Fact]
        public async Task Should_Evaluate_Rule_For_Year()
        {
            var target = await CreateTarget("<year>-03-01");

            var result = target.GetHolidays(2019);

            result.Should().ContainSingle().Which.Should().Be(1.March(2019));
        }

        [Fact]
        public async Task Should_Return_NextMonday_For_Weekends()
        {
            var target = await CreateTarget("<year>-03-02");

            var result = target.GetHolidays(2019);

            result.Should().ContainSingle().Which.Should().Be(4.March(2019));
        }

        [Fact]
        public async Task Can_Evaluate_Leap_Years()
        {
            var target = await CreateTarget("<year>-02-29");

            var result = target.GetHolidays(2000);

            result.Should().ContainSingle().Which.Should().Be(29.February(2000));
        }

        [Fact]
        public async Task Should_Return_Nothing_For_Invalid_Dates()
        {
            var target = await CreateTarget("<year>-02-29");

            var result = target.GetHolidays(2019);

            result.Should().BeEmpty();
        }


        private static async Task<FixedOrNextHolidayCalculator> CreateTarget(params string[] rules)
        {
            var repo = Substitute.For<IHolidayRuleRepository>();
            repo.GetAll(HolidayRuleType.FixedOrNext)
                .Returns(rules.Select(t => new HolidayRule { Rule = t }));

            var target = new FixedOrNextHolidayCalculator(repo);
            await target.InitializeAsync();
            return target;
        }
    }
}
