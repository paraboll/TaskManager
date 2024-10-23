using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TM.Application.Interfaces;
using TM.Domain.Extensions;
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
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;

            _employeeService = employeeService;
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = (await _employeeService.GetAllAsync())
                                                    .Select(_ => _.ConvertTo())
                                                    .ToList();
            return Ok(
                ResponseFactory.GoodResponse(
                    employees,
                    employees.Count
                ));
        }

        [HttpGet("employees/{login}")]
        public async Task<IActionResult> GetEmployeeByLogin(string login)
        {
            var employee = await _employeeService.GetByLoginAsync(login);
            return Ok(ResponseFactory.GoodResponse(employee.ConvertTo(), count: 1));
        }

        [HttpPost("employees")]
        public async Task<IActionResult> CreateEmployee([FromBody] UIEmployeeDTO newEmployee)
        {
            var employee = await _employeeService.CreateAsync(newEmployee.ConvertTo());
            return Ok(ResponseFactory.GoodResponse(employee.ConvertTo(), count: 1));
        }

        [HttpPut("employees")]
        public async Task<IActionResult> EditEmployee([FromBody] UIEmployeeDTO editEmployee)
        {
            var employee = await _employeeService.EditAsync(editEmployee.ConvertTo());
            return Ok(ResponseFactory.GoodResponse(employee.ConvertTo(), count: 1));
        }

        [HttpPut("employees/{login}/lock")]
        public async Task<IActionResult> LockEmployee(string login)
        {
            var employee = await _employeeService.LockAsync(login);
            return Ok(ResponseFactory.GoodResponse(employee.ToEditValid(), count: 1));
        }

        [HttpPut("employees/{login}/unlock")]
        public async Task<IActionResult> UnlockEmployee(string login)
        {
            var employee = await _employeeService.UnlockAsync(login);
            return Ok(ResponseFactory.GoodResponse(employee.ToEditValid(), count: 1));
        }

        [HttpPut("employees/{login}/delete")]
        public async Task<IActionResult> DeleteEmployee(string login)
        {
            var employee = await _employeeService.DeleteAsync(login);
            return Ok(ResponseFactory.GoodResponse(employee.ToEditValid(), count: 1));
        }

        [HttpPut("employees/{login}/reinstate")]
        public async Task<IActionResult> ReinstateEmployee(string login)
        {
            var employee = await _employeeService.ReinstateAsync(login);
            return Ok(ResponseFactory.GoodResponse(employee.ToEditValid(), count: 1));
        }
    }
}
