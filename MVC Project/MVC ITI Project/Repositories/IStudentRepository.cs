using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task EnrollInCourseAsync(int studentId, int courseId);
        Task<bool> IsEnrolledAsync(int studentId, int courseId);
        Task SetGradeAsync(int studentId, int courseId, double degree);
        Task<Student?> GetWithCoursesAsync(int id);
    }
}


