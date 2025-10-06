using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MVC_ITI_Project.Models
{
    public class UniversityContext : IdentityDbContext<ApplicationUser>
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }
            public DbSet<Department> Departments { get; set; }
            public DbSet<Course> Courses { get; set; }
            public DbSet<Student> Students { get; set; }
            public DbSet<Instructor> Instructors { get; set; }
            public DbSet<CourseStudents> CourseStudents { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                
            }
        // DbContext is configured via DI in Program.cs

    }
}
