using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ITI_Project.Models;
using MVC_ITI_Project.Repositories;

namespace MVC_ITI_Project.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _students;
        private readonly IDepartmentRepository _departments;

        public StudentsController(IStudentRepository students, IDepartmentRepository departments)
        {
            _students = students;
            _departments = departments;
        }

        public async Task<IActionResult> Index(string SearchString)
        {
            ViewData["CurrentFilter"] = SearchString;
            var all = await _students.GetAllAsync(null, null, s => s.Department, s => s.CourseStudents);
            if (!string.IsNullOrEmpty(SearchString))
            {
                var searchLower = SearchString.ToLower();
                all = all.Where(s =>
                    (s.Name != null && s.Name.ToLower().Contains(searchLower)) ||
                    (!string.IsNullOrEmpty(s.Address) && s.Address.ToLower().Contains(searchLower)) ||
                    s.Grade.ToString().Contains(searchLower) ||
                    (s.Department != null && s.Department.Name.ToLower().Contains(searchLower)) ||
                    s.Id.ToString().Contains(searchLower));
            }
            return View(all.ToList());
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _students.GetWithCoursesAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var departments = await _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DeptId = new SelectList(departments, "Id", "Name");

            var student = await _students.GetWithCoursesAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _students.GetWithCoursesAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var student = await _students.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            _students.Remove(student);
            await _students.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _students.Update(student);
                await _students.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var departments = await _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", student.DeptId);
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var departments = await _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DeptId = new SelectList(departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            if (ModelState.IsValid)
            {
                await _students.AddAsync(student);
                await _students.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var departments = await _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", student.DeptId);
            return View(student);
        }
    }
}
