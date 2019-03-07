using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillTest.WorkingDays.Core;
using SkillTest.WorkingDays.Services;
using SkillTest.WorkingDays.Services.RuleCalculators;

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
                .AddSingleton<IRuleRepository, RuleRepository>()
                .AddSingleton<IPublicHolidayCalculator, PublicHolidayCalculator>();

            services
                .AddSingleton<FixedRuleCalculator>()
                .AddSingleton<FixedOrNextRuleCalculator>()
                .AddSingleton<OccurrenceRuleCalculator>();

            services
                .AddSingleton<IRuleCalculator>(sp => sp.GetRequiredService<FixedRuleCalculator>())
                .AddSingleton<IRuleCalculator>(sp => sp.GetRequiredService<FixedOrNextRuleCalculator>())
                .AddSingleton<IRuleCalculator>(sp => sp.GetRequiredService<OccurrenceRuleCalculator>());

            services
                .AddSingleton<IAsyncInitializer>(sp => sp.GetRequiredService<FixedRuleCalculator>())
                .AddSingleton<IAsyncInitializer>(sp => sp.GetRequiredService<FixedOrNextRuleCalculator>())
                .AddSingleton<IAsyncInitializer>(sp => sp.GetRequiredService<OccurrenceRuleCalculator>());

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
