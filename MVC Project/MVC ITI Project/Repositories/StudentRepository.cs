using Microsoft.EntityFrameworkCore;
using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(UniversityContext context) : base(context)
        {
        }

        public async Task EnrollInCourseAsync(int studentId, int courseId)
        {
            var exists = await Context.CourseStudents.AnyAsync(cs => cs.StdId == studentId && cs.CrsId == courseId);
            if (exists)
            {
                return;
            }
            await Context.CourseStudents.AddAsync(new CourseStudents
            {
                StdId = studentId,
                CrsId = courseId,
                Degree = 0
            });
            await Context.SaveChangesAsync();
        }

        public Task<bool> IsEnrolledAsync(int studentId, int courseId)
        {
            return Context.CourseStudents.AnyAsync(cs => cs.StdId == studentId && cs.CrsId == courseId);
        }

        public async Task SetGradeAsync(int studentId, int courseId, double degree)
        {
            var cs = await Context.CourseStudents.FirstOrDefaultAsync(x => x.StdId == studentId && x.CrsId == courseId);
            if (cs == null)
            {
                throw new InvalidOperationException("Student is not enrolled in the course.");
            }
            cs.Degree = degree;
            await Context.SaveChangesAsync();
        }

        public Task<Student?> GetWithCoursesAsync(int id)
        {
            return Context.Students
                .Include(s => s.CourseStudents)
                    .ThenInclude(cs => cs.Course)
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}


