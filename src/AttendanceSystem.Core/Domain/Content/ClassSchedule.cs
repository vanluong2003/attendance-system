using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Core.Domain.Content
{
    [Table("ClassSchedules")]
    public class ClassSchedule
    {
        [Key]
        public Guid Id { get; set; }
        public required Guid ClassId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public required string Location { get; set; }
        public required DateTime StartTime  { get; set; }
        public required DateTime EndTime { get; set; }

    }
}
