using System.ComponentModel.DataAnnotations;

namespace GymApp.BLL.ViewModels.Member
{
    public class HealthRecordViewModel
    {
        [Range(1,300, ErrorMessage = "Height must be btween 1 and 300")]
        public decimal Height { get; set; }
        [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "Blood type is required")]
        [StringLength(3, ErrorMessage = "Blood type must be exactly 3 characters")]
        public string BloodType { get; set; } = default!;
        [StringLength(250, ErrorMessage = "Note must be btween 0 and 250 characters")]
        public string? Note { get; set; }
    }
}
