using Exam.Models;

namespace Exam.ViewModel.EmployeeViewModel
{
    public class EmployeeGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string ProfessionName { get; set; } = string.Empty;
    }
}
