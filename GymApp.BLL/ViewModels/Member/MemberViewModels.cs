namespace GymApp.BLL.ViewModels.Member
{
    public class MemberViewModels
    {
        public int Id { get; set; }
        public string? Photo { get; set; } 
        public string Name { get; set; } = default!;
        
        public string Phone { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Email { get; set; } = default!;

        // ui related properties can be added here, Such as : SelectList for dropdowns, etc.

    }
}
