using Microsoft.EntityFrameworkCore;
using MVC_ITI_Project.Models;

namespace MVC_ITI_Project.Repositories
{
    public class InstructorRepository : Repository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(UniversityContext context) : base(context)
        {
        }

        public Task<Instructor?> GetWithDetailsAsync(int id)
        {
            return Context.Instructors
                .Include(i => i.Department)
                .Include(i => i.Course)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Instructor>> GetByDepartmentAsync(int departmentId)
        {
            return await Context.Instructors
                .Where(i => i.DeptId == departmentId)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }
    }
}


