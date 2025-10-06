using Microsoft.EntityFrameworkCore;
using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(UniversityContext context) : base(context)
        {
        }

        public async Task<bool> NameExistsAsync(string name, int? ignoreId = null)
        {
            var query = Context.Departments.AsQueryable();
            if (ignoreId.HasValue)
            {
                query = query.Where(d => d.Id != ignoreId.Value);
            }
            return await query.AnyAsync(d => d.Name == name);
        }

        public async Task<Department?> GetWithMembersAsync(int id)
        {
            return await Context.Departments
                .Include(d => d.Courses)
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}


