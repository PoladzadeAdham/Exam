using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModel.EmployeeViewModel
{
    public class EmployeeCreateVM
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }
        [Required, MaxLength(256)]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int ProfessionId { get; set; }
    }
}
