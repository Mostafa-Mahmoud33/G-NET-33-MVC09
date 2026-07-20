using GymApp.DAl.Models;
using GymManagement.BLL.ViewModels.PlanViewModels;
namespace GymApp.BLL.Contracts
{
    public interface IPlansService
    {
        Task<IEnumerable<PlanViewModel>> PlansAsync(CancellationToken cancellationToken = default);

        Task<PlanViewModel?> GetPlanByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}