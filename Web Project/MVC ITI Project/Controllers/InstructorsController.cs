using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ITI_Project.Models;
using MVC_ITI_Project.Repositories;

namespace MVC_ITI_Project.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class InstructorsController : Controller
    {
        private readonly IInstructorRepository _instructors;
        private readonly IDepartmentRepository _departments;
        private readonly ICourseRepository _courses;

        public InstructorsController(IInstructorRepository instructors, IDepartmentRepository departments, ICourseRepository courses)
        {
            _instructors = instructors;
            _departments = departments;
            _courses = courses;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            var instructors = (await _instructors.GetAllAsync(null, q => q.OrderBy(i => i.Name), i => i.Department, i => i.Course)).AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();

                instructors = instructors.Where(i =>
                    i.Name.ToLower().Contains(searchQuery) ||
                    (!string.IsNullOrEmpty(i.Address) && i.Address.ToLower().Contains(searchQuery)) ||
                    i.Salary.ToString().Contains(searchQuery) ||
                    (i.Department != null && i.Department.Name.ToLower().Contains(searchQuery)) ||
                    (i.Course != null && i.Course.Name.ToLower().Contains(searchQuery))
                );
            }

            return View(instructors.ToList());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _instructors.GetWithDetailsAsync(id.Value);

            if (instructor == null) return NotFound();

            return View(instructor);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var departments = await _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DeptId = new SelectList(departments, "Id", "Name");
            ViewBag.CrsId = new SelectList(Enumerable.Empty<SelectListItem>());
            return View();
        }

        [HttpGet]
        public JsonResult GetCoursesByDepartment(int deptId)
        {
            var courses = _courses.GetByDepartmentAsync(deptId).Result
                .Select(c => new { c.Id, c.Name })
                .ToList();

            return Json(courses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Instructor instructor)
        {
            if (string.IsNullOrEmpty(instructor.Image))
            {
                instructor.Image = "defaultIns.png";
            }
            if (ModelState.IsValid)
            {
                var validCourse = (await _courses.GetByDepartmentAsync(instructor.DeptId)).Any(c => c.Id == instructor.CrsId);
                if (!validCourse)
                {
                    ModelState.AddModelError("CrsId", "The selected course does not belong to the chosen department.");
                }
                else
                {
                    await _instructors.AddAsync(instructor);
                    await _instructors.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            var departments = await _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", instructor.DeptId);
            var deptCourses = await _courses.GetByDepartmentAsync(instructor.DeptId);
            ViewBag.CrsId = new SelectList(deptCourses, "Id", "Name", instructor.CrsId);

            return View(instructor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructors.GetWithDetailsAsync(id);

            if (instructor == null)
                return NotFound();

            var departments = await _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", instructor.DeptId);
            var deptCourses = await _courses.GetByDepartmentAsync(instructor.DeptId);
            ViewBag.CrsId = new SelectList(deptCourses, "Id", "Name", instructor.CrsId);

            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Instructor instructor)
        {
            if (string.IsNullOrEmpty(instructor.Image))
            {
                instructor.Image = "defaultIns.png";
            }
            if (ModelState.IsValid)
            {
                var validCourse = (await _courses.GetByDepartmentAsync(instructor.DeptId)).Any(c => c.Id == instructor.CrsId);
                if (!validCourse)
                {
                    ModelState.AddModelError("CrsId", "The selected course does not belong to the chosen department.");
                }
                else
                {
                    _instructors.Update(instructor);
                    await _instructors.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            var departments = await _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", instructor.DeptId);
            var deptCourses = await _courses.GetByDepartmentAsync(instructor.DeptId);
            ViewBag.CrsId = new SelectList(deptCourses, "Id", "Name", instructor.CrsId);

            return View(instructor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _instructors.GetWithDetailsAsync(id.Value);

            if (instructor == null) return NotFound();

            return View(instructor);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _instructors.GetByIdAsync(id);
            if (instructor == null) return NotFound();

            _instructors.Remove(instructor);
            await _instructors.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
