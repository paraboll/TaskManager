using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TM.Application.Builders;
using TM.Application.Interfaces;

namespace TM.Application.Services
{
    public class ReportService
    {
        private readonly ILogger<ReportService> _logger;
        private readonly IBuisnessTaskRepository _buisnessTaskRepository;
        private readonly string _reportFolder;

        public ReportService(ILogger<ReportService> logger, 
            IBuisnessTaskRepository buisnessTaskRepository, string reportFolder)
        {
            _logger = logger;

            _reportFolder = reportFolder;
            _buisnessTaskRepository = buisnessTaskRepository;
        }

        public async Task SaveTasksToPdf(string filePath)
        {
            var pdfBuilder = new PdfBuilder();
            pdfBuilder.SetTitle("Business Tasks");

            string[] headers = { "Id", "ExternalId", "Name", "Description" };
            pdfBuilder.SetHeaders(headers);

            foreach (var task in await _buisnessTaskRepository.GetAllAsync())
            {
                string[] values = { task.Id.ToString(), task.ExternalId, task.Name, task.Description };
                pdfBuilder.AddRow(values);
            }

            pdfBuilder.Save(filePath);
        }

        public string GetReportFolder() { return _reportFolder; }
    }
}
