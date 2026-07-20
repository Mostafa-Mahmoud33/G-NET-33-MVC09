using GymManagement.BLL.ViewModels.TrainerViewModels;
using System.Text;

namespace GymApp.BLL.Contracts
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerViewModel>> GetAllTrainersAsync(CancellationToken cancellationToken = default);

        Task<TrainerViewModel?> GetTrainerDetailsAsync(int trainerId, CancellationToken cancellationToken = default);

        Task<TrainerToUpdateViewModel?> GetTrainerToUpdateAsync(int trainerId, CancellationToken cancellationToken = default);

        Task<bool> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default);

        Task<bool> UpdateTrainerDetailsAsync(int trainerId, TrainerToUpdateViewModel model, CancellationToken cancellationToken = default);

        Task<bool> RemoveTrainerAsync(int trainerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TrainerViewModel>> GetTrainersAsync(CancellationToken cancellationToken);
    }
}
