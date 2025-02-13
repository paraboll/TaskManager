using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TM.Application.Interfaces;
using TM.WebServer.Entities;
using TM.WebServer.Extensions;
using TM.WebServer.Factories;

namespace TM.WebServer.Controllers
{
#if RELEASE
    [Authorize]
#endif

    [ApiController]
    [Route("api/v1/")]
    public class BuisnessTaskController : ControllerBase
    {
        private readonly ILogger<BuisnessTaskController> _logger;
        private readonly IBuisnessTaskService _buisnessTaskService;

        public BuisnessTaskController(ILogger<BuisnessTaskController> logger, IBuisnessTaskService buisnessTaskService)
        {
            _logger = logger;

           _buisnessTaskService = buisnessTaskService;
        }

        [HttpGet("buisnessTasks")]
        public async Task<IActionResult> GetAllBuisnessTask()
        {
            var buisnessTasks = (await _buisnessTaskService.GetAllAsync())
                .Select(_ => _.ConvertTo()).ToList();

            return Ok(ResponseFactory.GoodResponse(
                buisnessTasks,
                buisnessTasks.Count
                ));
        }

        [HttpGet("buisnessTasks/{externalId}")]
        public async Task<IActionResult> GetBuisnessTaskByExternalId(string externalId)
        {
            var buisnessTask = await _buisnessTaskService.GetByExternalIdAsync(externalId);
            return Ok(ResponseFactory.GoodResponse(buisnessTask.ConvertTo(), count: 1));
        }

        [HttpPost("buisnessTasks")]
        public async Task<IActionResult> CreateBuisnessTask([FromBody] UIBuisnessTaskDTO newBuisnessTask)
        {
            var buisnessTask = await _buisnessTaskService.CreateAsync(newBuisnessTask.ConvertTo());
            return Ok(ResponseFactory.GoodResponse(buisnessTask.ConvertTo(), count: 1));
        }

        [HttpPut("buisnessTasks")]
        public async Task<IActionResult> EditEmployee([FromBody] UIBuisnessTaskDTO editBuisnessTask)
        {
            var buisnessTask = await _buisnessTaskService.EditAsync(editBuisnessTask.ConvertTo());
            return Ok(ResponseFactory.GoodResponse(buisnessTask.ConvertTo(), count: 1));
        }

        [HttpPost("buisnessTasks/{externalId}/TaskComment")]
        public async Task<IActionResult> CreateTaskComment([FromBody] UITaskCommentDTO newTaskComment)
        {
            var buisnessTask = await _buisnessTaskService.AddTaskCommentAsync(newTaskComment.ConvertTo());
            return Ok(ResponseFactory.GoodResponse(buisnessTask.ConvertTo(), count: 1));
        }

        [HttpDelete("buisnessTasks/{externalId}/TaskComment/{externalCommentId}")]
        public async Task<IActionResult> RemoveTaskComment(string externalId, string externalCommentId)
        {
            var taskComment = new UITaskCommentDTO()
            {
                BuisnessTaskexternalId = externalId,
                ExternalId = externalCommentId
            };

            var buisnessTask = await _buisnessTaskService.RemoveTaskCommentAsync(taskComment.ConvertTo());
            return Ok(ResponseFactory.GoodResponse(buisnessTask.ConvertTo(), count: 1));
        }
    }
}
