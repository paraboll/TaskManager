using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TM.Application.Interfaces;
using TM.Application.Services;

namespace TM.WebServer.Extensions
{
    public static class ReportServiceExtensions
    {
        public static IServiceCollection AddReportServiceConfigure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(Path.Combine(appDir, configuration["ReportFolder"])))
                Directory.CreateDirectory(Path.Combine(appDir, configuration["ReportFolder"]));

            services.AddScoped<ReportService>(_ => 
                new ReportService(
                    _.GetService<ILogger<ReportService>>(),
                    _.GetService<IBuisnessTaskRepository>(),
                    configuration["ReportFolder"]
                    ));

            return services;
        }
    }
}
