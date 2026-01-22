using System.Diagnostics;
using System.Threading.Tasks;
using Exam.Context;
using Exam.ViewModel.EmployeeViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    [Authorize(Roles = "Member, Admin")]
    public class HomeController(AppDbContext _context) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                                                    .Select(x => new EmployeeGetVM
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        ProfessionName = x.Profession.Name,
                                                        ImagePath = x.ImagePath,
                                                        Description = x.Description
                                                    })
                                                    .ToListAsync();

            return View(employees);
        }

    }
}
