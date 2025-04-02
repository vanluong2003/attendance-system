using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Core.Domain.Content
{
    [Table("Enrollments")]
    [PrimaryKey(nameof(StudentId), nameof(ClassId))]
    public class Enrollment
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
    }
}
