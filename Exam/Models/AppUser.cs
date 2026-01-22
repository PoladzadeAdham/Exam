using Microsoft.AspNetCore.Identity;

namespace Exam.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
