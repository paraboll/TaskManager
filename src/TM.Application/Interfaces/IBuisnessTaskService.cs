using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Entities;

namespace TM.Application.Interfaces
{
    public interface IBuisnessTaskService
    {
        Task<BuisnessTask> CreateAsync(BuisnessTask buisnessTask);
        Task<BuisnessTask> EditAsync(BuisnessTask buisnessTask);

        Task<IEnumerable<BuisnessTask>> GetAllAsync();
        Task<BuisnessTask> GetByExternalIdAsync(string extensionId);
        Task<IEnumerable<BuisnessTask>> SearchByNameAsync(string name);

        Task<BuisnessTask> AddTaskCommentAsync(TaskComment taskComment);
        Task<BuisnessTask> RemoveTaskCommentAsync(TaskComment taskComment);
    }
}
