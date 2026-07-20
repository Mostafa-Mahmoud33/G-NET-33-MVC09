using GymApp.DAl.Models;

namespace GymApp.DAl.Contracts
{
    public interface IPlansRepository
    {
        Task AddAsync(Plan plan, CancellationToken cancellationToken = default);
        Task DeleteAsync(Plan plan, CancellationToken cancellationToken = default);
        Task<IEnumerable<Plan>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default);
        Task<Plan?> GetById(int id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Plan plan, CancellationToken cancellationToken = default);
    }
}