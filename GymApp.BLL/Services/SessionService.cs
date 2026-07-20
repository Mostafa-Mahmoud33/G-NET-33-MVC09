using GymApp.BLL.Common;
using GymApp.BLL.ViewModels.Sessions;
using GymApp.DAl.Contracts;
using GymApp.DAl.Models.Enums;

namespace GymApp.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> CreateSessionAsync(CreateSessionViewModel model, CancellationToken cancellationToken = default)
        {
            //validate End date > Start date
            if (model.EndDate < model.StartDate)
                return Result.ValidationError("End date must be greater than start date");
            // start date > now
            if (model.StartDate > DateTime.Now)
                return Result.ValidationError("Start date must be in the future");
            var trainer = await _unitOfWork.GetRepository<Trainer>()
                .GetByIdAsync(model.TrainerId, cancellationToken);
            if (trainer is null) 
                return Result.NotFound($"Trainer with Id {model.TrainerId} not found");


            var isTrainerAvailable = await _unitOfWork.GetRepository<Trainer>()
                .AnyAsync(t => t.Sessions.Any(s => s.StartDate < model.EndDate && s.EndDate > model.StartDate));


            var category = await _unitOfWork.GetRepository<Category>()
                .GetByIdAsync(model.CategoryId, cancellationToken);
            if (category is null)
                return Result.NotFound($"Category with Id {model.CategoryId} not found");

            // Trainer specialty must match Category
            var isValidSpecialty = Enum.TryParse<Specialties>(category.Name, true, out _);
            if (!isValidSpecialty)
                return Result.ValidationError("Trainer specialty does not match category");
            // mapping

            var session = _mapper.Map<CreateSessionViewModel, Session>(model);
            _unitOfWork.Sessions.Add(session);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            return result ? Result.Ok() : Result.ValidationError("Failed to create session");

        }

        public async Task<bool> DeleteSessionAsync(int id, CancellationToken cancellationToken = default)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(id, cancellationToken);

            if (session == null)
                throw new Exception("Session Not Found");

            _unitOfWork.Sessions.Delete(session);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0;
        }

        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default)
        {
            var sessions = await _unitOfWork.Sessions
                .GetAllSessionsWithCategoryAndTrainerAsync(cancellationToken : cancellationToken);

            var result = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);



            foreach (var item in result)
            {
                item.AvailableSlots = item.Capacity - await _unitOfWork.Sessions.CountOfBookedSlotsAsync(item.Id);
            }
            return result;
        }

        public async Task<IEnumerable<CategorySelectItemViewModel>> GetCategorySelectItemsAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(cancellationToken: cancellationToken);
            return _mapper.Map<IEnumerable<CategorySelectItemViewModel>>(categories);
        }

        public async Task<Result<SessionViewModel>> GetSessionByIdAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            // get session
            var session = await _unitOfWork.Sessions.GetSessionWithCategoryAndTrainerByIdAsync(sessionId, cancellationToken);
            if (session is null)
                return Result<SessionViewModel>.NotFound($"Session with Id {sessionId} not found");


            // return result
            var sessionViewModel = _mapper.Map<SessionViewModel>(session);

            return Result<SessionViewModel>.Ok(sessionViewModel);
        }

        public async Task<Result<UpdateSessionViewModel>> GetSessionToUbdateAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(sessionId, cancellationToken);

            if (session is null)
                return Result<UpdateSessionViewModel>.NotFound($"Session with Id {sessionId} not found");

            var updateSessionViewModel = _mapper.Map<UpdateSessionViewModel>(session);
            return Result<UpdateSessionViewModel>.Ok(updateSessionViewModel);
        }

        public async Task<Result> UpdateSessionAsync(int sessionId, UpdateSessionViewModel model, CancellationToken cancellationToken = default)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(sessionId, cancellationToken);

            if (session is null)
                return Result.NotFound($"Session with Id {sessionId} not found");


            // validate session not started yet
            if (session.StartDate <= DateTime.Now)
                return Result.ValidationError("Session has already started and cannot be updated");



            if (model.EndDate < model.StartDate)
                return Result.ValidationError("End date must be greater than start date");
            // start date > now
            if (model.StartDate > DateTime.Now)
                return Result.ValidationError("Start date must be greater than now");

            var trainer = await _unitOfWork.GetRepository<Trainer>()
                .GetByIdAsync(model.TrainerId,cancellationToken);
            if (trainer is null)
                return Result.NotFound($"Trainer with Id {model.TrainerId} not found");


            // Trainer specialty must match Category
            var isValidSpecialty = Enum.TryParse<Specialties>(session.Category.Name, true, out _);

            if (isValidSpecialty)
                return Result.ValidationError("Trainer specialty does not match category");
            var isTrainerAvailable = await _unitOfWork.GetRepository<Trainer>()
               .AnyAsync(t => t.Sessions.Any(s => s.StartDate < model.EndDate && s.EndDate > model.StartDate && s.Id != sessionId), cancellationToken);

            if (isTrainerAvailable)
                return Result.ValidationError("Trainer is not available at this time");
            var updatedSession = _mapper.Map(model, session);
            _unitOfWork.Sessions.Update(updatedSession);

            var isUpdated = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > 0;

            return isUpdated ? Result.Ok() : Result.ValidationError("Failed to update session");
        }
    }
}
