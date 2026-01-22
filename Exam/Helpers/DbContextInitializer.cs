using Exam.Models;
using Exam.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Identity;

namespace Exam.Helpers
{
    public class DbContextInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly UserVm _userVm;

        public DbContextInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userVm = _configuration.GetSection("AdminSetting").Get<UserVm>() ?? new();
        }

        public async Task Initialize()
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Admin"
            });

            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Member"
            });

            AppUser admin = new()
            {
                UserName = _userVm.UserName,
                Email = _userVm.Email,
                FullName = _userVm.FullName,
            };

            var result = await _userManager.CreateAsync(admin, _userVm.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

        }


    }
}
