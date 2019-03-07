using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using NSubstitute;
using SkillTest.WorkingDays.Models;
using SkillTest.WorkingDays.Services;
using SkillTest.WorkingDays.Services.RuleCalculators;
using Xunit;

namespace SkillTest.WorkingDays.Test.Unit.Services.RuleCalculators
{
    public class OccurenceRuleCalculatorTests
    {
        [Fact]
        public async  Task Can_Calculate_Occurence()
        {
            var target = await CreateTarget("2-Monday-3");

            var result = target.GetHolidays(2019);

            result.Should().ContainSingle().Which.Should().Be(11.March(2019));
        }

        [Fact]
        public async Task Should_Rule_Nothing_If_No_Occurence()
        {
            var target = await CreateTarget("5-Monday-3");

            var result = target.GetHolidays(2019);

            result.Should().BeEmpty();
        }

        private static async Task<OccurrenceRuleCalculator> CreateTarget(params string[] rules)
        {
            var repo = Substitute.For<IRuleRepository>();
            repo.GetAll(HolidayRuleType.Occurance)
                .Returns(rules.Select(t => new HolidayRule { Rule = t }));

            var target = new OccurrenceRuleCalculator(repo);
            await target.InitializeAsync();
            return target;
        }
    }
}
