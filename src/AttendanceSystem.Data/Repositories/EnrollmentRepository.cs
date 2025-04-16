using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data.Repositories
{
    public class EnrollmentRepository : RepositoryBase<Enrollment, Guid>, IEnrollmentRepository
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
        public async Task<PageResult<EnrollmentDto>> GetAllPaging(Guid keyword, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Enrollments.AsQueryable();
            query = query.Where(x => x.ClassId.Equals(keyword));
            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.ClassId)
               .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

            return new PageResult<EnrollmentDto>
            {
                Results = await _mapper.ProjectTo<EnrollmentDto>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }

        public async Task<PageResult<StudentInClassDto>> GetAllStudent(Guid classId)
        {
            var students = await (from e in _context.Enrollments
                                  join u in _context.Users on e.StudentId equals u.Id
                                  where e.ClassId == classId
                                  select new StudentInClassDto
                                  {
                                      StudentId = e.StudentId,
                                      StudentFirstName = u.FirstName,
                                      StudentLastName = u.LastName
                                  }).ToListAsync();
            return new PageResult<StudentInClassDto>
            {
                Results = students,
                CurrentPage = 1,
                RowCount = students.Count,
                PageSize = 10
            };
        }
    }
}
