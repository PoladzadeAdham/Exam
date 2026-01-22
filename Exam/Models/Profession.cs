using Exam.Models.Common;

namespace Exam.Models
{
    public class Profession : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = [];
    }
}
