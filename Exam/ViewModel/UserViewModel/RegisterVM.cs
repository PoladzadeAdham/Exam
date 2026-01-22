using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModel.UserViewModel
{
    public class RegisterVM
    {
        [Required, MaxLength(256), MinLength(2)]
        public string UserName { get; set; }
        [Required, MaxLength(256), MinLength(2)]
        public string FullName { get; set; }
        [Required, MaxLength(256), MinLength(2), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(256), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(256), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
