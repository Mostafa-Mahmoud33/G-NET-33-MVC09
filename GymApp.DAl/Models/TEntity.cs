namespace GymApp.DAl.Models
{
    public class TEntity : GymUser
    {
        public string Photo { get; set; } = default!;
        // JoinDate =>

        public ICollection<Membership> MemberPlans { get; set; } = [];
        public HealthRecord HealthRecord { get; set; } = default!;
        public ICollection<Booking> MemberSessions { get; set; } = [];

    }
}
