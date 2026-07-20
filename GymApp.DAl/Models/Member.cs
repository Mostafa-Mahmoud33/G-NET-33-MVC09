namespace GymApp.DAl.Models
{
    public class Member : GymUser
    {
        public string Photo { get; set; } = string.Empty;

        public ICollection<Membership> MemberPlans { get; set; } = [];
        public HealthRecord HealthRecord { get; set; } = default!;
        public ICollection<Booking> MemberSessions { get; set; } = [];
    }
}
