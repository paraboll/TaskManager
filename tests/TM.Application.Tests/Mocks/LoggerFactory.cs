using Microsoft.Extensions.Logging;
using Moq;

namespace TM.Application.Tests.Mocks
{
    internal static class LoggerFactory
    {
        public static ILogger<T> Mock<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();
            return mockLogger.Object;
        }
    }
}
