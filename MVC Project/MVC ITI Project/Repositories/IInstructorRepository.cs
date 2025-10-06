using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        Task<Instructor?> GetWithDetailsAsync(int id);
        Task<IEnumerable<Instructor>> GetByDepartmentAsync(int departmentId);
    }
}


