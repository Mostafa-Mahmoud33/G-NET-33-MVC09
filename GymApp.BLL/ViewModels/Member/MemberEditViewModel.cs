using GymApp.DAl.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GymApp.BLL.ViewModels.Member
{
    public class MemberEditViewModel
    {
        public string? Photo { get; set; } 
        public string? Name { get; set; } = default!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        // [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Phone is required")]
        //reg ex for phone validation can be added here, such as:
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = default!;
        

        // Address properties
        [Required(ErrorMessage = "Building number is required")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street is required")]

        public string Street { get; set; } = default!;
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

    }
}
