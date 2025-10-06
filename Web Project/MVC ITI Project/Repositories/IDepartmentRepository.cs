using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<bool> NameExistsAsync(string name, int? ignoreId = null);
        Task<Department?> GetWithMembersAsync(int id);
    }
}


