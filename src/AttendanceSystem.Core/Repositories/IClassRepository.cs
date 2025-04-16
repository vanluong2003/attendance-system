using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IClassRepository : IRepository<Class, Guid>
    {
        Task<Class> GetByCourseCode(string courseCode);
        Task<List<Class>> GetLecturerClassAsync(Guid LecturerID);
        Task<PageResult<ClassDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10);
    }
}
