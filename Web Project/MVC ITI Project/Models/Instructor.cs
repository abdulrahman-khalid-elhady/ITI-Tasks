using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ITI_Project.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Instructor name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(1000, 100000, ErrorMessage = "Salary must be between 1,000 and 100,000.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }

        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        public string? Image { get; set; }

        // Foreign Keys
        [Required(ErrorMessage = "Department selection is required.")]
        [Display(Name = "Department")]
        public int DeptId { get; set; }

        [Required(ErrorMessage = "Course selection is required.")]
        [Display(Name = "Course")]
        public int CrsId { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(DeptId))]
        [ValidateNever]
        public Department? Department { get; set; }

        [ForeignKey(nameof(CrsId))]
        [ValidateNever]
        public Course? Course { get; set; }
    }
}
