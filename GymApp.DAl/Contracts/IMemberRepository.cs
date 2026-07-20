using GymApp.DAl.Models;
namespace GymApp.DAl.Contracts;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> PhoneExistsAsync(string phone, CancellationToken cancellationToken = default);

    //Task<bool> AnyAsync(Exception<Func<Member, bool>> predicate, CancellationToken cancellationToken = default);
}
