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
        public int CrsId { get; set; }
        public int StdId { get; set; }

        [ForeignKey(nameof(CrsId))]
        public Course Course { get; set; }

        [ForeignKey(nameof(StdId))]
        public Student Student { get; set; }
    }
}
