using System.Threading.Tasks;

namespace TM.Application.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizationAsync(string login, string password);
    }
}
