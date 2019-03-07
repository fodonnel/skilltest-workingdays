using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SkillTest.WorkingDays.Models;

namespace SkillTest.WorkingDays.Services
{
    public interface IRuleRepository
    {
        Task<IEnumerable<HolidayRule>> GetAll(HolidayRuleType ruleType);
    }

    public class RuleRepository : IRuleRepository
    {
        private const string RulesFile = "rules.csv";

        public async Task<IEnumerable<HolidayRule>> GetAll(HolidayRuleType ruleType)
        {
            var lines = await File.ReadAllLinesAsync(RulesFile);

            return lines
                .Select(line => line.Split(','))
                .Select(split => new HolidayRule
                {
                    RuleType = Enum.Parse<HolidayRuleType>(split[0], true),
                    Rule = split[1],
                    Description = split[2]
                })
                .Where(t => t.RuleType == ruleType)
                .ToList();
        }
    }
}
