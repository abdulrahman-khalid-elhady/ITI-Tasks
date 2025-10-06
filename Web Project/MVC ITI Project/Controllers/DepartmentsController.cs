using Microsoft.AspNetCore.Mvc;
using MVC_ITI_Project.Models;
using MVC_ITI_Project.Repositories;
using System.Linq.Expressions;

namespace MVC_ITI_Project.Controllers
{

    [Microsoft.AspNetCore.Authorization.Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departments;

        public DepartmentsController(IDepartmentRepository departments)
        {
            _departments = departments;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var departments = await _departments.GetAllAsync(
                orderBy: q => q.OrderBy(d => d.Name),
                includes: new Expression<Func<Department, object>>[]
                {
            d => d.Students,
            d => d.Courses,
            d => d.Instructors
                }
            );

            if (!string.IsNullOrEmpty(searchString))
            {
                var lower = searchString.ToLower();
                departments = departments.Where(d =>
                    (d.Name != null && d.Name.ToLower().Contains(lower)) ||
                    (!string.IsNullOrEmpty(d.ManagerName) && d.ManagerName.ToLower().Contains(lower))
                );
            }

            return View(departments.ToList());
        }


        public async Task<IActionResult> Details(int id)
        {
            var department = await _departments.GetWithMembersAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        public async Task<IActionResult> Add(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departments.AddAsync(department);
                await _departments.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departments.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _departments.Update(department);
                await _departments.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departments.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _departments.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _departments.Remove(department);
            await _departments.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
