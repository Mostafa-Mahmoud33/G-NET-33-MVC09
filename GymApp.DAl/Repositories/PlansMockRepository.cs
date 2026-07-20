using GymApp.DAl.Contracts;
using GymApp.DAl.Models;

namespace GymApp.DAl.Repositories
{
    public class PlansMockRepository //: IPlansRepository
    {
        private static List<Plan> _plans = new()
        {
            new Plan { Id = 1, Name = "Plan A", Description = "Description for Plan A", Price = 29.99m },
            new Plan { Id = 2, Name = "Plan B", Description = "Description for Plan B", Price = 49.99m },
            new Plan { Id = 3, Name = "Plan C", Description = "Description for Plan C", Price = 69.99m }
        };

        public Task<IEnumerable<Plan>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_plans.AsEnumerable());
        }

        public Task<Plan?> GetById(int id, CancellationToken cancellationToken = default)
        {
            var plan = _plans.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(plan);
        }

        public Task AddAsync(Plan plan, CancellationToken cancellationToken = default)
        {
            plan.Id = _plans.Max(x => x.Id) + 1;
            _plans.Add(plan);

            return Task.CompletedTask;
        }
    }
}