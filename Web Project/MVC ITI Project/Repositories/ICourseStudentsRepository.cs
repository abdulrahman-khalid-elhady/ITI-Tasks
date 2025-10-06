using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public interface ICourseStudentsRepository : IRepository<CourseStudents>
    {
        Task<bool> ExistsAsync(int studentId, int courseId);
        Task<CourseStudents?> GetWithDetailsAsync(int id);
        Task<IEnumerable<CourseStudents>> GetAllWithDetailsAsync();
    }
}


