using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ITI_Project.Models;
using MVC_ITI_Project.Repositories;

namespace MVC_ITI_Project.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CourseStudentsController : Controller
    {
        private readonly ICourseStudentsRepository _courseStudents;
        private readonly IStudentRepository _students;
        private readonly ICourseRepository _courses;

        public CourseStudentsController(ICourseStudentsRepository courseStudents, IStudentRepository students, ICourseRepository courses)
        {
            _courseStudents = courseStudents;
            _students = students;
            _courses = courses;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _courseStudents.GetAllWithDetailsAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _courseStudents.GetWithDetailsAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var students = await _students.GetAllAsync(orderBy: q => q.OrderBy(s => s.Name));
            var courses = await _courses.GetAllAsync(orderBy: q => q.OrderBy(c => c.Name));
            ViewBag.StdId = new SelectList(students, "Id", "Name");
            ViewBag.CrsId = new SelectList(courses, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseStudents model)
        {
            if (ModelState.IsValid)
            {
                if (await _courseStudents.ExistsAsync(model.StdId, model.CrsId))
                {
                    ModelState.AddModelError(string.Empty, "Student is already enrolled in this course.");
                }
                else
                {
                    await _courseStudents.AddAsync(model);
                    await _courseStudents.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            var students = await _students.GetAllAsync(orderBy: q => q.OrderBy(s => s.Name));
            var courses = await _courses.GetAllAsync(orderBy: q => q.OrderBy(c => c.Name));
            ViewBag.StdId = new SelectList(students, "Id", "Name", model.StdId);
            ViewBag.CrsId = new SelectList(courses, "Id", "Name", model.CrsId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _courseStudents.GetByIdAsync(id);
            if (item == null) return NotFound();
            var students = await _students.GetAllAsync(orderBy: q => q.OrderBy(s => s.Name));
            var courses = await _courses.GetAllAsync(orderBy: q => q.OrderBy(c => c.Name));
            ViewBag.StdId = new SelectList(students, "Id", "Name", item.StdId);
            ViewBag.CrsId = new SelectList(courses, "Id", "Name", item.CrsId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseStudents model)
        {
            if (ModelState.IsValid)
            {
                _courseStudents.Update(model);
                await _courseStudents.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var students = await _students.GetAllAsync(orderBy: q => q.OrderBy(s => s.Name));
            var courses = await _courses.GetAllAsync(orderBy: q => q.OrderBy(c => c.Name));
            ViewBag.StdId = new SelectList(students, "Id", "Name", model.StdId);
            ViewBag.CrsId = new SelectList(courses, "Id", "Name", model.CrsId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _courseStudents.GetWithDetailsAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _courseStudents.GetByIdAsync(id);
            if (item == null) return NotFound();
            _courseStudents.Remove(item);
            await _courseStudents.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
