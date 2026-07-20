using GymApp.DAl.Context;
using GymApp.DAl.Contracts;

namespace GymApp.DAl.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<int> CountOfBookedSlotsAsync(int sessionId, CancellationToken cancellationToken = default)
            => await _context.Bookings.CountAsync(x => x.SessionId == sessionId, cancellationToken);

        public async Task<IEnumerable<Session>> GetAllSessionsWithCategoryAndTrainerAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sessions.AsNoTracking()
                .Include(x => x.Trainer)
                .Include(x => x.Category)
                .ToListAsync(cancellationToken);

        }

        public async Task<Session?> GetSessionWithCategoryAndTrainerByIdAsync(int sessionId, CancellationToken cancellationToken = default)
            => await _context.Sessions.Include(x => x.Trainer)
                                      .Include(x => x.Category)
                                      .FirstOrDefaultAsync(x => x.Id == sessionId, cancellationToken);

    }
}
