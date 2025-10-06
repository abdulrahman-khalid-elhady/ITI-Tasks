using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ITI_Project.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [MinLength(3, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; }

        public string Image { get; set; }

        [MaxLength(30,ErrorMessage = "Address cannot exceed 30 characters")]
   
        [Display(Name = "City")]
        public string Address { get; set; }

        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
        public double Grade { get; set; }

        // Foreign Key
        //make dropdown list

        [Display(Name ="Department")]
        [Required]
        public int DeptId { get; set; }

        [ForeignKey(nameof(DeptId))]
        [ValidateNever]
        public Department? Department { get; set; }

        // Navigation
        public ICollection<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();
    }
}
