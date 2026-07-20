using GymApp.BLL.Common;
using GymApp.BLL.ViewModels.Sessions;
using GymApp.DAl.Contracts;

namespace GymApp.BLL.Contracts
{
    public interface ISessionService 
    {
        public Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default!);

        Task<IEnumerable<CategorySelectItemViewModel>> GetCategorySelectItemsAsync(CancellationToken cancellationToken = default!);
        Task<Result> CreateSessionAsync(CreateSessionViewModel model, CancellationToken cancellationToken = default!);

        Task<Result<SessionViewModel>> GetSessionByIdAsync(int sessionId, CancellationToken cancellationToken = default!);
        Task<Result<UpdateSessionViewModel>> GetSessionToUbdateAsync(int sessionId, CancellationToken cancellationToken = default!);
        Task<Result> UpdateSessionAsync(int sessionId, UpdateSessionViewModel model, CancellationToken cancellationToken = default!);
        Task<bool> DeleteSessionAsync(int sessionId, CancellationToken cancellationToken = default);
    }
}
