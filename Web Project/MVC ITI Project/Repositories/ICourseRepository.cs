using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course?> GetWithStudentsAsync(int id);
        Task<IEnumerable<Course>> GetByDepartmentAsync(int departmentId);
    }
}


