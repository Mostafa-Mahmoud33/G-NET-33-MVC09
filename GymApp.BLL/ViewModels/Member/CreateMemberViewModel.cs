using GymApp.DAl.Models.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace GymApp.BLL.ViewModels.Member
{
    public class CreateMemberViewModel
    {
        // Stream
        // Image => IFromFile
        public IFormFile? Photo { get; set; }

        // Member Properties
        [Required(ErrorMessage = "Name is required.")]
        //reg ex for name validation can be added here, such as:
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        // [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Phone number is required")]
        //reg ex for phone validation can be added here, such as:
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = default!;
        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        // Address properties
        [Required(ErrorMessage = "Building number is required")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street is required")]

        public string Street { get; set; } = default!;
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Health record is required")]
        public HealthRecordViewModel HealthRecord { get; set; } = default!;
    }
}
