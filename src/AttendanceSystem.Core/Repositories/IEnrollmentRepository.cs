using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment, Guid>
    {
        Task<Enrollment> GetEnrollmentByStudentAndClass(Guid studentId, Guid classId);
        Task<PageResult<EnrollmentDto>> GetAllPaging(Guid keyword, int pageIndex = 1, int pageSize = 10);
        Task<PageResult<StudentInClassDto>> GetAllStudent(Guid classId);
    }
}
