using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data.Repositories
{
    public class ClassScheduleRepository : RepositoryBase<ClassSchedule, Guid>, IClassScheduleRepository
    {
        private readonly IMapper _mapper;
        public ClassScheduleRepository(AttendanceSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public Task<ClassSchedule> GetScheduleByDateAndLocation(string location, DateTime date)
        {
            return _context.ClassSchedules.Where(x => x.Location == location &&
                                          x.StartTime <= date && date <= x.EndTime)
                .FirstOrDefaultAsync();
        }

        public async Task<PageResult<ClassScheduleDto>> GetAllPaging(Guid keyword, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.ClassSchedules.AsQueryable();
            query = query.Where(x => x.ClassId.Equals(keyword));
            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.StartTime)
               .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

            return new PageResult<ClassScheduleDto>
            {
                Results = await _mapper.ProjectTo<ClassScheduleDto>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }
    }
}
