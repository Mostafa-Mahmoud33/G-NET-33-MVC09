namespace GymApp.BLL.Contracts
{
    public interface IMemberService
    {
        Task <IEnumerable<MemberViewModels>> GetAllMembersAsync(CancellationToken cancellationToken = default);
        
        Task<bool> CreateMemberAsync(CreateMemberViewModel createMemberViewModel, CancellationToken cancellationToken = default);

        Task<MemberDetailsViewModel?> GetMemberDetailsViewModelAsync(int id, CancellationToken cancellationToken = default);
        Task<MemberEditViewModel?> GetMemberDetailForEditAsync(int id, CancellationToken cancellationToken = default);

        Task<HealthRecordViewModel?> GetHealthRecordDetailsViewModelAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateMemberAsync(int id, MemberEditViewModel memberViewModel, CancellationToken cancellationToken);

        Task<bool> DeleteMemberAsync(int id, CancellationToken cancellationToken = default);
    }
    /// Data Transfer Object (DTO) for member
    /// MemberDTO is a simplified representation of the Member entity that is used to transfer data between layers of the application.

    //public class MemberDTO
    //{
    // public int Id { get; set; }
    //public string Name { get; set; }
    //public string Email { get; set; }
    //public DateTime DateOfBirth { get; set; }

    //}

    /// ViewModels are used to pass data from controller to the view in an MVC application. They often contain properties that are specific
    /// to the view and may not directly corrspond to the database entities.
}
