using System.ComponentModel.DataAnnotations;

namespace Company.ViewModels
{
    public class LoginViewModel
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
