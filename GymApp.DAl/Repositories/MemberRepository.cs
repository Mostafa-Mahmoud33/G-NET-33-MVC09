using GymApp.DAl.Context;
using GymApp.DAl.Contracts;

namespace GymApp.DAl.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {

        public MemberRepository(GymDbContext context) : base(context) 
        {

        }
        //public Task<bool> AnyAsync(Exception<Func<Member, bool>> predicate, CancellationToken cancellationToken = default)
          //  => _context.Members.AnyAsync(predicate, cancellationToken);



        public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
            =>_context.Members.AnyAsync(m => m.Email == email, cancellationToken);
        
        public Task<bool> PhoneExistsAsync(string phone, CancellationToken cancellationToken = default)
            => _context.Members.AnyAsync(m=> m.Phone == phone, cancellationToken);

    }
}
