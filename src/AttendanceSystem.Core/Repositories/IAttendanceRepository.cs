using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IAttendanceRepository : IRepository<Attendance, Guid>
    {
        Task AddAttendaceAsync(Guid studentId, Guid scheduleId, DateTime? checkInTime, Attendance.AttendanceStatus attendanceStatus);
        Task GetAttendanceByStudentAndClassSchedule(Guid studentId, Guid scheduleId);
    }
}
