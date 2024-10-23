using System.Threading.Tasks;
using TM.Application.Interfaces;
using TM.Application.Services;
using TM.Application.Tests.Mocks;
using Xunit;

namespace TM.Application.Tests
{
    public class AuthorizationServiceTests
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationServiceTests()
        {
            var mockLogger = LoggerFactory.Mock<IAuthorizationService>();

            _authorizationService = new AuthorizationService(mockLogger, EmployeeFactory.MockAuthorizationService());
        }


        [Theory]
        [InlineData("AuthorizationGood", "AuthorizationGood", true)]
        [InlineData("AuthorizationBad", "AuthorizationBad", false)]
        public async Task Test_Authorization(string login, string password, bool expected)
        {
            var actual = await _authorizationService.AuthorizationAsync(login, password);

            Assert.Equal(expected, actual);
        }
    }
}
