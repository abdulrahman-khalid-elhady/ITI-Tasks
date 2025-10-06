using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;

namespace MVC_ITI_Project.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        [MinLength(2, ErrorMessage = "Location must be at least 2 characters")]

        public string ManagerName { get; set; }

        // Navigation Properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
