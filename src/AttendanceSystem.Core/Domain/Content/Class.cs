using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Core.Domain.Content
{
    [Table("Classes")]
    public class Class
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public required string CourseName { get; set; }
        [MaxLength(50)]
        public required string CourseCode { get; set; }
        public required Guid LecturerID { get; set; } 
    }
}
