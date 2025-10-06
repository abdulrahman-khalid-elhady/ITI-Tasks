using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ITI_Project.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="The Name is Required")]
        [MaxLength(100,ErrorMessage ="The Max lengh is 100")]
        [MinLength(2,ErrorMessage = "The Min lengh is 2")]
        public string Name { get; set; }

        [Range(0, 100,ErrorMessage = "The Range must be between 0 and 100")]
        public double Degree { get; set; }

        [Range(0, 100,ErrorMessage = "The Range must be between 0 and 100")]
        public double MinimumDegree { get; set; }

        public int Hours { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department")]
        public int DeptId { get; set; }
        [ValidateNever]
        public Department Department { get; set; }

        // Navigation
        public ICollection<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
