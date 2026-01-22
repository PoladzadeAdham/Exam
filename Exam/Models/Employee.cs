using Exam.Models.Common;

namespace Exam.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }


    }
}
