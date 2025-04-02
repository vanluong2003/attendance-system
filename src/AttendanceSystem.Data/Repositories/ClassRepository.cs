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

        public async Task<PageResult<ClassInListDto>> GetClassPagingAsync(string? courseName, string? courseCode, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Classes.AsQueryable();
            if(!string.IsNullOrEmpty(courseName))
            {
                query = query.Where(x => x.CourseName.Contains(courseName));
            }
            if(!string.IsNullOrEmpty(courseCode))
            {
                query = query.Where(x => x.CourseCode.Contains(courseCode));
            }
            var totalRow = await query.CountAsync();
            query = query.OrderByDescending(x => x.LecturerID)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            return new PageResult<ClassInListDto>
            {
                Results = await _mapper.ProjectTo<ClassInListDto>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }

        public Task<List<Class>> GetLecturerClassAsync(Guid LecturerId)
        {
            return _context.Classes.Where(x => x.LecturerID == LecturerId).ToListAsync();
        }
    }
}
