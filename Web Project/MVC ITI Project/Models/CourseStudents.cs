using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ITI_Project.Models
{
    public class CourseStudents
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 100)]
        public double Degree { get; set; }

        // Foreign Keys
        [Required]
        [Display(Name = "Course")]
        public int CrsId { get; set; }

        [Required]
        [Display(Name = "Student")]
        public int StdId { get; set; }

        [ForeignKey(nameof(CrsId))]
        [ValidateNever]
        public Course? Course { get; set; }

        [ForeignKey(nameof(StdId))]
        [ValidateNever]
        public Student? Student { get; set; }
    }
}
