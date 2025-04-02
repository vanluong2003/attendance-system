using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IClassScheduleRepository : IRepository<ClassSchedule, Guid>
    {
        Task<ClassSchedule> GetScheduleByDateAndLocation(string location, DateTime date);
    }
}
