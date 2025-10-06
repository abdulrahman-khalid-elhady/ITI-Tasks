using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ITI_Project.Models;
using MVC_ITI_Project.Repositories;

namespace MVC_ITI_Project.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courses;
        private readonly IDepartmentRepository _departments;

        public CoursesController(ICourseRepository courses, IDepartmentRepository departments)
        {
            _courses = courses;
            _departments = departments;
        }
        public IActionResult Index(string SearchString)
        {
            ViewData["CurrentFilter"] = SearchString;
            var courses = _courses.GetAllAsync(null, null, c => c.Department, c => c.CourseStudents).Result.AsQueryable();
            if (!string.IsNullOrEmpty(SearchString))
            {
                var searchLower = SearchString.ToLower();

                courses = courses.Where(s =>
                s.Name.ToLower().Contains(searchLower) ||
                s.Degree.ToString().Contains(searchLower) ||
                s.Hours.ToString().Contains(searchLower) ||
                (s.Department != null && s.Department.Name.ToLower().Contains(searchLower)) ||
                s.Id.ToString().Contains(searchLower));
            }

            return View(courses.ToList());
        }
        [HttpGet]
        public IActionResult Add() {
            var departments = _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name)).Result;
            ViewBag.DeptId = new SelectList(departments, "Id", "Name");
            return View();

        }
        [HttpPost]
        public IActionResult Add(Course course) {
            if (ModelState.IsValid)
            {
                _courses.AddAsync(course).Wait();
                _courses.SaveChangesAsync().Wait();
                return RedirectToAction("Index");
            }
            var departments = _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name)).Result;
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", course.DeptId);
            return View(course);
        }

        public IActionResult Details(int id)
        {
            var course = _courses.GetWithStudentsAsync(id).Result;

            if (course == null)
                return NotFound();

            return View(course);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courses.GetByIdAsync(id, c => c.Department).Result;

            if (course == null)
                return NotFound();

            var departments = _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name)).Result;
            ViewBag.DeptId = new SelectList(departments, "Id", "Name", course.DeptId);

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departments.GetAllAsync(orderBy: q => q.OrderBy(d => d.Name)).Result;
                ViewBag.DeptId = new SelectList(departments, "Id", "Name", course.DeptId);
                return View(course);
            }

            _courses.Update(course);
            _courses.SaveChangesAsync().Wait();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _courses.GetByIdAsync(id, c => c.Department).Result;

            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var course = _courses.GetByIdAsync(id).Result;
            if (course == null)
                return NotFound();

            _courses.Remove(course);
            _courses.SaveChangesAsync().Wait();
            return RedirectToAction(nameof(Index));
        }
    }
}