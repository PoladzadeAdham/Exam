using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModel.ProfessionViewModel
{
    public class ProfessionUpdateVm
    {
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }
    }
}
