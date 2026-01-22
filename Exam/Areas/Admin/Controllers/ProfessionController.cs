using System.Threading.Tasks;
using Exam.Context;
using Exam.Models;
using Exam.ViewModel.EmployeeViewModel;
using Exam.ViewModel.ProfessionViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProfessionController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var professions = await _context.Professions
                                                        .Select(x=> new ProfessionGetVM
                                                        {
                                                            Id = x.Id,
                                                            Name = x.Name
                                                        })
                                                        .ToListAsync();

            return View(professions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProfessionCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Profession profession = new()
            {
                Name = vm.Name
            };

            await _context.Professions.AddAsync(profession);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            var profession = await _context.Professions.FindAsync(id);

            if (profession is null)
                return NotFound();

            _context.Professions.Remove(profession);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            var profession = await _context.Professions
                                                 .Select(x=> new ProfessionUpdateVm
                                                 {
                                                     Id = x.Id,
                                                     Name = x.Name
                                                 })
                                                 .FirstOrDefaultAsync(x => x.Id == id);

            return View(profession);

        }

        [HttpPost]
        public async Task<IActionResult> Update(ProfessionUpdateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var profession = await _context.Professions.FindAsync(vm.Id);

            if (profession is null)
                return NotFound();

            profession.Name = vm.Name;

            _context.Professions.Update(profession);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


    }
}
