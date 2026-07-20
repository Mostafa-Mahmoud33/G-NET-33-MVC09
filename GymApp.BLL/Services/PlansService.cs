using GymApp.BLL.Contracts;
using GymApp.DAl.Contracts;
using GymApp.DAl.Models;
using GymManagement.BLL.ViewModels.PlanViewModels;

namespace GymApp.BLL.Services
{
    public class PlansService : IPlansService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlansService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PlanViewModel>> PlansAsync(CancellationToken cancellationToken = default)
        {
            var plans = await _unitOfWork
                .GetRepository<Plan>()
                .GetAllAsync(false, cancellationToken);

            return plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive
            });
        }

        public async Task<PlanViewModel?> GetPlanByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var plan = await _unitOfWork
                .GetRepository<Plan>()
                .GetByIdAsync(id, cancellationToken);

            if (plan == null)
                return null;

            return new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive
            };
        }
    }
}