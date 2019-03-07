using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SkillTest.WorkingDays.Core
{
    public static class WebHostExtensions
    {
        public static async Task InitAsync(this IWebHost host)
        {
            var services = host.Services.GetServices<IAsyncInitializer>();
            await Task.WhenAll(services.Select(t => t.InitializeAsync()));
        }
    }
}
