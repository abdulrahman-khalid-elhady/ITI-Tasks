using Microsoft.EntityFrameworkCore;
using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public class CourseStudentsRepository : Repository<CourseStudents>, ICourseStudentsRepository
    {
        public CourseStudentsRepository(UniversityContext context) : base(context)
        {
        }

        public Task<bool> ExistsAsync(int studentId, int courseId)
        {
            return Context.CourseStudents.AnyAsync(x => x.StdId == studentId && x.CrsId == courseId);
        }

        public Task<CourseStudents?> GetWithDetailsAsync(int id)
        {
            return Context.CourseStudents
                .Include(x => x.Course)
                .Include(x => x.Student)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CourseStudents>> GetAllWithDetailsAsync()
        {
            return await Context.CourseStudents
                .Include(x => x.Course)
                .Include(x => x.Student)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}


