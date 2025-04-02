using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data.Repositories
{
    internal class EnrollmentRepository : RepositoryBase<Enrollment, Guid>, IEnrollmentRepository
    {
        private readonly IMapper _mapper;
        public EnrollmentRepository(AttendanceSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public Task<Enrollment> GetEnrollmentByStudentAndClass(Guid studentId, Guid classId)
        {
            return _context.Enrollments.Where(x => x.StudentId == studentId && x.ClassId == classId).FirstOrDefaultAsync();
        }
    }
}
