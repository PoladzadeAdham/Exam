using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModel.UserViewModel
{
    public class LoginVM
    {
        [Required, MaxLength(256), MinLength(2), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(256), DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
