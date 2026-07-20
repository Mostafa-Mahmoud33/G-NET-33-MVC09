using System.ComponentModel.DataAnnotations.Schema;

namespace GymApp.DAl.Models
{
    public class Membership : BaseEntity
    {
        public Member Member { get; set; } = default!;

        public Plan Plan { get; set; } = default!;
        public int PlanId { get; set; }
        public int MemberId { get; set; }

        // Start date => Base Entity .Created At

        public DateOnly EndDate { get; set; }

        // Is Active End Date > CurrentDate
        [NotMapped]
        public bool IsActive => EndDate > DateOnly.FromDateTime(DateTime.Now);

        // string Status
        [NotMapped]
        public string Status => IsActive ? "Active" : "Inactive";

    }
}
