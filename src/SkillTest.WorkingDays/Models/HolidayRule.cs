using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillTest.WorkingDays.Models
{
    public class HolidayRule
    {
        public HolidayRuleType RuleType { get; set; }

        public string Rule { get; set; }

        public string Description { get; set; }
    }
}
