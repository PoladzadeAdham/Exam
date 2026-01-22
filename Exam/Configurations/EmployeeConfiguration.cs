using Exam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
            builder.Property(x => x.ImagePath).IsRequired();

            builder.HasOne(x => x.Profession).WithMany(x => x.Employees).HasForeignKey(x => x.ProfessionId);
        }
    }
}
