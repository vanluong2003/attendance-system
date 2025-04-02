using AttendanceSystem.Core.Repositories;

namespace AttendanceSystem.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        IClassRepository Classes { get; }
        IAttendanceRepository Attendances { get; }
        IClassScheduleRepository ClassSchedules { get; }
        IEnrollmentRepository Enrollments { get; }
        IUserRepository Users { get; }
        Task<int> CompeleAsync();
    }
}
