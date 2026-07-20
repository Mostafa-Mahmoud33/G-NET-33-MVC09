using System.ComponentModel.DataAnnotations;

namespace GymApp.BLL.ViewModels.Sessions
{
    public class UpdateSessionViewModel
    {
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 200 characters.")]
        public string Description { get; set; } = default!;
        [Range(0, 30, ErrorMessage = "Capacity must be between 0 and 30.")]
        public int Capacity { get; set; }
        [Display(Name = "Start Date & Time")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date & Time")]
        public DateTime EndDate { get; set; }

        public int TrainerId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
