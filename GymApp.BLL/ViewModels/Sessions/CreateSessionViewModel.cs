using System.ComponentModel.DataAnnotations;

namespace GymApp.BLL.ViewModels.Sessions
{
    public class CreateSessionViewModel
    {
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 200 characters.")]
        public string Description { get; set; } = default!;

        [Range(1, 25, ErrorMessage = "Capacity must be between 1 and 25.")]
        public int Capacity { get; set; }

        [Display(Name = "Start Date & Time")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date & Time")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Trainer")]
        public int TrainerId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

    }
}
