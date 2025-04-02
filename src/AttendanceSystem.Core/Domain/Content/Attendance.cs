using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Core.Domain.Content
{
    [Table("Attendances")]
    [PrimaryKey(nameof(StudentId), nameof(ScheduleId))]
    public class Attendance
    {
        public Guid StudentId { get; set; }
        public Guid ScheduleId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public AttendanceStatus? Status { get; set; }

        public enum AttendanceStatus
        {
            Absent = 0,
            Present = 1,
            Late = 2
        }
    }
}
