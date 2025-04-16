using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IClassScheduleRepository : IRepository<ClassSchedule, Guid>
    {
        Task<ClassSchedule> GetScheduleByDateAndLocation(string location, DateTime date);
        Task<PageResult<ClassScheduleDto>> GetAllPaging(Guid keyword, int pageIndex = 1, int pageSize = 10);
    }
}
