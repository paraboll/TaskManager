using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TM.Application.Services;

namespace TM.WebServer.Controllers
{
#if RELEASE
    [Authorize]
#endif

    [ApiController]
    [Route("api/v1/")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly ReportService _reportService;

        public ReportController(ILogger<ReportController> logger, ReportService reportService)
        {
            _logger = logger;

           _reportService = reportService;
        }

        [HttpGet("report1")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var relativePath = $"{_reportService.GetReportFolder()}\\Report_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
            var fullPath = Path.Combine(appDir, relativePath);

            await _reportService.SaveTasksToPdf(fullPath);

            return Ok();
        }
    }
}
