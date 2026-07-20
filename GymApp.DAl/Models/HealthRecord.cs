namespace GymApp.DAl.Models
{
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = default!;
        public string? Note { get; set; }

        // Last Updated => update At from BaseEntity

        public int MemberId { get; set; }
        public Member Member { get; set; } = default!;
    }
}
