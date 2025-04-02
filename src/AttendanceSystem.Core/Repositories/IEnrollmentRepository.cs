using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment, Guid>
    {
        Task<Enrollment> GetEnrollmentByStudentAndClass(Guid studentId, Guid classId);
    }
}
