using System.ComponentModel.DataAnnotations;

namespace GymApp.BLL.ViewModels.Account
{
    public class LoginViewModel
    {
        public string Email { get; set; } = default!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; }
    }
}
