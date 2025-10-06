using Microsoft.EntityFrameworkCore;
using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(UniversityContext context) : base(context)
        {
        }

        public Task<Course?> GetWithStudentsAsync(int id)
        {
            return Context.Courses
                .Include(c => c.CourseStudents)
                    .ThenInclude(cs => cs.Student)
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> GetByDepartmentAsync(int departmentId)
        {
            return await Context.Courses
                .Where(c => c.DeptId == departmentId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}


