using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SkillTest.WorkingDays.Core;
using SkillTest.WorkingDays.Services;
using SkillTest.WorkingDays.Services.HolidayRuleCalculators;
using SkillTest.WorkingDays.Services.PublicHolidayRules;

namespace SkillTest.WorkingDays
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<IWeekDaysCalculator, WeekDaysCalculator>()
                .AddSingleton<IWorkingDayCalculator, WorkingDayCalculator>()
                .AddSingleton<IHolidayRuleRepository, HolidayRuleRepository>()
                .AddSingleton<IPublicHolidayCalculator, PublicHolidayCalculator>();

            services
                .AddSingleton<FixedHolidayCalculator>()
                .AddSingleton<FixedOrNextHolidayCalculator>()
                .AddSingleton<DayOfMonthHolidayCalculator>();

            services
                .AddSingleton<IHolidayRuleCalculator>(sp => sp.GetRequiredService<FixedHolidayCalculator>())
                .AddSingleton<IHolidayRuleCalculator>(sp => sp.GetRequiredService<FixedOrNextHolidayCalculator>())
                .AddSingleton<IHolidayRuleCalculator>(sp => sp.GetRequiredService<DayOfMonthHolidayCalculator>());

            services
                .AddSingleton<IAsyncInitializer>(sp => sp.GetRequiredService<FixedHolidayCalculator>())
                .AddSingleton<IAsyncInitializer>(sp => sp.GetRequiredService<FixedOrNextHolidayCalculator>())
                .AddSingleton<IAsyncInitializer>(sp => sp.GetRequiredService<DayOfMonthHolidayCalculator>());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
