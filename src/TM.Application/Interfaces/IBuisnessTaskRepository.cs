using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Entities;

namespace TM.Application.Interfaces
{
    public interface IBuisnessTaskRepository
    {
        Task<BuisnessTask> AddAsync(BuisnessTask buisnessTask);
        Task AddsAsync(List<BuisnessTask> buisnessTasks);
        Task<BuisnessTask> EditAsync(BuisnessTask buisnessTask);

        Task<IEnumerable<BuisnessTask>> GetAllAsync();
        Task<BuisnessTask> GetByExternalIdIdAsync(string extensionId);
        Task<IEnumerable<BuisnessTask>> SearchByNameAsync(string name);

        Task<BuisnessTask> AddTaskCommentAsync(TaskComment taskComment);
        Task<BuisnessTask> RemoveTaskCommentAsync(TaskComment taskComment);
    }
}
