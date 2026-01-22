using System.Threading.Tasks;
using Exam.Context;
using Exam.Helpers;
using Exam.Models;
using Exam.ViewModel.EmployeeViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _enviroment;
        private readonly string _folderPath;

        public EmployeeController(AppDbContext context, IWebHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
            _folderPath = Path.Combine(_enviroment.WebRootPath, "images");
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                                                    .Select(x=>new EmployeeGetVM
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        ProfessionName = x.Profession.Name,
                                                        Description = x.Description,
                                                        ImagePath = x.ImagePath
                                                    })
                                                    .ToListAsync();

            return View(employees);
        }


        public async Task<IActionResult> Create()
        {
            await SendProfessionWithViewBag();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {
            await SendProfessionWithViewBag();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existingProfession = await _context.Professions.AnyAsync(x => x.Id == vm.ProfessionId);

            if (!existingProfession)
            {
                ModelState.AddModelError("", "Bele bir profession movcud deil!");
                return View();
            }

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("", "seklin olcusu 2 mbdan artiq ola bilmez!");
                return View();
            }

            if (!vm.Image.CheckType())
            {
                ModelState.AddModelError("", "sekil image tipinde olmalidir.");
                return View();
            }

            string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);

            Employee employee = new()
            {
                Name = vm.Name,
                Description = vm.Description,
                ImagePath = uniqueFileName,
                ProfessionId = vm.ProfessionId
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee is null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_folderPath, employee.ImagePath);

            ExtensionMethod.DeleteFile(path);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            await SendProfessionWithViewBag();

            var employee = await _context.Employees
                                                    .Select(x=> new EmployeeUpdateVM
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        Description = x.Description,
                                                        ProfessionId = x.ProfessionId
                                                    })
                                                    .FirstOrDefaultAsync(x => x.Id == id);

            return View(employee);

        }


        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdateVM vm)
        {
            await SendProfessionWithViewBag();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existingProfession = await _context.Professions.AnyAsync(x => x.Id == vm.ProfessionId);

            if (!existingProfession)
            {
                ModelState.AddModelError("", "Bele bir profession movcud deil!");
                return View();
            }

            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("", "seklin olcusu 2 mbdan artiq ola bilmez!");
                return View();
            }

            if (!vm.Image?.CheckType() ?? false)
            {
                ModelState.AddModelError("", "sekil image tipinde olmalidir.");
                return View();
            }


            var employee = await _context.Employees.FindAsync(vm.Id);
            if (employee is null)
                return NotFound();

            if (vm.Image is { })
            {
                string path = Path.Combine(_folderPath, employee.ImagePath);

                ExtensionMethod.DeleteFile(path);

                string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);

                employee.ImagePath = uniqueFileName;

            }

            employee.Name = vm.Name;
            employee.Description = vm.Description;
            employee.ProfessionId = vm.ProfessionId;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        private async Task SendProfessionWithViewBag()
        {
            var professions = await _context.Professions.ToListAsync();

            ViewBag.Professions = professions;
        }
    }
}
