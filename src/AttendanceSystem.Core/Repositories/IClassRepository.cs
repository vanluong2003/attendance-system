using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IClassRepository : IRepository<Class, Guid>
    {
        Task<List<Class>> GetLecturerClassAsync(Guid LecturerID);
        Task<PageResult<ClassInListDto>> GetClassPagingAsync(string? courseName, string? courseCode, int pageIndex = 1, int pageSize = 10);
    }
}
