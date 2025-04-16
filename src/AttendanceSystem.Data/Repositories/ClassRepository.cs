using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data.Repositories
{
    public class ClassRepository : RepositoryBase<Class, Guid>, IClassRepository
    {
        private readonly IMapper _mapper;
        public ClassRepository(AttendanceSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PageResult<ClassDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Classes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.CourseCode.Contains(keyword));
            }
            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.CourseCode)
               .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

            return new PageResult<ClassDto>
            {
                Results = await _mapper.ProjectTo<ClassDto>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }

        public Task<Class> GetByCourseCode(string courseCode)
        {
            return _context.Classes.FirstOrDefaultAsync(x => x.CourseCode == courseCode);
        }

        public Task<List<Class>> GetLecturerClassAsync(Guid LecturerId)
        {
            return _context.Classes.Where(x => x.LecturerID == LecturerId).ToListAsync();
        }
    }
}
