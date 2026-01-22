using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModel.ProfessionViewModel
{
    public class ProfessionCreateVM
    {
        [Required, MaxLength(256)]          
        public string Name { get; set; }
    }
}
