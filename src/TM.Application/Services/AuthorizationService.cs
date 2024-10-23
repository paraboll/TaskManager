using TM.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TM.Domain.Extensions;

namespace TM.Application.Services
{
    public class AuthorizationService: IAuthorizationService
    {
        private readonly ILogger<IAuthorizationService> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public AuthorizationService(ILogger<IAuthorizationService> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;

            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AuthorizationAsync(string login, string password)
        {
            var employee = await _employeeRepository.GetByLoginAsync(login);
            if (employee == null)
            {
                _logger.LogTrace($"Authorization. login: {login} ненайден.");
                return false;
            }

            var hashInputPassword = EmployeeExtensions.ComputeHashedPassword(password);
            if(employee.HashPassword == hashInputPassword) 
                return true;

            return false;
        }
    }
}
